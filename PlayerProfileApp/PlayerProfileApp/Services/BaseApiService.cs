using System;
using System.Net.Http.Headers;

namespace PlayerProfileApp.Services
{
    public interface IHttpClientProvider
    {
        HttpClient Client { get; }
    }

    public class BaseApiService
    {
        protected HttpClient _httpClient;

        protected void Initialize(IHttpClientProvider httpClientProvider)
        {
            _httpClient = httpClientProvider.Client;
        }

        protected virtual HttpRequestMessage CreateHttpGetRequestMessageAsync()
        {
            var result = new HttpRequestMessage();
            result.Method = new HttpMethod("GET");
            result.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
            return result;
        }
    }
}

