using System;

namespace Nudes.Identity.Options
{
    public class NudesIdentityOptions
    {
        public const string NudesIdenitySchema = "NudesIdentity";

        public AccountOptions Account { get; set; } = new AccountOptions();
        public ConsentOptions Consent { get; set; } = new ConsentOptions();
    }
}
