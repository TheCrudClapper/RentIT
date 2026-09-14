using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.DTO.Shared;
using RentIT.UI.Core.HttpClientContracts;
using RentIT.UI.Core.ResultTypes;
using RentIT.UI.Infrastructure.HttpClients.Base;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace RentIT.UI.Infrastructure.HttpClients;

public class CategoriesHttpClient : HttpClientBase, ICategoriesHttpClient
{
    public CategoriesHttpClient(HttpClient httpClient, IConfiguration config) : base(httpClient, config)
    {
    }

    public async Task<Result<IReadOnlyCollection<SelectItem>>> GetCategories()
    {
        var response = await _httpClient.GetAsync($"categories");

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails is null)
                return Result.Failure<IReadOnlyCollection<SelectItem>>(Error.Create("Failed", "Something went wrong."));

            return Result.Failure<IReadOnlyCollection<SelectItem>>(Error.Create(problemDetails.Title, problemDetails.Detail));
        }

        var categories = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<SelectItem>>();
        if (categories is null)
            return Result.Failure<IReadOnlyCollection<SelectItem>>(Error.Create("Failed", "Equipment was not recieved."));

        return Result.Success(categories);
    }
}
