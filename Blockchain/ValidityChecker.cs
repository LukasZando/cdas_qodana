namespace BlockchainApp.Blockchain;

public static class ValidityChecker
{
    
    public static bool IsValidChain(List<Block> chain)
    {
        var lastBlock = chain.First();
        var currentIndex = 1;

        while (currentIndex < chain.Count)
        {
            var block = chain[currentIndex];

            if (block.PreviousHash != lastBlock.Hash())
                return false;

            if (!IsValidProof(lastBlock.Proof, block.Proof))
                return false;

            lastBlock = block;
            currentIndex++;
        }

        return true;
    }
    
    public static bool IsValidProof(long lastProof, long proof)
    {
        var guess = $"{lastProof}{proof}";
        var guessHash = Hasher.Sha256(guess);
        return guessHash.StartsWith("0000");
    }
}