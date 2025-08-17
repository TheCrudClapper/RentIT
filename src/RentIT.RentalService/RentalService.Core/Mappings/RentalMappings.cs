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
<<<<<<< HEAD:src/RentIT.RentalService/RentalService.Core/Mappings/RentalMappings.cs
                EndDate = rental.EndDate,
=======
                CreatorEmail = rental.RentedBy.Email!,
                EndDate = rental.EndDate,
                TotalRentalPrice = rental.TotalRentalPrice,
                EquipmentName = rental.Equipment.Name,
>>>>>>> 6cdc2c7a6c07dad638ba1cbe5a252fd415b7cd49:src/RentIT.Core/Mappings/RentalMappings.cs
                ReturnedDate = rental.ReturnedDate
            };
        }

        public static Rental ToRentalEntity(this RentalAddRequest request)
        {
            return new Rental
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
<<<<<<< HEAD:src/RentIT.RentalService/RentalService.Core/Mappings/RentalMappings.cs
                EquipmentId = request.EquipmentId,
=======
                RentedByUserId = request.UserId,
                EquipmentId = request.EquipmentId,
                TotalRentalPrice = 0,
>>>>>>> 6cdc2c7a6c07dad638ba1cbe5a252fd415b7cd49:src/RentIT.Core/Mappings/RentalMappings.cs
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
