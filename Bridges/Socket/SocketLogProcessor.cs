using System.Text;
using BlockChainTracer.Bridges.Socket.Event;
using BlockChainTracer.Model;
using BlockChainTracer.Service;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;

namespace BlockChainTracer.Bridges.Socket;

public class SocketLogProcessor : ILogProcessor
{
    public string GetBridgeName()
    {
        return "Socket";
    }

    public (CrossChainSwap, bool) ProcessLog(FilterLog log, string chain)
    {
        var socketBridge = log.DecodeEvent<SocketBridge>();
        if (socketBridge != null)
        {
            return (SocketTransactionsMatcher.PairInput(new IsolatedTransaction(chain, log.TransactionHash, socketBridge.Event.Sender, socketBridge.Event.Receiver),
                (log.TransactionHash[2..].ToUpperInvariant(), socketBridge.Event.Receiver)), true);
        }
        var send = log.DecodeEvent<Send>();
        if (send != null)
        {
            var txHash = BitConverter.ToString(send.Event.SrcChainTxHash).Replace("-", "");
            return (SocketTransactionsMatcher.PairOutput(new IsolatedTransaction(chain, log.TransactionHash, null, send.Event.Receiver), //Не хочу делать отдельный запрос для получения адреса контракта. Да это в принципе и не нужно
                (txHash, send.Event.Receiver)), true);
        }
        return (null, false);
    }
}
