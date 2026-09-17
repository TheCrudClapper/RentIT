using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Interfaces;
using EquipmentService.Core.DTO.Equipments;
using EquipmentService.Core.DTO.Shared;

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
            InternalNotes = request.Notes,
            RentalPricePerDay = request.RentalPricePerDay,
            SerialNumber = request.SerialNumber,
            Status = request.Status,
            OwnerId = request.UserId,
            DateCreated = DateTime.UtcNow,
            IsActive = true,
        };
    }

    public static Equipment ToEquipment(this UserEquipmentAddRequest request)
    {
        return new Equipment
        {
            Id = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            Name = request.Name,
            InternalNotes = request.Notes,
            RentalPricePerDay = request.RentalPricePerDay,
            SerialNumber = request.SerialNumber,
            Status = request.Status,
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
            InternalNotes = request.Notes,
            DateEdited = DateTime.UtcNow,
            Status = request.Status,
            OwnerId = request.UserId,
            RentalPricePerDay = request.RentalPricePerDay,
            SerialNumber = request.SerialNumber,
        };
    }

    public static EquipmentResponse ToEquipmentResponse(this Equipment equipment)
    {
        return new EquipmentResponse
        {
            Id = equipment.Id,
            Name = equipment.Name,
            CategoryId = equipment.CategoryId,
            RentalPricePerDay = equipment.RentalPricePerDay,
            Notes = equipment.InternalNotes,
            SerialNumber = equipment.SerialNumber,
            Status = equipment.Status
        };
    }

    public static UpdatedResponse ToUpdatedResponse<T>(this T entity) where T : BaseEntity
    {
        return new UpdatedResponse(entity.Id, entity.DateEdited.GetValueOrDefault());
    }

    public static CreatedResponse ToCreatedResponse<T>(this T entity) where T : BaseEntity
    {
        return new CreatedResponse(entity.Id, entity.DateCreated);
    }

    public static UserEquipmentListResponse ToUserEquipmentListResponse(this Equipment equipment)
        => new UserEquipmentListResponse(equipment.Id, equipment.Name, equipment.SerialNumber, equipment.RentalPricePerDay);
}

