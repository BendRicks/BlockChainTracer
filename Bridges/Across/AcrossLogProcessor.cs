using BlockChainTracer.Bridges.Across.Event;
using BlockChainTracer.Model;
using BlockChainTracer.Service;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;

namespace BlockChainTracer.Bridges.Across
{
    public class AcrossLogProcessor : ILogProcessor
    {
        public string GetBridgeName()
        {
            return "Across";
        }

        public (CrossChainSwap, bool) ProcessLog(FilterLog log, string chain)
        {
            var deposit = log.DecodeEvent<V3FundsDeposited>();
            if (deposit != null)
            {
                return (AcrossTransactionsMatcher.PairInput(new IsolatedTransaction(chain, log.TransactionHash, deposit.Event.Recipient, deposit.Event.Depositor), deposit.Event.DepositId), true);
            }
            var fill = log.DecodeEvent<FilledV3Relay>();
            if (fill != null)
            {
                return (AcrossTransactionsMatcher.PairOutput(new IsolatedTransaction(chain, log.TransactionHash, fill.Event.Depositor, fill.Event.Recipient), fill.Event.DepositId), true);
            }
            return (null, false);
        }

    }
}