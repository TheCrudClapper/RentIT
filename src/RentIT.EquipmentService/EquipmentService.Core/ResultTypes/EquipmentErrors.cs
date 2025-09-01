namespace EquipmentService.Core.ResultTypes;

public class EquipmentErrors
{
    public static readonly Error EquipmentNotFound = new Error(
        404, "Equipment of given Id not found");

    public static readonly Error FailedToDeleteEquipment = new Error(
        404, "Failed to delete Equipment");
    public static Error EquipmentRented(DateTime startDate, DateTime endDate) => new Error(
       422, $"Equipment is not avaliable right now, Rented from {startDate} to {endDate} by someone");

    public static Error EquipmentInMaintnance = new Error(
        422, "Equimpment is now in maintenance");

    public static Error EquipmentAlreadyExist = new Error(
        422, "Equimpment like this already exists");
}

