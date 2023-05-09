using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OA.Service.Services;
internal class TokenService : ITokenService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims, string Jwt_Key, string Jwt_Issuer, string Jwt_Audience)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt_Key));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokeOptions = new JwtSecurityToken(
                    Jwt_Issuer,
                    Jwt_Audience,
                    claims,
                    expires: DateTime.Now.AddDays(5),
                    signingCredentials: signinCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token,string Jwt_Key)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwt_Key)),
            ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");
        return principal;
    }
}

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims, string Jwt_Key, string Jwt_Issuer, string Jwt_Audience);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token,string Jwt_Key);
}