using BlockchainApp.Blockchain;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Controllers;

[ApiController]
[Route("[controller]")]
public class BlockChainController
{
    [HttpGet]
    [ProducesResponseType<MineResponse>(StatusCodes.Status200OK)]
    [Route("mine")]
    public ActionResult<MineResponse> Mine()
    {
        return Miner.Mine();
    }
    
    [HttpPost]
    [Route("transactions/new")]
    public string NewTransaction(Transaction transaction)
    {
        var index = BlockchainImpl.NewTransaction(transaction.Sender, transaction.Recipient, transaction.Amount);
        
        return $"Transaction created. Will be added to Block {index}";
    }
    
    [HttpGet]
    [ProducesResponseType<ChainResponse>(StatusCodes.Status200OK)]
    [Route("chain")]
    public ActionResult<ChainResponse> GetChain()
    {
        var chain = BlockchainImpl.Chain;
        
        return new ChainResponse
        {
            Chain = chain,
            Length = chain.Count
        };
    }
}