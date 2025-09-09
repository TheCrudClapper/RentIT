using RentalService.Core.Domain.Entities;
using RentalService.Core.DTO.RentalDto;

namespace RentalService.Core.Mappings
{
    public static class RentalMappings
    {
        public static RentalResponse ToRentalResponse(this Rental rental, EquipmentResponse equipment)
        {
            return new RentalResponse(rental.Id,
                rental.ReturnedDate,
                rental.StartDate,
                rental.UserId,
                rental.EndDate,
                rental.RentalPrice,
                new EquipmentResponse(
                    equipment.Id,
                    equipment.Name,
                    equipment.CreatedByUserId,
                    equipment.RentalPricePerDay,
                    equipment.SerialNumber,
                    equipment.CategoryName,
                    equipment.Status,
                    equipment.Notes));
        }

        public static Rental ToRental(this UserRentalAddRequest request)
        {
            return new Rental
            {
                EquipmentId = request.EquipmentId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                IsActive = true,
                DateCreated = DateTime.Now,
            };
        }

        public static Rental ToRentalEntity(this RentalAddRequest request)
        {
            return new Rental
            {
                UserId = request.UserId,
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
                UserId = request.UserId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                EquipmentId = request.EquipmentId,
                DateEdited = DateTime.UtcNow,
                ReturnedDate = request.ReturnedDate,
            };
        }
    }
}
