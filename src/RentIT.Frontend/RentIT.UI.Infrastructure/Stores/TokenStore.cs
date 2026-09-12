using RentIT.UI.Core.InfrastructureContracts;

namespace RentIT.UI.Infrastructure.Stores;

public class TokenStore : ITokenStore
{
    private string Token { get; set; } = null!;

    public string GetAccessToken() => Token is not null
            ? Token
            : throw new ArgumentException("Token is not present in store.");

    public void SaveAccessToken(string token) => Token = token;
}
