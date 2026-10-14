using System.ComponentModel.DataAnnotations;

namespace RentIT.Contracts.DTO.EquipmentMicroservice.Categories;

public class CategoryUpdateRequest
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; } = null!;
    [StringLength(255)]
    public string? Description { get; set; }
}


