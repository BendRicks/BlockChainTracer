using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockChainTracer.Bridges.Across.Event
{
    public class V3RelayExecutionEventInfoBase
    {
        [Parameter("address", "updatedRecipient", 1)]
        public string UpdatedRecipient { get; set; }
        [Parameter("bytes", "updatedMessage", 2)]
        public byte[] UpdatedMessage { get; set; }
        [Parameter("uint256", "updatedOutputAmount", 3)]
        public BigInteger UpdatedOutputAmount { get; set; }
        [Parameter("uint8", "fillType", 4)]
        public byte FillType { get; set; }
    }
}