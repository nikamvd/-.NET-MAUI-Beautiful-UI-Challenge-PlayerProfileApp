using System;
using System.Text;
using PlayerProfileApp.Constants;
using PlayerProfileApp.Models;
using PlayerProfileApp.Services;
using Newtonsoft.Json;

namespace PlayerProfileApp.Services
{
    public interface IFriendsService
    {
        Task<IEnumerable<Friend>> GetFriends(CancellationToken cancellationToken = default);
        Task<IEnumerable<Friend>> GetFriendsFromMauiAsset();
    }

    public class FriendsService : BaseApiService, IFriendsService
    {
        public FriendsService(IHttpClientProvider httpClientProvider)
        {
            Initialize(httpClientProvider);
        }

        /// <summary>
        /// Get the friends list from cloud
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Friend>> GetFriends(CancellationToken cancellationToken = default)
        {
            try
            {
                var request = CreateHttpGetRequestMessageAsync();
                var urlBuilder = new StringBuilder();
                urlBuilder.Append(ApiUrls.BaseAddress != null ? ApiUrls.BaseAddress.TrimEnd('/') : "").Append(ApiUrls.FriendsUrl);

                request.RequestUri = new Uri(urlBuilder.ToString(), UriKind.RelativeOrAbsolute);
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var friendsCollection = JsonConvert.DeserializeObject<FriendsCollection>(responseData);
                    return friendsCollection.Friends;
                }
                else
                {
                    throw new Exception("Fatal Error Occured while retrieving friends data");
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Fatal Error Occured while retrieving friends data", innerException: exception);
            }
        }

        /// <summary>
        /// Get the friends list from MAUI assets
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Friend>> GetFriendsFromMauiAsset()
        {
            try
            {
                var assetData = await LoadFriendsDataFromMauiAsset();
                if(string.IsNullOrEmpty(assetData))
                {
                    return new List<Friend>();
                }
                var friendsCollection = JsonConvert.DeserializeObject<FriendsCollection>(assetData);
                return friendsCollection.Friends;
            }
            catch (Exception exception)
            {
                throw new Exception("Fatal Error Occured while retrieving friends data", innerException: exception);
            }
        }

        private async Task<string> LoadFriendsDataFromMauiAsset()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("friends.json");
                using var reader = new StreamReader(stream);

                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}

