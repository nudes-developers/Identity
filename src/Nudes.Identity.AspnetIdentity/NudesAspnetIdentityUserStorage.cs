using Microsoft.AspNetCore.Identity;
using Nudes.Identity.Features.Users;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Nudes.Identity.AspnetCoreIdentity
{
    public class NudesAspnetIdentityUserStorage<TUser> : INudesIdentityUserStorage where TUser : class
    {
        private readonly UserManager<TUser> userManager;

        public NudesAspnetIdentityUserStorage(UserManager<TUser> userManager)
        {
            this.userManager = userManager;
        }

        public Task<UserResult> AutoProvisionUser(string provider, string providerUserId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<UserResult> FindByExternalProvider(string provider, string userId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public async Task<UserResult> ValidateUserCredentials(string username, string password, CancellationToken cancellationToken = default)
        {
            var user = await userManager.FindByNameAsync(username);
            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserResult()
                {
                    Username = await userManager.GetUserNameAsync(user),
                    Claims = await userManager.GetClaimsAsync(user),
                    IsActive = true,
                    SubjectId = await userManager.GetUserIdAsync(user),
                };
            }
            else
                return null;
        }


        public Task GenerateResetPasswordTokenFor(string username, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public Task<bool> IsResetPasswordTokenValid(string token, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public Task ConsumeResetPasswordToken(string token, string newPassword, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }
}
