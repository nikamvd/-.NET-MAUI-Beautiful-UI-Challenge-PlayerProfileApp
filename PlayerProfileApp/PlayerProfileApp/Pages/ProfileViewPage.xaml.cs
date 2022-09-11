using PlayerProfileApp.Base;

namespace PlayerProfileApp.Pages;

public partial class ProfileViewPage : BaseGolfMatePage
{
	public ProfileViewPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }
}
