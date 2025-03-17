namespace BlockchainApp.Blockchain;

public static class BlockchainImpl
{
    public static List<Block> Chain { get; private set; }
    
    public static HashSet<string> Nodes { get; }

    private static List<Transaction> _currentTransactions;

    static BlockchainImpl()
    {
        Chain = [];
        NewBlock(100, "1");

        _currentTransactions = [];
        Nodes = [];
    }
    
    public static long NewTransaction(string sender, string recipient, long amount)
    {
        _currentTransactions.Add(new Transaction
        {
            Sender = sender,
            Recipient = recipient,
            Amount = amount
        });

        return LastBlock.Index + 1;
    }
    
    public static Block NewBlock(long proof, string? previousHash = null)
    {
        var block = new Block
        {
            Index = Chain.Count + 1,
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            Transactions = _currentTransactions,
            Proof = proof,
            PreviousHash = previousHash ?? LastBlock.Hash()
        };

        _currentTransactions = new List<Transaction>();
        Chain.Add(block);

        return block;
    }
    
    public static Block LastBlock => Chain.Last();
    
    public static void RegisterNodes(IEnumerable<string> addresses)
    {
        Nodes.UnionWith(addresses);
    }
    
    public static async Task<bool> ResolveConflicts()
    {
        var neighbours = Nodes;
        List<Block>? newChain = null;

        var maxLength = (long) Chain.Count;
        
        var client = new HttpClient();

        foreach (var node in neighbours)
        {
            var response = await client.GetFromJsonAsync<ChainResponse>($"http://{node}/BlockChain/chain");

            if (response == null) continue;

            var length = response.Length;
            var chain = response.Chain;

            if (length > maxLength && ValidityChecker.IsValidChain(chain))
            {
                maxLength = length;
                newChain = chain;
            }
        }

        if (newChain != null)
        {
            Chain = newChain;
            return true;
        }

        return false;
    }
}