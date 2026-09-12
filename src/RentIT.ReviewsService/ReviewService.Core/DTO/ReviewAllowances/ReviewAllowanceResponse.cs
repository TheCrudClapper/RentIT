namespace ReviewService.Core.DTO.ReviewAllowances;

public record ReviewAllowanceResponse(Guid UserId, Guid EquipmentId, Guid RentalId);
