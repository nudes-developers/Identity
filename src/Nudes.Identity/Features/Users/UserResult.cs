using System.Collections.Generic;
using System.Security.Claims;

namespace Nudes.Identity.Features.Users
{
    /// <summary>
    /// An representation of the platform's user that will be used to sign and deal with all authentication data
    /// </summary>
    public class UserResult
    {
        /// <summary>
        /// User's identifier
        /// </summary>
        public string SubjectId { get; set; }
        /// <summary>
        /// User's identifier credential
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Provider name
        /// </summary>
        public string ProviderName { get; set; }
        /// <summary>
        /// Provider identifier
        /// </summary>
        public string ProviderSubjectId { get; set; }
        /// <summary>
        /// Weather the user is active or not
        /// </summary>
        public bool IsActive { get; set; }
        //
        // Summary:
        //     Gets or sets the claims.
        public ICollection<Claim> Claims { get; set; }
    }
}
