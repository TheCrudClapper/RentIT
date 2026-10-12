namespace RentIT.Contracts.DTO.EquipmentMicroservice.Equipments.User;

public record UserEquipmentListItem
    (Guid Id,
    string Name,
    int Quantity,
    string CategoryName,
    DateTime DateCreated);

