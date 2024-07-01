using System.Collections.Concurrent;
using BlockChainTracer.Model;
using BlockChainTracer.Service;

namespace BlockChainTracer.Bridges.Socket;

public class SocketTransactionsMatcher : ITransactionMatcher
{

    private static readonly ConcurrentDictionary<object, IsolatedTransaction> _unpairedsocketBridges = new();
    private static readonly ConcurrentDictionary<object, IsolatedTransaction> _unpairedSends = new();

    public static CrossChainSwap PairInput(IsolatedTransaction isolatedTransaction, object key)
    {
        if (_unpairedSends.ContainsKey(key))
        {
            var sendTransaction = _unpairedSends[key];
            _unpairedSends.TryRemove(key, out _);
            return new CrossChainSwap(isolatedTransaction, sendTransaction, DateTime.UtcNow);
        }
        else
        {
            _unpairedsocketBridges.TryAdd(key, isolatedTransaction);
        }
        return null;
    }

    public static CrossChainSwap PairOutput(IsolatedTransaction isolatedTransaction, object key)
    {
        if (_unpairedsocketBridges.ContainsKey(key))
        {
            var socketBridgeTransaction = _unpairedsocketBridges[key];
            _unpairedsocketBridges.TryRemove(key, out _);
            return new CrossChainSwap(socketBridgeTransaction, isolatedTransaction, DateTime.UtcNow);
        }
        else
        {
            _unpairedSends.TryAdd(key, isolatedTransaction);
        }
        return null;
    }

}