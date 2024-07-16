using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SportTracker.Client.Shared
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);
        Task Post(string uri, object value);
        Task<T> Post<T>(string uri, object value);
    }

    public class HttpService(HttpClient httpClient) : IHttpService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);
            return await SendRequest<T>(request);
        }

        public async Task Post(string uri, object value)
        {
            var request = CreateRequest(HttpMethod.Post, uri, value);
            await SendRequest(request);
        }

        private HttpRequestMessage CreateRequest(
            HttpMethod method,
            string uri,
            object? value = null
        )
        {
            var request = new HttpRequestMessage(method, uri);
            if (value != null)
            {
                request.Content = new StringContent(
                    JsonSerializer.Serialize(value),
                    Encoding.UTF8,
                    "application/json"
                );
            }
            return request;
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            // todo auth, logout if 401

            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error?["message"]);
            }

            // var options = new JsonSerializerOptions();
            // options.PropertyNameCaseInsensitive = true;
            // options.Converters.Add(new StringConverter());
            return await response.Content.ReadFromJsonAsync<T>();
        }

        private async Task SendRequest(HttpRequestMessage request)
        {
            // todo auth, logout if 401

            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                throw new Exception(error?["message"]);
            }
        }
    }
}
