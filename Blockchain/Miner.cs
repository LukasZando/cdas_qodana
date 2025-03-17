namespace BlockchainApp.Blockchain;

public static class Miner
{
    private static IHttpContextAccessor _httpContextAccessor;
    
    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public static MineResponse Mine()
    {
        var lastBlock = BlockchainImpl.LastBlock;
        var lastProof = lastBlock.Proof;
        
        var proof = ProofOfWork(lastProof);
        
        var port = _httpContextAccessor?.HttpContext?.Request.Host.Port?.ToString() ?? "unknown_port";

        BlockchainImpl.NewTransaction("0", port, 1);
        
        var previousHash = lastBlock.Hash();
        var block = BlockchainImpl.NewBlock(proof, previousHash);
        
        return new MineResponse
        {
            Message = "New Block Forged",
            Index = block.Index,
            Transactions = block.Transactions,
            Proof = block.Proof,
            PreviousHash = block.PreviousHash
        };
    }
    
    private static long ProofOfWork(long lastProof)
    {
        long proof = 0;
        
        while (!ValidityChecker.IsValidProof(lastProof, proof)) proof++;

        return proof;
    }
}