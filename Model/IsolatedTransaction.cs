namespace BlockChainTracer.Model;

public class IsolatedTransaction
{
    public long Id { get; set; }
    public string TransactionHash { get; set; }
    public string ChainName { get; set; }
    public string AddressFrom { get; set; }
    public string AddressTo { get; set; }

    public IsolatedTransaction(string chainName, string transactionHash, string addressFrom, string addressTo)
    {
        TransactionHash = transactionHash;
        ChainName = chainName;
        AddressFrom = addressFrom;
        AddressTo = addressTo;
    }
}
