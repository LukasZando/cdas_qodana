namespace BlockchainApp.Blockchain;

public class ChainResponse
{
    public List<Block> Chain { get; set; }
    public long Length { get; set; }
}