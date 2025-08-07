using RentIT.Core.Domain.Entities;
using RentIT.Core.DTO.RentalDto;

namespace RentIT.Core.Mappings
{
    public static class RentalMappings
    {
        public static RentalResponse ToRentalResponse(this Rental rental)
        {
            return new RentalResponse
            {
                StartDate = rental.StartDate,
                Id = rental.Id,
                CreatorEmail = rental.RentedBy.Email!,
                EndDate = rental.EndDate,
                TotalRentalPrice = rental.TotalRentalPrice,
                EquipmentName = rental.Equipment.Name,
                ReturnedDate = rental.ReturnedDate
            };
        }

        public static Rental ToRentalEntity(this RentalAddRequest request)
        {
            return new Rental
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                RentedByUserId = request.UserId,
                EquipmentId = request.EquipmentId,
                TotalRentalPrice = 0,
                ReturnedDate = null,
                IsActive = true,
                DateCreated = DateTime.UtcNow,
            };
        }

        public static Rental ToRentalEntity(this RentalUpdateRequest request)
        {
            return new Rental
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EquipmentId = request.EquipmentId,
                DateEdited = DateTime.UtcNow,
                ReturnedDate = request.ReturnedDate,
            };
        }
    }
}
