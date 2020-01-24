using MediatR;
using Nudes.Identity.Features.Users;

namespace Nudes.Identity.Features.Users
{
    /// <summary>
    /// Queries for a valid user for the specified credentials
    /// </summary>
    public class ValidateUserCredentialsQuery : IRequest<UserResult>
    {
        public ValidateUserCredentialsQuery() { /* as is */ }

        public ValidateUserCredentialsQuery(string username, string password)
        {
            Username = username;
            Password = password;
        }


        /// <summary>
        /// User access identifier, can be username or password dependent on how you manage access
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// User password, should be hashed and compared to be sure that the actor should have access
        /// </summary>
        public string Password { get; set; }
    }
}
