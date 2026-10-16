using EquipmentService.Core.Domain.ResultTypes;
using RentIT.Contracts.DTO.EquipmentMicroservice.Carts;

namespace EquipmentService.Core.ServiceContracts;

public interface ICartService
{
    Task<Result> AddToCart(Guid userId, CartItemAddRequest request);
    Task<Result> UpdateCartItem(Guid userId, CartItemUpdateRequest request);
    Task<Result> RemoveFromCart(Guid userId);
}
