using IdentityModel.OidcClient.Browser;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppSample.Services
{
    public class Browser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            WebAuthenticatorResult authResult = await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri("myapp://auth_callback"));
            
            return new BrowserResult()
            {
                Response = Parse(authResult)
            };
        }

        private string Parse(WebAuthenticatorResult result)
        {
            string code = result?.Properties["code"];
            string scope = result?.Properties["scope"];
            string state = result?.Properties["state"];
            string sessionState = result?.Properties["session_state"];
            return $"myapp://auth_callback#code={code}&scope={scope}&state={state}&session_state={sessionState}";
        }
    }
}
