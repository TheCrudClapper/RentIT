namespace EquipmentService.Core.DTO.Equipments.User;

public record UserEquipmentListResponse
    (Guid Id,
    string Name,
    int Quantity,
    string CategoryName,
    DateTime DateCreated);
