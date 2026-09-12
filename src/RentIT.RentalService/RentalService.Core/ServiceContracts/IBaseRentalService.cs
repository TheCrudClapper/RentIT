namespace RentalService.Core.ServiceContracts;

public interface IBaseRentalService
{
    /// <summary>
    /// Calculates the total rental price for a specified date range using the given daily price.
    /// </summary>
    /// <param name="startDate">The start date of the rental period. Must be earlier than or equal to <paramref name="endDate"/>.</param>
    /// <param name="endDate">The end date of the rental period. Must be later than or equal to <paramref name="startDate"/>.</param>
    /// <param name="dailyPrice">The price charged per day for the rental. Must be a non-negative value.</param>
    /// <returns>The total rental price for the specified period, calculated as the number of days (inclusive) multiplied by the
    /// daily price.</returns>
    decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, decimal dailyPrice);

    /// <summary>
    /// Calculates the total rental price based on the rental period, return date, and daily price, including any
    /// overdue charges.
    /// </summary>
    /// <remarks>If the item is returned after the scheduled end date, the total price includes charges for
    /// each additional overdue day. If returned on or before the end date, no overdue charges are applied.</remarks>
    /// <param name="startDate">The date when the rental period begins. The time component is ignored; only the date is considered.</param>
    /// <param name="endDate">The date when the rental period is scheduled to end. The time component is ignored; only the date is considered.</param>
    /// <param name="returnedDate">The actual date the item was returned. If this date is after the scheduled end date, additional charges for
    /// overdue days are applied. The time component is ignored; only the date is considered.</param>
    /// <param name="dailyPrice">The price charged per day for the rental. Must be a non-negative value.</param>
    /// <returns>The total rental price as a decimal value, including charges for any overdue days.</returns>
    decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, DateTime returnedDate, decimal dailyPrice);
}
