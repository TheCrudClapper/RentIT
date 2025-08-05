using System.ComponentModel.DataAnnotations.Schema;

namespace RentIT.Core.Domain.Entities
{
    /// <summary>
    /// Represents a rental entity that contains information about the equipment associated with the rental.
    /// </summary>
    /// <remarks>The <see cref="Rental"/> class is used to manage and track equipment rentals. It includes a
    /// collection of  <see cref="Equipment"/> objects that are part of the rental. This class inherits from <see
    /// cref="BaseEntity"/>,  which provides common properties for all entities in the system.</remarks>
    public class Rental : BaseEntity
    {
        public Guid EquipmentId { get; set; }
        public Guid RentedByUserId { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalRentalPrice { get; set; }
        [ForeignKey("EquipmentId")]
        public Equipment Equipment { get; set; } = null!;

        [ForeignKey("RentedByUserId")]
        public User RentedBy { get; set; } = null!;

    }
}
