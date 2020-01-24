using MediatR;

namespace Nudes.Identity.Features.Users
{
    /// <summary>
    /// Query for an user using the Provider and UserId
    /// </summary>
    public class ExternalProviderUserQuery : IRequest<UserResult>
    {
        public ExternalProviderUserQuery() { /* as is */ }

        public ExternalProviderUserQuery(string provider, string userId)
        {
            Provider = provider;
            UserId = userId;
        }

        public string Provider { get; set; }

        public string UserId { get; set; }
    }
}
