using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;

namespace Nudes.Identity
{
    public static class PublicExtensions
    {
        public static IMvcBuilder AddNudesIdentity(this IMvcBuilder builder)
        {
            builder.Services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
                o.PageViewLocationFormats.Add("/Features/{1}/{0}.cshtml");

                o.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
                o.PageViewLocationFormats.Add("/Features/Shared/{0}.cshtml");
            });
            //.ConfigureOptions<UIConfigureOptions>();

            return builder.AddApplicationPart(typeof(PublicExtensions).Assembly);

        }
    }

    public class UIConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        public UIConfigureOptions(IHostEnvironment env)
        {
            Env = env;
        }

        public IHostEnvironment Env { get; }

        public void PostConfigure(string name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Env.ContentRootFileProvider == null)
                throw new InvalidOperationException("Missing FileProvider.");

            options.FileProvider = options.FileProvider ?? Env.ContentRootFileProvider;

            var basePath = "wwwroot";

            var filesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, basePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, filesProvider);
        }
    }
}
