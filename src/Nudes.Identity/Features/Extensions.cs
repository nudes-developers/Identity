using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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

            return context.SignInAsync(NudesIdentityOptions.NudesIdenitySchema, identityUser.CreatePrincipal(), properties);
        }

        /// <summary>
        /// Internal signout method that signouts the user for the NudesIdentity schema
        /// </summary>
        public static Task SignOutFromIdentity(this HttpContext context) => context.SignOutAsync(NudesIdentityOptions.NudesIdenitySchema);

    }
}
