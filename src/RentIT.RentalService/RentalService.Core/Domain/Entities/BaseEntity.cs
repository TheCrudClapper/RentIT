using System.ComponentModel.DataAnnotations;

namespace RentalService.Core.Domain.Entities
{
    /// <summary>
    /// Represents the base entity type for database models, providing common properties for tracking entity state and
    /// metadata.
    /// </summary>
    /// <remarks>This class includes properties for uniquely identifying the entity, tracking creation,
    /// modification, and deletion timestamps,  and indicating whether the entity is active. It is intended to be used
    /// as a base class for other entity types in the application.</remarks>
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated {  get; set; }
        
        public DateTime? DateEdited { get; set; }

        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
