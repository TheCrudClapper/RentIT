using EquipmentService.Core.Domain.Entities;
using EquipmentService.Core.DTO.EquipmentDto;
using EquipmentService.Core.DTO.UserDto;

namespace EquipmentService.Core.Mappings;

public static class EquipmentMappings
{
    public static Equipment ToEquipment(this EquipmentAddRequest request)
    {
        return new Equipment
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Notes = request.Notes,
            RentalPricePerDay = request.RentalPricePerDay,
            SerialNumber = request.SerialNumber,
            Status = request.Status,
            CreatedByUserId = request.UserId,
            DateCreated = DateTime.UtcNow,
            IsActive = true,
        };
    }

    public static Equipment ToEquipment(this UserEquipmentAddRequest request)
    {
        return new Equipment
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
            Notes = request.Notes,
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
            Notes = request.Notes,
            DateEdited = DateTime.UtcNow,
            Status = request.Status,
            CreatedByUserId = request.UserId,
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
            CategoryName = equipment.Category.Name,
            CreatedByUserId = equipment.CreatedByUserId,
            RentalPricePerDay = equipment.RentalPricePerDay,
            Notes = equipment.Notes,
            SerialNumber = equipment.SerialNumber,
            Status = equipment.Status.ToString(),
        };
    }
}

