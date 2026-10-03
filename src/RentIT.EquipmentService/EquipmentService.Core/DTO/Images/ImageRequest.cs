namespace EquipmentService.Core.DTO.Images;

public record ImageRequest(Guid? Id, bool IsCover, byte[] Image);
