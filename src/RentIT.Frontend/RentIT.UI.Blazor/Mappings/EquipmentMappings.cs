using RentIT.BlazorFrontend.Models.Equipments;
using RentIT.UI.Contracts.DTO.EquipmentMicroservice.Equipments;

namespace RentIT.BlazorFrontend.Mappings;

public static class EquipmentMappings
{
    public static UserEquipmentFormModel ToUserModel(this UserEquipmentResponse response)
        => new()
        {
            CategoryId = response.CategoryId,
            Name = response.Name,
            Description = response.Description,
            Condition = response.Condition,
            Quantity = response.Quantity,
            InternalNotes = response.InternalNotes,
        };

    public static UserEquipmentAddRequest ToAddRequest(this UserEquipmentFormModel model)
        => new()
        {
            CategoryId = model.CategoryId,
            Name = model.Name,
            Condition = model.Condition,
            Description = model.Description,
            InternalNotes = model.InternalNotes,
            Quantity = model.Quantity,
        };

    public static UserEquipmentUpdateRequest ToUpdateRequest(this UserEquipmentFormModel model)
        => new()
        {
            CategoryId = model.CategoryId,
            Name = model.Name,
            Condition = model.Condition,
            Description = model.Description,
            Quantity = model.Quantity,
            InternalNotes = model.InternalNotes 
        };

}
