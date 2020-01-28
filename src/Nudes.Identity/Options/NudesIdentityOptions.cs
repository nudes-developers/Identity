using Nudes.Identity.Features.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nudes.Identity.Options
{
    public class NudesIdentityOptions
    {
        public const string NudesIdenitySchema = "NudesIdentity";
        private Func<ValidateUserCredentialsQuery, CancellationToken, Task<UserResult>> validateUserCredentials;
        private Func<ExternalProviderUserQuery, CancellationToken, Task<UserResult>> externalProviderUser;
        private Func<AutoProvisionUserCommand, CancellationToken, Task<UserResult>> autoProvisionUser;

        public AccountOptions Account { get; set; } = new AccountOptions();
        public ConsentOptions Consent { get; set; } = new ConsentOptions();

        public Func<ValidateUserCredentialsQuery, CancellationToken, Task<UserResult>> ValidateUserCredentials
        {
            get
            {
                if (validateUserCredentials == null)
                    throw new Exception($"You must set the validate user credentials function using NudesIdentityOptions.{nameof(ValidateUserCredentials)}");
                return validateUserCredentials;
            }

            set => validateUserCredentials = value;
        }

        public Func<ExternalProviderUserQuery, CancellationToken, Task<UserResult>> ExternalProviderUser
        {
            get
            {
                if (externalProviderUser == null)
                    throw new Exception($"You must set the validate user credentials function using NudesIdentityOptions.{nameof(ExternalProviderUser)}");
                return externalProviderUser;
            }

            set => externalProviderUser = value;
        }

        public Func<AutoProvisionUserCommand, CancellationToken, Task<UserResult>> AutoProvisionUser
        {
            get
            {
                if (autoProvisionUser == null)
                    throw new Exception($"You must set the validate user credentials function using NudesIdentityOptions.{nameof(AutoProvisionUser)}");
                return autoProvisionUser;
            }

            set => autoProvisionUser = value;
        }
    }
}
