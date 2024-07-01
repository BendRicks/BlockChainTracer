using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChainTracer.Bridges.Across.Event;

[Event("FilledV3Relay")]
public class FilledV3Relay : IEventDTO
{
    [Parameter("address", "inputToken", 1)]
    public string InputToken { get; set; }

    [Parameter("address", "outputToken", 2)]
    public string OutputToken { get; set; }

    [Parameter("uint256", "inputAmount", 3)]
    public BigInteger InputAmount { get; set; }

    [Parameter("uint256", "outputAmount", 4)]
    public BigInteger OutputAmount { get; set; }

    [Parameter("uint256", "repaymentChainId", 5)]
    public BigInteger RepaymentChainId { get; set; }

    [Parameter("uint256", "originChainId", 6, true)]
    public BigInteger OriginChainId { get; set; }

    [Parameter("uint32", "depositId", 7, true)]
    public uint DepositId { get; set; }

    [Parameter("uint32", "fillDeadline", 8)]
    public uint FillDeadline { get; set; }

    [Parameter("uint32", "exclusivityDeadline", 9)]
    public uint ExclusivityDeadline { get; set; }

    [Parameter("address", "exclusiveRelayer", 10)]
    public string ExclusiveRelayer { get; set; }

    [Parameter("address", "relayer", 11, true)]
    public string Relayer { get; set; }

    [Parameter("address", "depositor", 12)]
    public string Depositor { get; set; }

    [Parameter("address", "recipient", 13)]
    public string Recipient { get; set; }

    [Parameter("bytes", "message", 14)]
    public byte[] Message { get; set; }

    [Parameter("tuple", "relayExecutionInfo", 15)]
    public V3RelayExecutionEventInfoBase RelayExecutionInfo { get; set; }
}