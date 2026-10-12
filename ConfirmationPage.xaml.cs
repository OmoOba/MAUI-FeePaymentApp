namespace FeePaymentApp;

public partial class ConfirmationPage : ContentPage
{
    public ConfirmationPage(
        string matricNo,
        string studentName,
        string feeType,
        string level,
        decimal amount,
        string transactionId)
    {
        InitializeComponent();

        MatricNoLabel.Text = matricNo;
        NameLabel.Text = studentName;
        FeeTypeLabel.Text = feeType;
        LevelLabel.Text = level;
        AmountLabel.Text = $"₦{amount:N0}";
        TransactionIdLabel.Text = transactionId;
        DateLabel.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
    }

    private async void OnDownloadReceiptClicked(object sender, EventArgs e)
    {
        // Simulate receipt download
        await DisplayAlert("Receipt", "Receipt has been downloaded to your device.", "OK");
    }

    private async void OnBackToHomeClicked(object sender, EventArgs e)
    {
        // Clear navigation stack and go back to fee payment page
        await Navigation.PopToRootAsync();
    }
}
