using RentalService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalService.Core.Domain.Entities
{
    /// <summary>
    /// Represents a rental entity that contains information about the equipment associated with the rental.
    /// </summary>
    /// <remarks>The <see cref="Rental"/> class is used to manage and track equipment rentals. It includes a
    /// collection of  <see cref="Equipment"/> objects that are part of the rental. This class consumes contract <see
    /// cref="IBaseEntity"/>,  which provides common properties for all entities in the system.</remarks>
    public class Rental : IBaseEntity, ISoftDelete
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal RentalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public bool IsActive { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
