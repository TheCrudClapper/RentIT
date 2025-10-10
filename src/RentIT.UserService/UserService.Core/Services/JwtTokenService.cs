using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using UserService.Core.Domain.Entities;
using UserService.Core.ServiceContracts;

namespace UserService.Core.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtTokenService> _logger;
    public JwtTokenService(IConfiguration configuration,
        ILogger<JwtTokenService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public string GenerateJwtToken(User user, IList<string> roles)
    {
        //Creating security key
        var signinKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));

        //Creating signing credentials out of key and using HmacSha256
        var credentials = new SigningCredentials(signinKey,
            SecurityAlgorithms.HmacSha256);

        //Piling up user's claims
        List<Claim> claims =
        [
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email!),
                ..roles.Select(role => new Claim(ClaimTypes.Role, role))
        ];


        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow
                .AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            SigningCredentials = credentials,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
        };

        var tokenHandler = new JsonWebTokenHandler();

        //Creating token
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        _logger.LogInformation($"Access token successfully generated for User: {user.Email}" +
            $" Token: {accessToken}");

        return accessToken;
    }
    public string GenerateJwtRefreshToken()
    {
        throw new NotImplementedException();
    }
}
