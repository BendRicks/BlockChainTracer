using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace BlockChainTracer.Model
{
    public class IsolatedTransaction
    {
        public long Id { get; set; }
        public string TransactionHash { get; set; }
        public string ChainName { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }

        public IsolatedTransaction(string chainName, string transactionHash, string addressFrom, string addressTo)
        {
            ChainName = chainName;
            TransactionHash = transactionHash;
            AddressFrom = addressFrom;
            AddressTo = addressTo;
        }

    }
}