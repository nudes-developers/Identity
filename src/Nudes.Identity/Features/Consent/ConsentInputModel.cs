using System.Collections.Generic;

namespace Nudes.Identity
{
    public class ConsentInputModel
    {
        public string Button { get; set; }

        public string Description { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
    }
}