using System;
using PlayerProfileApp.Base;
using PlayerProfileApp.Models;
using PlayerProfileApp.Pages;
using System.Windows.Input;
using PlayerProfileApp.Services;
using PlayerProfileApp.Constants;

namespace PlayerProfileApp.ViewModels
{
    public class ProfileTappedEventArgs : EventArgs
    {
        public ProfileTappedEventArgs(Friend tappedPlayer)
        {
            TappedPlayer = tappedPlayer;
        }

        public Friend TappedPlayer { get; set; }
    }

    public class FriendsListViewModel : ExtendedBindableObject, ILazyNavigator, IMonitorAppearance
    {
        public List<Friend> Friends;
        private readonly IFriendsService _friendsService;
        public FriendsListViewModel(IFriendsService friendsService)
        {
            _friendsService = friendsService;
        }

        public event EventHandler<ProfileTappedEventArgs> ProfileTapped;

        public INavigation Navigation { get; set; }
        public ICommand FriendSelectedCommand => new Command<Friend>(async (Friend selectedPlayer) => await NavigateToProfileView(selectedPlayer));

        private Friend _selectedPlayer;
        public Friend SelectedPlayer
        {
            get => _selectedPlayer;
            set { SetProperty(ref _selectedPlayer, value); }
        }

        private async Task NavigateToProfileView(Friend selectedPlayer)
        {
            if (selectedPlayer == null)
            {
                return;
            }

            var clonedPlayer = selectedPlayer.Clone();
            if (Config.Desktop)
            {
                OnProfileTapped(new ProfileTappedEventArgs(clonedPlayer));
            }
            else
            {
                ProfileViewPage profileViewPage = new ProfileViewPage()
                {
                    BindingContext = new ProfileViewViewModel(clonedPlayer, Friends)
                };
                await Navigation.PushAsync(profileViewPage, false);
            }
            SelectedPlayer = null;
        }

        private List<PlayerGroup> _players;
        public List<PlayerGroup> Players
        {
            get => _players;
            set { SetProperty(ref _players, value); }
        }

        private async void InitializeFriendsList()
        {
            var players = await _friendsService.GetFriendsFromMauiAsset();

            var playersList = new List<PlayerGroup>();
            List<Friend> friends = players.Where(player => player.IsFriend).ToList();
            Friends = new List<Friend>();
            Friends.AddRange(friends);
            PlayerGroup friendsGroup = new PlayerGroup("Friends", friends);
            playersList.Add(friendsGroup);

            List<Friend> recentPlayers = players.Where(player => !player.IsFriend).ToList();
            Friends.AddRange(recentPlayers);
            PlayerGroup recentlyPlayedGroup = new PlayerGroup("Acquaintances", recentPlayers);
            playersList.Add(recentlyPlayedGroup);
            Players = playersList;

            if (Config.Desktop)
            {
                SelectedPlayer = friends.FirstOrDefault();
                //OnProfileTapped(new ProfileTappedEventArgs(SelectedPlayer));
            }
        }

        private void OnProfileTapped(ProfileTappedEventArgs e)
        {
            var handler = ProfileTapped;
            handler?.Invoke(this, e);
        }

        public void OnAppearing()
        {
            InitializeFriendsList();
        }

        public void OnDisappearing()
        {
            
        }
    }
}

