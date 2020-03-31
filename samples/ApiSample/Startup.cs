using IdentityServer4;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nudes.Identity;
using System;
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


        const string client_uri = "http://192.168.137.105";
        const string client_port = "5003";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(op =>
            {
                op.IssuerUri = "http://192.168.137.1:5000";
            }).AddInMemoryApiResources(new List<ApiResource>()
            {
                new ApiResource("api1", "My API #1")
            }).AddInMemoryIdentityResources(new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            }).AddInMemoryClients(new List<Client>()
            {
                new Client
                {
                    ClientId = "revisor_code",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = true,
                    RedirectUris =           { $"{client_uri}:{client_port}/callback" },
                    PostLogoutRedirectUris = { $"{client_uri}:{client_port}" },
                    AllowedCorsOrigins =     { $"{client_uri}:{client_port}" },
                    AccessTokenLifetime= 120,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    }
                },
            }).AddDeveloperSigningCredential();

            services
                .AddAuthentication(conf =>
                {
                    conf.DefaultScheme = "Bearer";
                })
                .AddJwtBearer("Bearer", op =>
                {
                    op.Authority = "http://192.168.137.1:5000";
                    op.Audience = "api1";
                    op.RequireHttpsMetadata = false;
                    op.TokenValidationParameters.ValidateLifetime = true;
                    op.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
                });

            services.AddControllersWithViews()
                .AddNudesIdentity<NudesIdentityUserStorage>();

            services.AddDbContext<Db>(d => d.UseInMemoryDatabase("nudes"));

            /* 
             * or using this if you are using Aspnetcore Identity (requires de Nudes.Identity.AspnetCoreIdentity package
               services.AddControllersWithViews()
                .AddNudesAspnetCoreIdentity<User>();
            */

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddMediatR(this.GetType().Assembly);

            services.AddCors(s => s.AddPolicy("corsPolicy", d =>
            {
                d.WithOrigins($"{client_uri}:{client_port}")
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
