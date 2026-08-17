using ReviewService.Core.Domain.HttpClientContracts;
using ReviewService.Core.Domain.ResultTypes;
using ReviewService.Core.DTO.Rental;
using System.Net.Http.Json;

namespace ReviewService.Infrastructure.HttpClients;

public class RentalMicroserviceClient : IRentalMicroserviceClient
{
    private readonly HttpClient _httpClient;
    public RentalMicroserviceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Result<RentalResponse>> GetRentalByRentalIdAsync(Guid rentalId, CancellationToken cancellationToken = default)
    {
        var rentalResponse = await _httpClient.GetAsync($"/gateway/Rentals/{rentalId}");

        if (!rentalResponse.IsSuccessStatusCode)
        {
            string message = await rentalResponse.Content.ReadAsStringAsync(cancellationToken);
            return Result.Failure<RentalResponse>(Error.Create(ErrorType.Unexpected, ((int)rentalResponse.StatusCode).ToString(), message));
        }

        RentalResponse? obj = await rentalResponse.Content.ReadFromJsonAsync<RentalResponse>(cancellationToken);
        if (obj is null)
            return Result.Failure<RentalResponse>(Error.Create(ErrorType.Unexpected, "500", "Invalid response from underlying microservice"));

        return obj;
    }
}
