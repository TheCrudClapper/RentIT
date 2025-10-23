using Microsoft.Extensions.Configuration;
using RentalService.Core.ServiceContracts;

namespace RentalService.Core.Services;

public abstract class BaseRentalService : IBaseRentalService
{
    protected readonly IConfiguration _configuration;

    //Multiplier in format eg. 1.3, 2.1 etc
    //Default fallback value is 1.4
    private readonly decimal _daysOverduePriceMultiplier;
    public BaseRentalService(IConfiguration configuration)
    {
        _configuration = configuration;
        _daysOverduePriceMultiplier =
           decimal.TryParse(_configuration["DAYS_OVERDUE_COST_MULTIPLIER"], out decimal parsedValue) ? parsedValue : 1.4m;
    }

    public decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, DateTime returnedDate, decimal dailyPrice)
    {
        var days = (endDate.Date - startDate.Date).Days;
        if (returnedDate > endDate)
        {
            var daysOverdue = (returnedDate.Date - endDate.Date).Days;
            return (dailyPrice * days) + (daysOverdue * (_daysOverduePriceMultiplier * dailyPrice));
        }

        return dailyPrice * days;
    }
    public decimal CalculateTotalRentalPrice(DateTime startDate, DateTime endDate, decimal dailyPrice)
    {
        var days = (endDate.Date - startDate.Date).Days;
        return dailyPrice * days;
    }
}
