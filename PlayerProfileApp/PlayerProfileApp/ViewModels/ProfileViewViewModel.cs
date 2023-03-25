using System;
using PlayerProfileApp.Base;
using PlayerProfileApp.Models;
using PlayerProfileApp.Resources;
using System.Windows.Input;
using PlayerProfileApp.Constants;

namespace PlayerProfileApp.ViewModels
{
    public class ProfileViewViewModel : ExtendedBindableObject, ILazyNavigator
    {
        public ProfileViewViewModel(Friend selectedPlayer, List<Friend> players)
        {
            Players = players;
            SelectedPlayer = selectedPlayer;
            BackCommand = new Command(async () => await NavigateBack());
        }

        private List<Friend> _players;
        public List<Friend> Players
        {
            get => _players;
            set { SetProperty(ref _players, value); }
        }

        private Friend _selectedPlayer;
        public Friend SelectedPlayer
        {
            get => _selectedPlayer;
            set { SetProperty(ref _selectedPlayer, value); }
        }

        public string ProfileMessage
        {
            get
            {
                return string.Format(GolfMateResources.ProfileMessage, SelectedPlayer.FirstName);
            }
        }

        public bool IsDesktop
        {
            get
            {
                return Config.Desktop;
            }
        }

        public INavigation Navigation { get; set; }

        private ICommand _backCommand;
        public ICommand BackCommand
        {
            get => _backCommand;
            set { SetProperty(ref _backCommand, value); }
        }

        private async Task NavigateBack()
        {
            await Navigation?.PopAsync(false);
        }
    }
}

