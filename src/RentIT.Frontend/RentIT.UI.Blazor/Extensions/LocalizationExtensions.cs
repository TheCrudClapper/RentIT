namespace RentIT.BlazorFrontend.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddLocalizationSupport(this IServiceCollection services)
    {
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        return services;
    }

    public static WebApplication UseLocalization(this WebApplication app)
    {
        var supportedCultures = new[]
        {
                "pl",
                "en"
        };

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("pl")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);
        return app;
    }
}
