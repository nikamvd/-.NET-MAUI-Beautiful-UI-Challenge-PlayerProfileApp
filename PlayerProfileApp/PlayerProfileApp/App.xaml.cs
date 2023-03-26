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
                BarBackgroundColor = Color.FromRgba("#512BD4"),
                BackgroundColor = Color.FromRgba("#512BD4"),
                BarTextColor = Color.FromRgba("#FFFFFF")
            };
            MainPage = navPage;
        }
    }
}

