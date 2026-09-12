using EquipmentService.Core.Domain.ResultTypes;

namespace EquipmentService.Core.Domain.Entities.Equipments.Errors;

public class EquipmentErrors
{
    public static readonly Error EquipmentNotFound = Error.Create(ErrorType.NotFound, "Equipment.NotFound", "Equipment of given Id not found");

    public static readonly Error FailedToDeleteEquipment = Error.Create(ErrorType.Unexpected, "Equipment.FailedToDelete", "Failed to delete Equipment");

    public static Error EquipmentRented(DateTime startDate, DateTime endDate) => Error.Create(ErrorType.Conflict, "Equipment.Rented", $"Equipment is not avaliable right now, Rented from {startDate} to {endDate} by someone");

    public static Error EquipmentInMaintnance = Error.Create(ErrorType.Conflict, "Equipment.InMaintenance", "Equimpment is now in maintenance");

    public static Error EquipmentAlreadyExist = Error.Create(ErrorType.Conflict, "Equipment.AlreadyExists", "Equimpment like this already exists");
}
