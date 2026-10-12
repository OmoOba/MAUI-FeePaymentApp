using System.Threading.Tasks;

namespace FeePaymentApp;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
        _ = LoadMainPageAsync();
    }

    private async Task LoadMainPageAsync()
    {
        await Task.Delay(2500);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Application.Current!.MainPage = new FeePaymentPage();
        });
    }
}
