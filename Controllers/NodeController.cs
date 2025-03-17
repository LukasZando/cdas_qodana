using System.Text.Json;
using BlockchainApp.Blockchain;
using Microsoft.AspNetCore.Mvc;

namespace BlockchainApp.Controllers;

[ApiController]
[Route("[controller]")]
public class NodeController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("resolve")]
    public async Task<ActionResult<(string message, ChainResponse chain)>> Resolve()
    {
        var replaced = await BlockchainImpl.ResolveConflicts();
        var response = new ChainResponse
        {
            Chain = BlockchainImpl.Chain,
            Length = BlockchainImpl.Chain.Count
        };
        
        Console.WriteLine(replaced ? "Our chain was replaced" : "Our chain is authoritative");
        Console.WriteLine(JsonSerializer.Serialize(response));
        
        return (replaced ? "Our chain was replaced" : "Our chain is authoritative", response);
    }
    
    [HttpPost]
    [Route("register")]
    public ActionResult<HashSet<string>> NewTransaction(List<string> nodes)
    {
        BlockchainImpl.RegisterNodes(nodes);
        
        return BlockchainImpl.Nodes;
    }
}