using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChainTracer.Bridges.Across.Event;

[Event("V3FundsDeposited")]
class V3FundsDeposited : IEventDTO
{
    [Parameter("address", "inputToken", 1)]
    public string InputToken { get; set; }

    [Parameter("address", "outputToken", 2)]
    public string OutputToken { get; set; }

    [Parameter("uint256", "inputAmount", 3)]
    public BigInteger InputAmount { get; set; }

    [Parameter("uint256", "outputAmount", 4)]
    public BigInteger OutputAmount { get; set; }

    [Parameter("uint256", "destinationChainId", 5, true)]
    public BigInteger DestinationChainId { get; set; }

    [Parameter("uint32", "depositId", 6, true)]
    public uint DepositId { get; set; }

    [Parameter("uint32", "quoteTimestamp", 7)]
    public uint QuoteTimestamp { get; set; }

    [Parameter("uint32", "fillDeadline", 8)]
    public uint FillDeadline { get; set; }

    [Parameter("uint32", "exclusivityDeadline", 9)]
    public uint ExclusivityDeadline { get; set; }

    [Parameter("address", "depositor", 10, true)]
    public string Depositor { get; set; }

    [Parameter("address", "recipient", 11)]
    public string Recipient { get; set; }

    [Parameter("address", "exclusiveRelayer", 12)]
    public string ExclusiveRelayer { get; set; }

    [Parameter("bytes", "message", 13)]
    public byte[] Message { get; set; }
}