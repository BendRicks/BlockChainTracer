using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace BlockChainTracer.Model
{
    public class IsolatedTransaction(string chainName, string transactionHash, string addressFrom, string addressTo)
    {
        public long Id { get; set; }
        public string TransactionHash { get; set; } = transactionHash;
        public string ChainName { get; set; } = chainName;
        public string AddressFrom { get; set; } = addressFrom;
        public string AddressTo { get; set; } = addressTo;
    }
}