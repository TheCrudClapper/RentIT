using EquipmentService.API.DTO.Public;
using EquipmentService.Core.DTO.Equipments.Admin;
using EquipmentService.Core.DTO.Equipments.User;

namespace EquipmentService.API.Mappings;

public static class DtoMappings
{
    public async static Task<EquipmentAddRequestInternal> Map(this EquipmentAddRequestExternal dto)
    {
        var images = await Task.WhenAll(
           dto.Images.Select(image => image.MapAsync()));

        return new EquipmentAddRequestInternal()
        {
            CategoryId = dto.CategoryId,
            Condition = dto.Condition,
            Description = dto.Description,
            InternalNotes = dto.InternalNotes,
            Name = dto.Name,
            Quantity = dto.Quantity,
            UserId = dto.UserId,
            Images = images,
        };
    }

    public async static Task<EquipmentUpdateRequestInternal> Map(this EquipmentUpdateRequestExternal dto)
    {
        var images = await Task.WhenAll(
           dto.Images.Select(image => image.MapAsync()));

        return new EquipmentUpdateRequestInternal()
        {
            CategoryId = dto.CategoryId,
            Condition = dto.Condition,
            Description = dto.Description,
            InternalNotes = dto.InternalNotes,
            Name = dto.Name,
            Quantity = dto.Quantity,
            UserId = dto.UserId,
            Images = images,
        };
    }

    public async static Task<UserEquipmentUpdateRequestInternal> Map(this UserEquipmentUpdateRequestExternal dto)
    {
        var images = await Task.WhenAll(
           dto.Images.Select(image => image.MapAsync()));

        return new UserEquipmentUpdateRequestInternal()
        {
            CategoryId = dto.CategoryId,
            Condition = dto.Condition,
            Description = dto.Description,
            InternalNotes = dto.InternalNotes,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Images = images,
        };
    }

    public async static Task<UserEquipmentAddRequestInternal> Map(this UserEquipmentAddRequestExternal dto)
    {
        var images = await Task.WhenAll(
           dto.Images.Select(image => image.MapAsync()));

        return new UserEquipmentAddRequestInternal()
        {
            CategoryId = dto.CategoryId,
            Condition = dto.Condition,
            Description = dto.Description,
            InternalNotes = dto.InternalNotes,
            Name = dto.Name,
            Quantity = dto.Quantity,
            Images = images,
        };
    }

}
