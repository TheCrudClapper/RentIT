using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.DTO.EquipmentImages;
using EquipmentService.Core.DTO.Equipments.Admin;
using EquipmentService.Core.DTO.Equipments.User;

namespace EquipmentService.Core.Mappings;

public static class EquipmentMappings
{
    public static Equipment ToEquipment(this EquipmentAddRequest request)
    {
        return new Equipment
        {
            Id = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            Name = request.Name,
            InternalNotes = request.InternalNotes,
            Description = request.Description,
            Quantity = request.Quantity,
            Condition = request.Condition,
            UserId = request.UserId,
            DateCreated = DateTime.UtcNow,
            IsActive = true,
        };
    }

    public static Equipment ToUserEquipment(this UserEquipmentAddRequest request)
    {
        return new Equipment
        {
            Id = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            Name = request.Name,
            InternalNotes = request.InternalNotes,
            Description = request.Description,
            Quantity = request.Quantity,
            Condition = request.Condition,
            DateCreated = DateTime.UtcNow,
            IsActive = true,
        };
    }

    public static Equipment ToEquipment(this EquipmentUpdateRequest request)
    {
        return new Equipment
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            InternalNotes = request.InternalNotes,
            Description = request.Description,
            Quantity = request.Quantity,
            Condition = request.Condition,
            UserId = request.UserId,
        };
    }

    public static EquipmentResponse ToEquipmentResponse(this Equipment equipment)
        => new EquipmentResponse
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Quantity = equipment.Quantity,
            InternalNotes = equipment.InternalNotes,
            UserId = equipment.UserId,
            Condition = equipment.Condition,
            Description = equipment.Description,
            CategoryId = equipment.CategoryId,
            Images = equipment.Images.Select(x => new EquipmentImageResponse(x.Id, x.ResourcePath) { })
                .ToList(),
        };

    public static UserEquipmentResponse ToUserEquipmentResponse(this Equipment equipment)
        => new UserEquipmentResponse
        {
            Id = equipment.Id,
            Name = equipment.Name,
            Quantity = equipment.Quantity,
            InternalNotes = equipment.InternalNotes,
            Description = equipment.Description,
            CategoryId = equipment.CategoryId,
            Condition = equipment.Condition,
            Images = equipment.Images.Select(x => new EquipmentImageResponse(x.Id, x.ResourcePath) { })
                .ToList(),
        };
   

    public static UserEquipmentListResponse ToUserEquipmentListResponse(this Equipment equipment)
        => new UserEquipmentListResponse(equipment.Id, equipment.Name, equipment.Quantity, equipment.Category.Name, equipment.DateCreated);
}

