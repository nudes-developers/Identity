using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Nudes.Identity.Features.Users
{
    public interface INudesIdentityUserStorage
    {
        Task<UserResult> ValidateUserCredentials(string username, string password, CancellationToken cancellationToken = default);

        Task<UserResult> ExternalProvider(string provider, string userId, CancellationToken cancellationToken = default);

        Task<UserResult> AutoProvision(string provider, string providerUserId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);
    }
}
