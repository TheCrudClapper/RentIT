using System.ComponentModel.DataAnnotations;

namespace RentIT.Contracts.DTO.EquipmentMicroservice.EquipmentImages;

public class EquipmentImageAddRequest
{
    [Required]
    public byte[] Image { get; set; } = null!;
}

