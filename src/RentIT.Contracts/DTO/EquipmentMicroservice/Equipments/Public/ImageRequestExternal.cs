using Microsoft.AspNetCore.Http;

namespace RentIT.Contracts.DTO.EquipmentMicroservice.Equipments.Public;

public record ImageRequestExternal(Guid? Id, bool IsCover, IFormFile Image);