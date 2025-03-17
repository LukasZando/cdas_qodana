namespace BlockchainApp.Blockchain;

public class MineResponse
{
    public string Message { get; set; }
    public long Index { get; set; }
    public List<Transaction> Transactions { get; set; }
    public long Proof { get; set; }
    public string PreviousHash { get; set; }
}