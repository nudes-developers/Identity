using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudes.Identity;
using System.Collections.Generic;

namespace WebSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentityServer()
                .AddInMemoryApiResources(new List<ApiResource>()
                {
                    new ApiResource("api1", "My API #1")
                })
                .AddInMemoryIdentityResources(new List<IdentityResource>()
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                })
                .AddInMemoryClients(new List<Client>()
                {
                    new Client
                    {
                        ClientId = "client-admin",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        RequireConsent = false,
                        RedirectUris =           { "http://localhost:3000/callback.html" },
                        PostLogoutRedirectUris = { "http://localhost:3000/" },
                        AllowedCorsOrigins =     { "http://localhost:3000" },

                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                        }
                    }
                })
                .AddDeveloperSigningCredential();

            services.AddAuthentication()
                .AddJwtBearer(op =>
                {
                    op.Authority = "http://localhost:5000";
                    op.Audience = "api1";
                    op.RequireHttpsMetadata = false;
                });

            services.AddControllersWithViews()
                .AddNudesIdentity();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseIdentityServer();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            //app.UseSpa(spa =>
            //{
            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseReactDevelopmentServer(npmScript: "start");
            //    }
            //});
        }
    }
}
