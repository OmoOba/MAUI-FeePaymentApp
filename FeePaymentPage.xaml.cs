using FeePaymentApp.Models;
using FeePaymentApp.Services;

namespace FeePaymentApp;

public partial class FeePaymentPage : ContentPage
{
    private readonly MockApiService _apiService; // Use MockApiService for testing
    // private readonly ApiService _apiService;     // Use ApiService for real API
    private StudentDto? _currentStudent;

    private readonly Dictionary<string, decimal> _feeAmounts = new()
    {
        { "Acceptance fee", 5000 },
        { "Verification fee", 10000 },
        { "School fee", 50000 }
    };

    public FeePaymentPage()
    {
        InitializeComponent();
        _apiService = new MockApiService();
        // _apiService = new ApiService(); // Uncomment for real API
    }

    private async void OnVerifyMatricClicked(object sender, EventArgs e)
    {
        var matricNo = MatricEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(matricNo))
        {
            await DisplayAlert("Validation", "Please enter your matric number.", "OK");
            return;
        }

        try
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            ErrorLabel.IsVisible = false;

            _currentStudent = await _apiService.GetStudentByMatricNoAsync(matricNo);

            if (_currentStudent != null)
            {
                StudentNameLabel.Text = $"{_currentStudent.FirstName} {_currentStudent.LastName}";
                StudentEmailLabel.Text = _currentStudent.Email;
                StudentDepartmentLabel.Text = _currentStudent.Department;
                StudentLevelLabel.Text = _currentStudent.CurrentLevel.ToString();

                StudentInfoFrame.IsVisible = true;
                SubmitButton.IsEnabled = true;
            }
            else
            {
                ErrorLabel.Text = "Matric number not found. Please check and try again.";
                ErrorLabel.IsVisible = true;
                StudentInfoFrame.IsVisible = false;
                SubmitButton.IsEnabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = $"Error: {ex.Message}";
            ErrorLabel.IsVisible = true;
            StudentInfoFrame.IsVisible = false;
            SubmitButton.IsEnabled = false;
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
        }
    }

    private void OnFeeTypeChanged(object sender, EventArgs e)
    {
        var selectedFee = FeeTypePicker.SelectedItem?.ToString();

        if (selectedFee != null && _feeAmounts.ContainsKey(selectedFee))
        {
            var amount = _feeAmounts[selectedFee];
            FeeAmountLabel.Text = $"Amount: ₦{amount:N0}";
            FeeAmountLabel.IsVisible = true;
        }
        else
        {
            FeeAmountLabel.IsVisible = false;
        }
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var feeType = FeeTypePicker.SelectedItem?.ToString();
        var level = LevelPicker.SelectedItem?.ToString();

        if (_currentStudent == null)
        {
            await DisplayAlert("Validation", "Please verify your matric number first.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(feeType))
        {
            await DisplayAlert("Validation", "Please select a fee type.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(level))
        {
            await DisplayAlert("Validation", "Please select your level.", "OK");
            return;
        }

        try
        {
            SubmitButton.IsEnabled = false;

            var request = new FeePaymentRequest
            {
                MatricNo = _currentStudent.MatricNo,
                FeeType = feeType,
                Level = level,
                Amount = _feeAmounts[feeType]
            };

            var response = await _apiService.SubmitFeePaymentAsync(request);

            if (response?.Success == true)
            {
                // Navigate to confirmation page
                await Navigation.PushAsync(new ConfirmationPage(
                    _currentStudent.MatricNo!,
                    $"{_currentStudent.FirstName} {_currentStudent.LastName}",
                    feeType,
                    level,
                    response.Amount,
                    response.TransactionId!
                ));
            }
            else
            {
                await DisplayAlert("Error", response?.Message ?? "Payment submission failed.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
        finally
        {
            SubmitButton.IsEnabled = true;
        }
    }
}
