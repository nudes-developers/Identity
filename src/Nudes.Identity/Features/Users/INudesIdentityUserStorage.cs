using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Nudes.Identity.Features.Users
{
    public interface INudesIdentityUserStorage
    {
        Task<UserResult> ValidateUserCredentials(string username, string password, Client client, CancellationToken cancellationToken = default);

        Task<UserResult> FindByExternalProvider(string provider, string userId, CancellationToken cancellationToken = default);

        Task<UserResult> AutoProvisionUser(string provider, string providerUserId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);

        Task GenerateResetPasswordTokenFor(string username, CancellationToken cancellationToken = default);

        Task<bool> IsResetPasswordTokenValid(string token, CancellationToken cancellationToken = default);

        Task ConsumeResetPasswordToken(string token, string newPassword, CancellationToken cancellationToken = default);
    }
}
