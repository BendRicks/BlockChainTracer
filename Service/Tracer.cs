using System.Collections.Concurrent;
using System.Numerics;
using BlockChainTracer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Web3;

namespace BlockChainTracer.Service;

class Tracer : IHostedService
{
    private readonly IConfiguration _config;
    private readonly Dictionary<string, BridgeConfig> _bridgesConfigs;
    private readonly Dictionary<string, ChainConfig> _chainsConfigs;

    private readonly CrossChainSwapContext _crossChainSwapContext;

    public Tracer(IConfiguration config, CrossChainSwapContext crossChainSwapContext, IOptions<Dictionary<string, BridgeConfig>> bridgeConfigs, IOptions<Dictionary<string, ChainConfig>> chainsConfigs)
    {
        _config = config;
        _crossChainSwapContext = crossChainSwapContext;
        _bridgesConfigs = bridgeConfigs.Value;
        _chainsConfigs = chainsConfigs.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        bool useDatabase = _config["useDatabase"].Equals("True");
        await Parallel.ForEachAsync(_chainsConfigs, async (chainConfig, index) =>
        {
            var thread = new Thread(new ThreadStart(async () =>
            {
                var web3 = new Web3(chainConfig.Value.Url);

                ConcurrentBag<ILogProcessor> logProcessors = new(
                    chainConfig.Value.SupportedBridges.Select(str => (ILogProcessor)Activator.CreateInstance(Type.GetType(_bridgesConfigs[str].LogProcessorClassName)))
                );

                var blockProgressRepo = new InMemoryBlockchainProgressRepository();

                var processor = web3.Processing.Logs.CreateProcessor(
                    async log =>
                    {
                        foreach (var logProcessor in logProcessors)
                        {
                            var processResults = logProcessor.ProcessLog(log, chainConfig.Key);
                            if (processResults.Item2)
                            {
                                if (processResults.Item1 != null)
                                {
                                    processResults.Item1.BridgeName = logProcessor.GetBridgeName();
                                    Console.WriteLine("==============================================================================================================================");

                                    if (useDatabase)
                                    {
                                        try
                                        {
                                            await _crossChainSwapContext.CrossChainSwaps.AddAsync(processResults.Item1);
                                            await _crossChainSwapContext.SaveChangesAsync();
                                            Console.WriteLine("Bridge: {0} \t\tRecord's id in database - {1}", logProcessor.GetBridgeName(), processResults.Item1.Id);
                                        }
                                        catch (Exception e)
                                        {
                                            string message = e.InnerException != null ? e.InnerException.Message : e.Message;
                                            Console.WriteLine("Error inserting cross-chain swap, cause: {0}", message);
                                            Console.WriteLine("Bridge: {0}", logProcessor.GetBridgeName());
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Bridge: {0}", logProcessor.GetBridgeName());
                                    }
                                    Console.WriteLine("Type: \t\t{0,-72}{1,-72}", "Input", "Output");
                                    Console.WriteLine("Chain: \t\t{0,-72}{1,-72}", processResults.Item1.InputTransaction.ChainName, processResults.Item1.OutputTransaction.ChainName);
                                    Console.WriteLine("Address: \t{0,-72}{1,-72}", processResults.Item1.InputTransaction.AddressFrom, processResults.Item1.OutputTransaction.AddressTo);
                                    Console.WriteLine("TxHash: \t{0,-72}{1,-72}", processResults.Item1.InputTransaction.TransactionHash, processResults.Item1.OutputTransaction.TransactionHash);
                                    Console.WriteLine("==============================================================================================================================");
                                }
                                break;
                            }
                        }
                    }, blockProgressRepository: blockProgressRepo
                );
                if (chainConfig.Value.StartBlock == 0 && chainConfig.Value.EndBlock == 0)
                {
                    var latestBlock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();
                    Console.WriteLine("Starting log processor for {0} in realtime (latest block is {1})", chainConfig.Key, latestBlock);
                    await processor.ExecuteAsync(startAtBlockNumberIfNotProcessed: latestBlock, waitInterval: chainConfig.Value.Period);
                }
                else if (chainConfig.Value.StartBlock != 0 && chainConfig.Value.EndBlock == 0)
                {
                    Console.WriteLine("Starting log processor for {0} for blocks from {1}", chainConfig.Key, chainConfig.Value.StartBlock);
                    await processor.ExecuteAsync(
                    startAtBlockNumberIfNotProcessed: new BigInteger(chainConfig.Value.StartBlock),
                    waitInterval: chainConfig.Value.Period);
                }
                else
                {
                    Console.WriteLine("Starting log processor for {0} for blocks from {1} to {2}", chainConfig.Key, chainConfig.Value.StartBlock, chainConfig.Value.EndBlock);
                    await processor.ExecuteAsync(
                    startAtBlockNumberIfNotProcessed: new BigInteger(chainConfig.Value.StartBlock),
                    toBlockNumber: new BigInteger(chainConfig.Value.EndBlock)
                    );
                }
            }));
            thread.Start();
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}