using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BlockchainApp.Blockchain;

public class Block
{
    public long Index { get; set; }
    public long Timestamp { get; set; }
    public List<Transaction> Transactions { get; set; }
    public long Proof { get; set; }
    public string PreviousHash { get; set; }
    
    public string Hash()
    {
        var json = JsonSerializer.Serialize(this);
        return Hasher.Sha256(json);
    }
}