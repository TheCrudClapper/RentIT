using EquipmentService.Core.Domain.HtppClientContracts;
using EquipmentService.Core.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System.Text.Json;

namespace EquipmentService.Infrastructure.HttpClients
{
    public class RentalMicroserviceClient : IRentalMicroserviceClient
    {
        private readonly HttpClient _httpClient;
        public RentalMicroserviceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Result> DeleteRentalsByEquipmentId(Guid equipmentId)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"/gateway/Rentals/byEquipmentId/{equipmentId}");

                if (!response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var probDetails = JsonSerializer.Deserialize<ProblemDetails>(content, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });

                        string errorMessage = probDetails?.Detail ?? "Unknow error happened";
                        return Result.Failure(new Error(probDetails.Status.Value, errorMessage));
                    }
                    catch (JsonException)
                    {
                        return Result.Failure(new Error(400, content));
                    }
                }


                return Result.Success();
            }
            catch (BrokenCircuitException)
            {
                return Result.Failure(new Error(503, "Service unavaliable, try again later"));
            }
            
;       }
    }
}
