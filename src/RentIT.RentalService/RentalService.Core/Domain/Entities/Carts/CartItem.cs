using RentalService.Core.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalService.Core.Domain.Entities.Carts;

public class CartItem : BaseEntity, ISoftDelete
{
    public Guid CartId { get; set; }
    [ForeignKey("CartId")]
    public Cart Cart { get; set; } = null!;
    public Guid RentalListingId { get; set; }
    public int Quantity { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime? DateDeleted { get; set; }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        DateDeleted = DateTime.UtcNow;
        IsActive = false;
    }
}
