using Microsoft.Extensions.DependencyInjection;
using Nudes.Identity.AspnetCoreIdentity;
using Nudes.Identity.Options;
using System;

namespace Nudes.Identity
{
    public static class Extension
    {

        /// <summary>
        /// Adds all necessary services and includes the controllers of this assembly
        /// Also configures RazorViewEngineOptions to include our views and
        /// configures IdentityServerOptions to use our Cookie AuthenticationSchema
        /// And by default uses a version of UserManager to store and retrieve users
        /// </summary>
        public static IMvcBuilder AddNudesAspnetCoreIdentity<TUser>(this IMvcBuilder builder) where TUser : class
        {
            return builder.AddNudesIdentity<NudesAspnetIdentityUserStorage<TUser>>();
        }

        /// <summary>
        /// Adds all necessary services and includes the controllers of this assembly
        /// Also configures RazorViewEngineOptions to include our views and
        /// configures IdentityServerOptions to use our Cookie AuthenticationSchema
        /// And by default uses a version of UserManager to store and retrieve users
        /// </summary>
        public static IMvcBuilder AddNudesAspnetCoreIdentity<TUser>(this IMvcBuilder builder, Action<NudesIdentityOptions> options) where TUser : class
        {
            return builder.AddNudesIdentity<NudesAspnetIdentityUserStorage<TUser>>(options);
        }
    }
}
