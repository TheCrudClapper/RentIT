using EquipmentService.Core.Domain.Entities.Carts;
using EquipmentService.Core.Domain.RepositoryContracts;
using EquipmentService.Core.Domain.ResultTypes;
using EquipmentService.Core.ServiceContracts;
using EquipmentService.Core.Validators;
using RentIT.Contracts.DTO.EquipmentMicroservice.Carts;

namespace EquipmentService.Core.Services.CartServices;

public class CartService : ICartService
{
    private readonly IGenericRepository<Cart> _cartRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICartValidator _cartValidator;
    public CartService(IGenericRepository<Cart> cartRepository, IUnitOfWork unitOfWork, ICartValidator cartValidator)
    {
        _cartRepository = cartRepository;
        _unitOfWork = unitOfWork;
        _cartValidator = cartValidator;
    }

    public Task<Result> AddToCart(Guid userId, CartItemAddRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RemoveFromCart(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateCartItem(Guid userId, CartItemUpdateRequest request)
    {
        //var cart = await _cartRepository.GetByConditionAsync(x => x.UserId == userId);
        //if (cart is null)
        //    return Result.Failure(Error.Create(ErrorType.NotFound, "Cart.NotFound", "User cart was not found"));
        throw new NotImplementedException();T d
       
    }
}
