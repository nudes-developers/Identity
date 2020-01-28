using System.Collections.Generic;
using System.Security.Claims;

namespace Nudes.Identity.Features.Users
{
    public class AutoProvisionUserCommand
    {
        public AutoProvisionUserCommand()
        {
        }

        public AutoProvisionUserCommand(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            Provider = provider;
            ProviderUserId = providerUserId;
            Claims = claims;
        }

        public string Provider { get; }
        public string ProviderUserId { get; }
        public IEnumerable<Claim> Claims { get; }
    }
}
