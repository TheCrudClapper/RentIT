using Microsoft.Extensions.Configuration;

namespace RentIT.UI.Infrastructure.HttpClients.Base;

public abstract class HttpClientBase
{
    protected readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    protected HttpClientBase(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _httpClient.BaseAddress = new Uri(_config.GetValue<string>("DefaultApiUrl")
            ?? throw new ArgumentNullException("Default api url is not defined"));
    }
}
