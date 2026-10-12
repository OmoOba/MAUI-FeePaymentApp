namespace FeePaymentApp.Models;

public class FeePaymentResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public string? TransactionId { get; set; }
    public decimal Amount { get; set; }
}
