using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RentIT.Core.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(50)]
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<Equipment> CreatedEquipmentItems { get; set; } = new List<Equipment>();
        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
