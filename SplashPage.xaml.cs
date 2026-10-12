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
        await Task.Delay(3000);
        MainThread.BeginInvokeOnMainThread(() =>
        {
            Application.Current!.MainPage = new NavigationPage(new FeePaymentPage())
            {
                BarBackgroundColor = Color.FromArgb("#1F51FF")
            };
        });
    }
}
