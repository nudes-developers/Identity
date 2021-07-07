using System;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Nudes.Identity.Features.Users;
using Nudes.Identity.Options;

namespace Nudes.Identity
{
    public static class Extensions
    {
        /// <summary>
        /// Determines whether the client is configured to use PKCE.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="client_id">The client identifier.</param>
        /// <returns></returns>
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }

        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

        /// <summary>
        /// Internal signin method that generates the cookie authentication for an UserResult in the NudesIdentity schema
        /// </summary>
        internal static Task SignInAsync(this HttpContext context, UserResult user, AuthenticationProperties properties)
        {
            var clock = context.RequestServices.GetRequiredService<ISystemClock>();

            var identityUser = new IdentityServerUser(user.SubjectId)
            {
                DisplayName = user.Username,
                AuthenticationTime = clock.UtcNow.UtcDateTime,
            };

            if (user.Claims != null)
                identityUser.AdditionalClaims = user.Claims;

            return context.SignInAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme, identityUser.CreatePrincipal(), properties);
        }

        /// <summary>
        /// Internal signout method that signouts the user for the NudesIdentity schema
        /// </summary>
        public static Task SignOutFromIdentity(this HttpContext context) => context.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);


        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = "";

            return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
        }
    }
}
