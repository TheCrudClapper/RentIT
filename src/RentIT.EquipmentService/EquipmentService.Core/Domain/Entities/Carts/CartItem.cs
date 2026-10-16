using EquipmentService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EquipmentService.Core.Domain.Entities.Carts;

public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    [ForeignKey("CartId")]
    public Cart Cart { get; set; } = null!;
    public Guid RentalListingId { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
