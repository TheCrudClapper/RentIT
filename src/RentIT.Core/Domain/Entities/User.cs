using System.ComponentModel.DataAnnotations;

namespace RentIT.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(50)]
        public string Email { get; set; } = null!;
        public bool IsAdmin { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<Equipment> CreatedEquipmentItems { get; set; } = new List<Equipment>();
    }
}
