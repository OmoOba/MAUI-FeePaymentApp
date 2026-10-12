namespace FeePaymentApp;

public partial class FeePaymentPage : ContentPage
{
    public FeePaymentPage()
    {
        InitializeComponent();
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var matricNo = MatricEntry.Text?.Trim();
        var feeType = FeeTypePicker.SelectedItem?.ToString();
        var level = LevelPicker.SelectedItem?.ToString();

        if (string.IsNullOrWhiteSpace(matricNo))
        {
            await DisplayAlert("Validation", "Please enter your matric number.", "OK");
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

        StatusLabel.Text =
            $"Payment submitted successfully for Matric No: {matricNo} | Fee: {feeType} | Level: {level}";
        StatusLabel.IsVisible = true;

        await DisplayAlert("Success", "Your payment was submitted successfully.", "OK");
    }
}
