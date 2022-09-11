using System;
using PlayerProfileApp.Base;
using PlayerProfileApp.Models;
using PlayerProfileApp.Resources;
using System.Windows.Input;

namespace PlayerProfileApp.ViewModels
{
    public class ProfileViewViewModel : ExtendedBindableObject, ILazyNavigator
    {
        public ProfileViewViewModel(Friend selectedPlayer)
        {
            SelectedPlayer = selectedPlayer;
            BackCommand = new Command(async () => await NavigateBack());
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

