namespace EquipmentService.Core.DTO.Equipments;

public record UserEquipmentListResponse(Guid Id, string Name, string SerialNumber, decimal RentalPricePerDay);
