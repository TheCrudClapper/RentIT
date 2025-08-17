using RentalService.Core.Domain.Entities;
using RentalService.Core.DTO.RentalDto;

namespace RentalService.Core.Mappings
{
    public static class RentalMappings
    {
        public static RentalResponse ToRentalResponse(this Rental rental)
        {
            return new RentalResponse
            {
                StartDate = rental.StartDate,
                Id = rental.Id,
                EndDate = rental.EndDate,
                ReturnedDate = rental.ReturnedDate
            };
        }

        public static Rental ToRentalEntity(this RentalAddRequest request)
        {
            return new Rental
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EquipmentId = request.EquipmentId,
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
