namespace ReviewService.Core.DTO.ReviewAllowance;

public record ReviewAllowanceResponse(Guid UserId, Guid EquipmentId, Guid RentalId);
