namespace FeePaymentApp.Models;

public class FeePaymentRequest
{
    public string? MatricNo { get; set; }
    public string? FeeType { get; set; }
    public string? Level { get; set; }
    public decimal Amount { get; set; }
}
