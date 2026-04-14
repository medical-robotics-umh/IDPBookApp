using IDPBookApp.Pages;
using IDPBookApp.Models;
using IDPBookApp.ViewModel;

namespace IDPBookApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        return new Window(new AppShell());
    }
}
 