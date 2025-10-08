namespace RentalService.Core.DTO.RentalDto;

public record ReturnEquipmentRequest(
    Guid RentalId,
    DateTime ReturnedDate);
