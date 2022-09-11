using System;
using PlayerProfileApp.Base;
using PlayerProfileApp.Models;
using PlayerProfileApp.Pages;

namespace PlayerProfileApp.ViewModels
{
    public class DesktopShellViewModel : ExtendedBindableObject, IMonitorAppearance
    {
        public DesktopShellViewModel(FriendsListViewModel friendsListViewModel)
        {
            _friendsListViewModel = friendsListViewModel;
            _friendsListViewModel.ProfileTapped += _friendsListViewModel_ProfileTapped;
        }

        private void _friendsListViewModel_ProfileTapped(object sender, ProfileTappedEventArgs e)
        {
            ProfileViewPage profileViewPage = new ProfileViewPage()
            {
                BindingContext = new ProfileViewViewModel(e.TappedPlayer)
            };
            CurrentProfileView = profileViewPage;
        }

        public void OnAppearing()
        {
            _friendsListViewModel.OnAppearing();
        }

        public void OnDisappearing()
        {
            
        }

        private FriendsListViewModel _friendsListViewModel;
        public FriendsListViewModel FriendsListViewModel
        {
            get => _friendsListViewModel;
            set { SetProperty(ref _friendsListViewModel, value); }
        }

        private ProfileViewPage _CurrentProfileView;
        public ProfileViewPage CurrentProfileView
        {
            get => _CurrentProfileView;
            set { SetProperty(ref _CurrentProfileView, value); }
        }
    }
}

