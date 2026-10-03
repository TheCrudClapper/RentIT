namespace EquipmentService.API.DTO.Public;

public record ImageRequestExternal(Guid? Id, bool IsCover, IFormFile Image);