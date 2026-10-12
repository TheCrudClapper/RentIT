namespace RentIT.Contracts.DTO.EquipmentMicroservice.Images;

public record ImageRequest(Guid? Id, bool IsCover, byte[] Image);

