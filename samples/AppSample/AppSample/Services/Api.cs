using IdentityModel.OidcClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppSample.Services
{
    public class Api
    {
        public OidcClient OidcClient { get; private set; }

        public HttpClient HttpClient { get; private set; }

        public Api()
        {
            OidcClient = new OidcClient(GetOidcOptions());
            HttpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://nudesidentity.azurewebsites.net/api/")
            };
        }

        OidcClientOptions GetOidcOptions() => new OidcClientOptions
        {
            Authority = "https://nudesidentity.azurewebsites.net",
            ClientId = "app_code",
            Scope = "profile openid api1 offline_access",
            RedirectUri = "myapp://auth_callback",
            Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
            ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
            Browser = new Browser()
        };

        public Task Auth(CancellationToken cancellationToken =  default)
        {
            return OidcClient.LoginAsync(cancellationToken: cancellationToken).ContinueWith(loginResultTask =>
            {
                var loginResult = loginResultTask.Result;
                Settings.AccessToken = loginResult.AccessToken;
                Settings.AccessTokenExpiration = loginResult.AccessTokenExpiration;
                Settings.RefreshToken = loginResult.RefreshToken;
            });
        }

        
        public async Task<string> WeatherForecast()
        {
            if (Settings.AccessTokenExpiration - DateTime.Now < TimeSpan.FromMinutes(1) && !String.IsNullOrWhiteSpace(Settings.RefreshToken))
            {
                var refreshResult = await OidcClient.RefreshTokenAsync(Settings.RefreshToken);
                if (refreshResult.IsError)
                {
                    Settings.AccessToken = null;
                    Settings.AccessTokenExpiration = DateTime.Now;
                    Settings.RefreshToken = null;
                }
                else
                {
                    Settings.AccessToken = refreshResult.AccessToken;
                    Settings.AccessTokenExpiration = refreshResult.AccessTokenExpiration;
                    Settings.RefreshToken = refreshResult.RefreshToken;
                }
            }
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            var result = await HttpClient.GetAsync("WeatherForecast");
            var content = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
                return content;
            else
                return $"Status Code: {result.StatusCode}:\n{content}";
        }
    }
}
