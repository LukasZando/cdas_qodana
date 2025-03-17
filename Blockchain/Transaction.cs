using System.ComponentModel.DataAnnotations;

namespace BlockchainApp.Blockchain;

public class Transaction
{
    [Required]
    public String Sender { get; set; }
    [Required]
    public String Recipient { get; set; }
    [Required]
    public long Amount { get; set; }
}