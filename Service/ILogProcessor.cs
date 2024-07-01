using BlockChainTracer.Model;
using Nethereum.RPC.Eth.DTOs;

namespace BlockChainTracer.Service;

public interface ILogProcessor
{
    /// <summary>
    /// Processes passed log to find Events specific for some bridge
    /// </summary>
    /// <param name="log">Log to process</param>
    /// <param name="chain">Chain name</param>
    /// <returns>
    /// Returns cortege of 2 elements: first is CrossChainSwap object (null if no full cross-chain swaps found), second is bool that indicates if log processor 
    /// found and processed event and there is no need to continue processing that log
    /// </returns>
    (CrossChainSwap, bool) ProcessLog(FilterLog log, string chain);
    string GetBridgeName();
}
