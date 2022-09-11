using System;
namespace PlayerProfileApp.Models
{
    public class Friend
    {
        public string Name => $"{FirstName} {LastName}";
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string NickName { get; init; } = string.Empty;
        public DateTime DateOfBirth { get; init; }
        public string ProfilePictureUrl { get; init; } = string.Empty;
        public bool IsFriend { get; init; }
        public long MajorsWon { get; init; }
        public string Age => $"{DateTime.Now.AddYears(-DateOfBirth.Year).Year} yrs";

        public Friend Clone()
        {
            return (Friend)this.MemberwiseClone();
        }
    }

    public class FriendsCollection
    {
        public List<Friend> Friends { get; set; }
    }

    public class PlayerGroup : List<Friend>
    {
        public string GroupName { get; private set; }

        public PlayerGroup(string name, List<Friend> players) : base(players)
        {
            GroupName = name;
        }
    }
}

