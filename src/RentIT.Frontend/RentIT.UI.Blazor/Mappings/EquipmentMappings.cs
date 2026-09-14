using EquipmentService.Core.DTO.Equipments;
using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.UI.Core.DTO.Equipments;

namespace RentIT.BlazorFrontend.Mappings;

public static class EquipmentMappings
{
    public static EquipmentModel ToModel(this EquipmentResponse response)
        => new()
        {
            CategoryId = response.CategoryId,
            Name = response.Name,
            Notes = response.Notes,
            RentalPricePerDay = response.RentalPricePerDay,
            SerialNumber = response.SerialNumber,
            Status = response.Status
        };

    public static EquipmentAddRequest ToAddRequest(this EquipmentModel model)
        => new()
        {
            CategoryId = model.CategoryId,
            Name = model.Name,
            Notes = model.Notes,
            RentalPricePerDay = model.RentalPricePerDay,
            SerialNumber = model.SerialNumber,
            Status = model.Status
        };

    public static EquipmentUpdateRequest ToUpdateRequest(this EquipmentModel model)
        => new()
        {
            CategoryId = model.CategoryId,
            Name = model.Name,
            Notes = model.Notes,
            RentalPricePerDay = model.RentalPricePerDay,
            SerialNumber = model.SerialNumber,
            Status = model.Status
        };
}
