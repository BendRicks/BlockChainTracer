using BlockChainTracer.Model;
using BlockChainTracer.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlockChainTracer;

class Program
{

    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("settings.json", false).Build();
        await Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Tracer>().AddSingleton<IConfiguration>(config).AddSingleton<CrossChainSwapContext>()
                .Configure<Dictionary<string, BridgeConfig>>(config.GetSection("bridges"))
                .Configure<Dictionary<string, ChainConfig>>(config.GetSection("chains")); 
            }).RunConsoleAsync();

    }

}
