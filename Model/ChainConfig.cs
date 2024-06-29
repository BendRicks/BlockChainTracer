namespace BlockChainTracer.Model;

public class ChainConfig
{
    public required string Url { get; set; }
    public int Period { get; set; }
    public int StartBlock { get; set; }
    public int EndBlock { get; set; }
    public List<string> SupportedBridges { get; set; }
}