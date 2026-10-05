using RentalService.Core.Domain.Interfaces;

namespace RentalService.Core.Domain.Entities.Carts;

public class Cart : BaseEntity, ISoftDelete
{
    public Guid UserId { get; set; }
    public ICollection<CartItem> Items { get; set; } = [];
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

