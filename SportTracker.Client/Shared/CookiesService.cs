using Microsoft.JSInterop;
using SportTracker.Shared.Models;

// based on https://stackoverflow.com/a/69873060
namespace SportTracker.Client.Shared
{
    public interface ICookiesService
    {
        Task Set(Cookies key, string value, int? days = null);
        Task Delete(Cookies key);
        Task<string> Get(Cookies key, string fallback = "");
    }

    public class CookiesService(IJSRuntime jsRuntime) : ICookiesService
    {
        private readonly IJSRuntime _JSRuntime = jsRuntime;

        public async Task Set(Cookies key, string value, int? days = null)
        {
            var expiry = days != null ? DateToUTC(days.Value) : "";
            await SetCookieJs($"{key}={value}; expires={expiry}; path=/");
        }

        public async Task<string> Get(Cookies key, string fallback = "")
        {
            var cookieString = await GetCookieJs();
            if (string.IsNullOrEmpty(cookieString))
            {
                return fallback;
            }

            var allCookies = cookieString.Split(';');

            foreach (var cookie in allCookies)
            {
                if (!string.IsNullOrEmpty(cookie) && cookie.IndexOf('=') > 0)
                {
                    if (cookie[..cookie.IndexOf('=')].Trim().Equals(key.ToString()))
                    {
                        return cookie[(cookie.IndexOf('=') + 1)..];
                    }
                }
            }

            return fallback;
        }

        public async Task Delete(Cookies key)
        {
            var expiry = DateToUTC(-1); // expires yesterday
            await SetCookieJs($"{key}=; expires={expiry}; path=/");
        }

        private async Task SetCookieJs(string value)
        {
            await _JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
        }

        private async Task<string> GetCookieJs()
        {
            return await _JSRuntime.InvokeAsync<string>("eval", $"document.cookie");
        }

        private static string DateToUTC(int days) =>
            DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
    }
}
