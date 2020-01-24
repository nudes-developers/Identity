using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Nudes.Identity.Options;
using System;

namespace Nudes.Identity
{
    public static class PublicExtensions
    {
        /// <summary>
        /// Adds all necessary services and includes the controllers of this assembly
        /// Also configures RazorViewEngineOptions to include our views and
        /// configures IdentityServerOptions to use our Cookie AuthenticationSchema
        /// </summary>
        public static IMvcBuilder AddNudesIdentity(this IMvcBuilder builder)
        {
            ConfigureServices(builder.Services);

            return builder.AddApplicationPart(typeof(PublicExtensions).Assembly);
        }

        /// <summary>
        /// Adds all necessary services and includes the controllers of this assembly
        /// Also configures RazorViewEngineOptions to include our views and
        /// configures IdentityServerOptions to use our Cookie AuthenticationSchema
        /// </summary>
        /// <param name="options">Action to make changes on NudesIdentityOption</param>
        public static IMvcBuilder AddNudesIdentity(this IMvcBuilder builder, Action<NudesIdentityOptions> options)
        {
            ConfigureServices(builder.Services);

            builder.Services.Configure(options);

            return builder.AddApplicationPart(typeof(PublicExtensions).Assembly);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configure view location to search for razor views and pages
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
                o.PageViewLocationFormats.Add("/Features/{1}/{0}.cshtml");

                o.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                o.PageViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
            });

            // Configure IdentityServer to use our cookie authentication schema
            services.Configure<IdentityServerOptions>(setup => setup.Authentication.CookieAuthenticationScheme = NudesIdentityOptions.NudesIdenitySchema);

            // Add IdentityOptions and give possibility to configure
            services.AddOptions<NudesIdentityOptions>();
        }
    }

}
