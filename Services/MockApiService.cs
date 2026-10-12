using FeePaymentApp.Models;

namespace FeePaymentApp.Services;

/// <summary>
/// Mock API service for testing without a real backend.
/// Replace ApiService with this for development/testing.
/// </summary>
public class MockApiService
{
    private readonly Random _random = new();

    public async Task<StudentDto?> GetStudentByMatricNoAsync(string matricNo)
    {
        // Simulate network delay
        await Task.Delay(1500);

        // Mock data - returns student if matric no matches pattern
        if (matricNo.Length >= 5)
        {
            return new StudentDto
            {
                MatricNo = matricNo,
                FirstName = "Adebayo",
                LastName = "Shamsideen",
                Email = "adebayo@university.edu",
                Department = "Computer Science",
                CurrentLevel = 300
            };
        }

        // Return null for invalid matric number
        return null;
    }

    public async Task<FeePaymentResponse?> SubmitFeePaymentAsync(FeePaymentRequest request)
    {
        // Simulate network delay
        await Task.Delay(2000);

        // Mock payment processing
        var transactionId = $"TXN{DateTime.Now.Ticks}";
        var feeAmounts = new Dictionary<string, decimal>
        {
            { "Acceptance fee", 5000 },
            { "Verification fee", 10000 },
            { "School fee", 50000 }
        };

        var amount = feeAmounts.ContainsKey(request.FeeType ?? "")
            ? feeAmounts[request.FeeType]
            : 0;

        return new FeePaymentResponse
        {
            Success = true,
            Message = "Payment processed successfully",
            TransactionId = transactionId,
            Amount = amount
        };
    }
}
