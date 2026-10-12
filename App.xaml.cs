using FeePaymentApp.Services;

namespace FeePaymentApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new SplashPage();
    }
}
