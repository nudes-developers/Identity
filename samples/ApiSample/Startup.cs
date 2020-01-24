using IdentityServer4;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudes.Identity;
using Nudes.Identity.Options;
using System.Collections.Generic;

namespace ApiSample
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
                            "api1"
                        }
                    },
                })
                .AddDeveloperSigningCredential();

            services
                .AddAuthentication("Bearer")
                .AddCookie(NudesIdentityOptions.NudesIdenitySchema)
                .AddJwtBearer(op =>
                {
                    op.Authority = "http://localhost:5000";
                    op.Audience = "api1";
                    op.RequireHttpsMetadata = false;
                });

            services.AddControllersWithViews()
                .AddNudesIdentity(options => options.Account.ShowLogoutPrompt = false);


            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddMediatR(this.GetType().Assembly);

            //services.Configure<NudesIdentityOptions>(d => d.ShowLogoutPrompt = false);

            services.AddCors(s => s.AddPolicy("corsPolicy", d =>
            {
                d.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            }));

           
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

            app.UseCors("corsPolicy");

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            //app.UseMiddleware<SecurityHeadersMiddleware>();

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
