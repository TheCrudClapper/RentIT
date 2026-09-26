using Microsoft.Extensions.Configuration;
using RentIT.UI.Contracts.DTO.Shared;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;

namespace RentIT.UI.Infrastructure.HttpClients;

public class CategoriesHttpClient : HttpClientBase, ICategoriesHttpClient
{
    public CategoriesHttpClient(HttpClient httpClient, IConfiguration config) : base(httpClient, config) { }

    public async Task<Result<IReadOnlyCollection<SelectItem>>> GetCategories()
        => await GetAsync<IReadOnlyCollection<SelectItem>>("categories");
}
