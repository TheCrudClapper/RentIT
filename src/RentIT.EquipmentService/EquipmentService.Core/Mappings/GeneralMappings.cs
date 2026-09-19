using EquipmentService.Core.Domain.Interfaces;
using EquipmentService.Core.DTO.Shared;

namespace EquipmentService.Core.Mappings;

public static class GeneralMappings
{
    public static UpdatedResponse ToUpdatedResponse<T>(this T entity) where T : BaseEntity
    {
        return new UpdatedResponse(entity.Id, entity.DateEdited.GetValueOrDefault());
    }

    public static CreatedResponse ToCreatedResponse<T>(this T entity) where T : BaseEntity
    {
        return new CreatedResponse(entity.Id, entity.DateCreated);
    }
}
