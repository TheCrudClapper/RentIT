using RentIT.UI.Core.DTO.Equipments;
using RentIT.UI.Core.ResultTypes;

namespace RentIT.UI.Core.HttpClientContracts;

public interface IUserEquipmentHttpClient 
{
    Task<Result<IReadOnlyCollection<UserEquipmentListResponse>>> GetUserEquipmentsList();
}
