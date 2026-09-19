using System.ComponentModel.DataAnnotations;

namespace EquipmentService.Core.DTO.EquipmentImages;

public class EquipmentImageAddRequest
{
    [Required]
    public byte[] Image { get; set; } = null!;
}
