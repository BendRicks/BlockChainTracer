
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChainTracer.Bridges.Socket.Event;

[Event("Send")]
public class Send : IEventDTO
{
    [Parameter("address", "receiver", 1)]
    public string Receiver { get; set; }

    [Parameter("uint256", "amount", 2)]
    public BigInteger Amount { get; set; }

    [Parameter("bytes32", "srcChainTxHash", 3)]
    public byte[] SrcChainTxHash { get; set; }
}
