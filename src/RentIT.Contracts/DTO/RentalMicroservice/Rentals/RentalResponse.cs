namespace RentIT.Contracts.DTO.RentalMicroservice.Rentals
{
    public record RentalResponse(Guid Id,
        DateTime? ReturnedDate,
        DateTime StartDate,
        Guid RentedByUserId,
        DateTime EndDate,
        decimal TotalRentalPrice,
        EquipmentResponse equipmentDetails);
}

