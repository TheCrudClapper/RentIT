using RentIT.Core.Domain.Entities;
namespace RentIT.Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllActiveUsersAsync();
    }
}
