using Microsoft.EntityFrameworkCore;
using Nudes.Identity.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ApiSample
{
    public class NudesIdentityUserStorage : INudesIdentityUserStorage
    {
        private readonly Db db;
        static readonly string bobId = "61fd80bf-2b94-4901-b90a-04c5fac5820b";
        static string bobPassword = "bob";
        public NudesIdentityUserStorage(Db db)
        {
            this.db = db;
        }

        public Task<UserResult> AutoProvision(string provider, string providerUserId, IEnumerable<Claim> claims, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<UserResult> ExternalProvider(string provider, string userId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public async Task GenerateResetPasswordTokenFor(string username, CancellationToken cancellationToken = default)
        {
            if (username == "bob")
            {
                var a = new Models.ResetPasswordToken
                {
                    UserId = Guid.Parse(bobId),
                };

                db.ResetPasswordTokens.Add(a);
                await db.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> IsResetPasswordTokenValid(string token, CancellationToken cancellationToken = default)
        {
            var resetPassword = await db.ResetPasswordTokens.FirstOrDefaultAsync(d => d.Id.ToString() == token);
            return resetPassword != null && !resetPassword.ConsumedAt.HasValue;
        }
        public async Task ConsumeResetPasswordToken(string token, string newPassword, CancellationToken cancellationToken = default)
        {
            var resetPassword = await db.ResetPasswordTokens.FirstOrDefaultAsync(d => d.Id.ToString() == token);
            if (resetPassword != null && !resetPassword.ConsumedAt.HasValue)
            {
                resetPassword.ConsumedAt = DateTime.Now;
                await db.SaveChangesAsync(cancellationToken);
                bobPassword = newPassword;
            }
        }

        public Task<UserResult> ValidateUserCredentials(string username, string password, CancellationToken cancellationToken = default)
        {
            if (username == "bob" && password == bobPassword)
            {
                return Task.FromResult(new UserResult()
                {
                    SubjectId = bobId.ToString(),
                    Username = "bob",
                    IsActive = true,
                });
            }
            return Task.FromResult((UserResult)null);
        }
    }
}
