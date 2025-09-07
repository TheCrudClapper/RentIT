using RentalService.Core.Domain.Interfaces;
using RentalService.Core.ResultTypes;

namespace EquipmentService.Core.Validators.ValidatorContracts
{
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
        /// <summary>
        /// Validates the provided entity before it is created and persisted.
        /// </summary>
        /// <param name="entity">The new entity instance to validate.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the entity passed validation 
        /// and, if not, containing error details.
        /// </returns>
        Task<Result> ValidateNewEntity(T entity);

        /// <summary>
        /// Validates the provided entity before it is updated in persistence storage.
        /// </summary>
        /// <param name="entity">The modified entity instance to validate.</param>
        /// <param name="entityId">The identifier of the entity being updated.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating whether the update operation is valid 
        /// and, if not, containing error details.
        /// </returns>
        Task<Result> ValidateUpdateEntity(T entity, Guid entityId);

    }
}
