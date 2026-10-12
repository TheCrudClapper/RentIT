namespace RentIT.Contracts.DTO.ReviewMicroservice.ReviewAllowances;

public record ReviewAllowanceResponse(Guid UserId, Guid EquipmentId, Guid RentalId);

