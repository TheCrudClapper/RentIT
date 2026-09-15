using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RentIT.UI.Core.ResultTypes;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RentIT.UI.Infrastructure.HttpClients.Base;
public static class ApiErrors
{
    public static Error UnknownError
        => Error.Create("ApiError", "Unknown api error happened.");

    public static Error ForbiddenResource
        => Error.Create("ForbiddenResource", "Access to this resource is forbidden.");

    public static Error PayloadIsEmpty
        => Error.Create("PayloadEmpty", "Payload is empty.");
}


public abstract class HttpClientBase
{
    protected readonly HttpClient _httpClient;
    private readonly IConfiguration _config;
    private readonly JsonSerializerOptions _jsonOptions;
    protected HttpClientBase(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
        _httpClient.BaseAddress = new Uri(_config.GetValue<string>("DefaultApiUrl")
            ?? throw new ArgumentNullException("Default api url is not defined"));

        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

    }

    protected async Task<Result<T>> GetAsync<T>(string resource)
    {
        try
        {
            var response = await _httpClient.GetAsync(resource);
            return await HandleResponseAsync<T>(response);
        }
        catch(Exception ex) 
        {
            return Result.Failure<T>(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result> GetAsync(string resource)
    {
        try
        {
            var response = await _httpClient.GetAsync(resource);
            return await HandleResponseAsync(response);
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result> DeleteAsync(string resource)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(resource);
            return await HandleResponseAsync(response);
        }
        catch(Exception ex)
        {
            return Result.Failure(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result<T>> DeleteAsync<T>(string resource)
    {
        try
        {
            var response = await _httpClient.DeleteAsync(resource);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<T>(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result<T>> PostAsync<T>(string resource, object payload)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(resource, payload, _jsonOptions);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<T>(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result> PostAsync(string resource, object payload)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(resource, payload, _jsonOptions);
            return await HandleResponseAsync(response);
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result<T>> PutAsync<T>(string resource, object payload)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(resource, payload, _jsonOptions);
            return await HandleResponseAsync<T>(response);
        }
        catch (Exception ex)
        {
            return Result.Failure<T>(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    protected async Task<Result> PutAsync(string resource, object payload)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(resource, payload, _jsonOptions);
            return await HandleResponseAsync(response);
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Create("RequestFailed", $"Request failed: {ex.Message}"));
        }
    }

    private async Task<Result> HandleResponseAsync(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return Result.Success();

        Error error = await ExtractErrorAsync(response);
        return Result.Failure(error);
    }

    private async Task<Result<T>> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            try
            {
                T? payload = await response.Content.ReadFromJsonAsync<T>(_jsonOptions);
                if (payload is null)
                    return Result.Failure<T>(ApiErrors.PayloadIsEmpty);

                return Result.Success<T>(payload);
            }
            catch (Exception ex)
            {
                return Result.Failure<T>(Error.Create("InvalidJson", $"Invalid JSON: {ex.Message}"));
            }
        }

        Error error = await ExtractErrorAsync(response);
        return Result.Failure<T>(error);
    }

    private async Task<Error> ExtractErrorAsync(HttpResponseMessage response)
    {
        try
        {
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return ApiErrors.ForbiddenResource;

            var details = await response.Content.ReadFromJsonAsync<ProblemDetails>(_jsonOptions);
            if (details is null)
                return ApiErrors.PayloadIsEmpty;

            return new Error(details.Title, details.Detail);
        }
        catch(Exception ex)
        {
            return ApiErrors.UnknownError;
        }
    }
}

