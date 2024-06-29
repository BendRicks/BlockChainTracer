using BlockChainTracer.Model;

namespace BlockChainTracer.Service
{
    public interface ITransactionMatcher
    {
        /// <summary>
        /// Tries to find cross-chain swaps with this transaction as input
        /// </summary>
        /// <param name="transaction">Input transaction</param>
        /// <param name="key">Key, used to pair transactions</param>
        /// <returns>Null if no cross-chain swaps where found, otherwise CrossChainSwap object</returns>
        static CrossChainSwap PairInput(IsolatedTransaction transaction, object key)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Tries to find cross-chain swaps with this transaction as output
        /// </summary>
        /// <param name="transaction">Output transaction</param>
        /// <param name="key">Key, used to pair transactions</param>
        /// <returns>Null if no cross-chain swaps where found, otherwise CrossChainSwap object</returns>
        static CrossChainSwap PairOutput(IsolatedTransaction transaction, object key) 
        {
            throw new NotImplementedException();
        }
    }
}