namespace RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;

public record UserEquipmentListResponse
    (Guid Id,
    string Name,
    int Quantity,
    string CategoryName,
    DateTime DateCreated);
