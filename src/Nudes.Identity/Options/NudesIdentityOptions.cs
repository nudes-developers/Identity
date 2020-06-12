using Nudes.Identity.Features.Users;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nudes.Identity.Options
{
    public class NudesIdentityOptions
    {
        public AccountOptions Account { get; set; } = new AccountOptions();
        public ConsentOptions Consent { get; set; } = new ConsentOptions();

        public LayoutOptions Layout { get; set; } = new LayoutOptions();
    }
}
