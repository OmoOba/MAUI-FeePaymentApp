using FeePaymentApp.Models;
using System.Net.Http.Json;

namespace FeePaymentApp.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public ApiService()
    {
        _httpClient = new HttpClient();
        // Replace with your actual API URL
        _baseUrl = "https://api.example.com/api";
        _httpClient.Timeout = TimeSpan.FromSeconds(30);
    }

    public async Task<StudentDto?> GetStudentByMatricNoAsync(string matricNo)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/students/{matricNo}");
            
            if (response.IsSuccessStatusCode)
            {
                var student = await response.Content.ReadAsAsync<StudentDto>();
                return student;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching student: {ex.Message}");
            throw;
        }
    }

    public async Task<FeePaymentResponse?> SubmitFeePaymentAsync(FeePaymentRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/payments/submit", request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<FeePaymentResponse>();
                return result;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error submitting payment: {ex.Message}");
            throw;
        }
    }
}
