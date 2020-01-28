using Nudes.Identity.Features.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApiSample
{
    public class NudesIdentityUserStorage : INudesIdentityUserStorage
    {
        public Task<UserResult> AutoProvision(string provider, string providerUserId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<UserResult> ExternalProvider(string provider, string userId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<UserResult> ValidateUserCredentials(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username == "bob" && password == "bob")
            {
                return Task.FromResult(new UserResult()
                {
                    SubjectId = Guid.NewGuid().ToString(),
                    Username = "bob",
                    IsActive = true,
                });
            }
            return Task.FromResult((UserResult)null);
        }
    }
}
