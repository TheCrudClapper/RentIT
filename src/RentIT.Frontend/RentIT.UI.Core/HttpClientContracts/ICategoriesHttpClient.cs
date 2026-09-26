using RentIT.UI.Contracts.DTO.Shared;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.HttpClientContracts;

public interface ICategoriesHttpClient
{
    Task<Result<IReadOnlyCollection<SelectItem>>> GetCategories();
}
