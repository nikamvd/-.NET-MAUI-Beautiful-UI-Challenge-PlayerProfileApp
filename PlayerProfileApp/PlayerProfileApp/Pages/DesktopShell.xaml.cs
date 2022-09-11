using PlayerProfileApp.Base;
using PlayerProfileApp.ViewModels;

namespace PlayerProfileApp.Pages;

public partial class DesktopShell
{
    public DesktopShell()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var desktopShellViewModel = this.BindingContext as DesktopShellViewModel;
        if(desktopShellViewModel != null)
        {
            desktopShellViewModel.OnAppearing();
        }
    }
}