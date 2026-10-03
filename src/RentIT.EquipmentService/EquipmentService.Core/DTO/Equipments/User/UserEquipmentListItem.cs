namespace EquipmentService.Core.DTO.Equipments.User;

public record UserEquipmentListItem
    (Guid Id,
    string Name,
    int Quantity,
    string CategoryName,
    DateTime DateCreated);
