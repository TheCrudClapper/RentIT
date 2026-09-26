using Microsoft.Extensions.Localization;
using RentIT.BlazorFrontend.ServiceContracts;

namespace RentIT.BlazorFrontend.Services.Localization;

public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer<LangResource> _localizer;
    public LocalizationService(IStringLocalizer<LangResource> localizer)
        => _localizer = localizer;

    public string GetLocalizedText(string key)
        => _localizer.GetString(key);

}
