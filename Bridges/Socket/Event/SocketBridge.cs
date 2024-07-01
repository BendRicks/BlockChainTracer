using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChainTracer.Bridges.Socket.Event;

[Event("SocketBridge")]
public class SocketBridge : IEventDTO
{
    [Parameter("uint256", "amount", 1)]
    public BigInteger Amount { get; set; }
    
    [Parameter("address", "token", 2)]
    public string Token { get; set; }

    [Parameter("uint256", "toChainId", 3)]
    public BigInteger ToChainId { get; set; }

    [Parameter("bytes32", "bridgeName", 4)]
    public byte[] BridgeName { get; set; }

    [Parameter("address", "sender", 5)]
    public string Sender { get; set; }
    
    [Parameter("address", "receiver", 6)]
    public string Receiver { get; set; }

    [Parameter("bytes32", "metadata", 7)]
    public byte[] Metadata { get; set; }
}
