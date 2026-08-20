
namespace RentIT.UI.Core.InfrastructureContracts;
public interface ITokenStore
{
    void SaveAccessToken(string token);
    string GetAccessToken(string token);
}
