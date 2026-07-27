using Brawndo_Components.Extensions;
using Microsoft.AspNetCore.Localization;

namespace Brawndo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddLocalization();

            // Registers the School connection, repositories, and services in one call.
            builder.Services.AddBrawndoComponents(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Configure localization
            var supportedCultures = new[] { "en", "fr-CA" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            localizationOptions.RequestCultureProviders.Clear();
            localizationOptions.RequestCultureProviders.Add(new CustomRouteDataRequestCultureProvider());

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{language=en}/{controller=Home}/{action=Index}/{id?}",
                defaults: new { language = "en" });

            // Redirect root to default language
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/" || context.Request.Path.Value == "")
                {
                    context.Response.Redirect("/en/");
                    return;
                }
                await next();
            });

            app.Run();
        }
    }

    public class CustomRouteDataRequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var path = httpContext?.Request.Path.Value ?? "";
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length > 0)
            {
                var language = segments[0];
                if (language == "en" || language == "fr")
                {
                    var culture = language == "fr" ? "fr-CA" : "en";
                    return Task.FromResult<ProviderCultureResult?>(new ProviderCultureResult(culture, culture));
                }
            }

            return Task.FromResult<ProviderCultureResult?>(null);
        }
    }
}
