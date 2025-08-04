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
                CreatorEmail = rental.CreatedBy.Email!,
                EndDate = rental.EndDate,
                RentalPrice = rental.RentalPrice,
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
                UserId = request.UserId,
                EquipmentId = request.EquipmentId,
                RentalPrice = request.RentalPrice,
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
                UserId = request.UserId,
                EquipmentId = request.EquipmentId,
                DateEdited = DateTime.UtcNow,
                ReturnedDate = request.ReturnedDate,
            };
        }
    }
}
