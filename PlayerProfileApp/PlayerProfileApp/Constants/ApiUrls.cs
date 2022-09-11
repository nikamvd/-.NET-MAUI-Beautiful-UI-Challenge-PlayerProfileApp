using System;
namespace PlayerProfileApp.Constants
{
    public static class ApiUrls
    {
        /// <summary>
        /// Use this base address to retrieve json data from cloud
        /// </summary>
        public const string BaseAddress = "use your own cloud base address to store the json data";
        public const string FriendsUrl = @"/golf/friends.json";
    }

    public static class Config
    {
        public static bool Desktop
        {
            get
            {
#if WINDOWS || MACCATALYST
            return true;
#else
                return false;
#endif
            }
        }
    }
}

