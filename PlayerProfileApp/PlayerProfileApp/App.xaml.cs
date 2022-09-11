using PlayerProfileApp.Pages;
using PlayerProfileApp.ViewModels;
using PlayerProfileApp.Constants;

namespace PlayerProfileApp;

public partial class App : Application
{
	public App(FriendsListViewModel friendsListViewModel)
	{
		InitializeComponent();

        if (Config.Desktop)
        {
            var desktopShellViewModel = new DesktopShellViewModel(friendsListViewModel);
            var desktopShell = new DesktopShell()
            {
                BindingContext = desktopShellViewModel
            };
            MainPage = desktopShell;
        }
        else
        {
            var friendsListPage = new FriendsListPage() { BindingContext = friendsListViewModel };
            var navPage = new NavigationPage(friendsListPage)
            {
                BarBackgroundColor = Color.FromRgba("#F1791C"),
                BackgroundColor = Color.FromRgba("#F1791C"),
                BarTextColor = Color.FromRgba("#FFFFFF")
            };
            MainPage = navPage;
        }
    }
}

