using System.Collections.Concurrent;
using BlockChainTracer.Model;
using BlockChainTracer.Service;

namespace BlockChainTracer.Bridges.Across
{

    public class AcrossTransactionsMatcher : ITransactionMatcher
    {

        private static readonly ConcurrentDictionary<object, IsolatedTransaction> _unpairedDeposits = new();
        private static readonly ConcurrentDictionary<object, IsolatedTransaction> _unpairedFills = new();

        public static CrossChainSwap PairInput(IsolatedTransaction isolatedTransaction, object key)
        {
            if (_unpairedFills.ContainsKey(key)) {
                var fillTransaction = _unpairedFills[key];
                _unpairedFills.TryRemove(key, out _);
                return new CrossChainSwap(isolatedTransaction, fillTransaction, DateTime.UtcNow);
            } else {
                _unpairedDeposits.TryAdd(key, isolatedTransaction);
            }
            return null;
        }

        public static CrossChainSwap PairOutput(IsolatedTransaction isolatedTransaction, object key)
        {
            if (_unpairedDeposits.ContainsKey(key)) {
                var depositTransaction = _unpairedDeposits[key];
                _unpairedDeposits.TryRemove(key, out _);
                return new CrossChainSwap(depositTransaction, isolatedTransaction, DateTime.UtcNow);
            } else {
                _unpairedFills.TryAdd(key, isolatedTransaction);
            }
            return null;
        }

    }
}