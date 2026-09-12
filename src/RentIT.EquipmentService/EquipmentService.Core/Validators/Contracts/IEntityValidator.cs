using EquipmentService.Core.Domain.Entities.Equipments;
using EquipmentService.Core.Domain.Interfaces;
using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Validators.Contracts;

/// <summary>
/// Defines a contract for validating business rules of domain entities 
/// when creating a new entity or updating an existing one.
/// </summary>
/// <typeparam name="T">
/// Type of the entity to be validated. Must be a reference type
/// that implements <see cref="IBaseEntity"/>.
/// </typeparam>
public interface IEntityValidator<T>
    where T : class, IBaseEntity
{
    Task<Result> ValidateEntity(Equipment entity, Guid? entityId = null, CancellationToken cancellationToken = default);
}
