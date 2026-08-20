using Microsoft.AspNetCore.Authentication.Cookies;
using RentIT.BlazorFrontend.Components;
using RentIT.BlazorFrontend.Extensions;
using RentIT.BlazorFrontend.Handlers;
using RentIT.UI.Core.Extensions;
using RentIT.UI.Infrastructure.Extensions;

namespace RentIT.BlazorFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.Cookie.Name = "RentIt.Auth";
                    options.AccessDeniedPath = "/access-denied";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                });

            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddBearerTokenHandler();

            builder.Services
                .AddUILayer()
                .AddCoreLayer()
                .AddInfrastructureLayer()
                .AddValidation();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
