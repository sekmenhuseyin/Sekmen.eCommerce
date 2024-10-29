using Sekmen.Commerce.Auth.Application.Models;

namespace Sekmen.Commerce.Auth.Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}

public sealed class JwtTokenGenerator(
    AppSettingsModel appSettings
) : IJwtTokenGenerator
{
    public string GenerateToken(ApplicationUser user, IEnumerable<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(appSettings.JwtOptions.Secret);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!)
        };
        claims.AddRange(roles.Select(m => new Claim(ClaimTypes.Role, m)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = appSettings.JwtOptions.Audience,
            Issuer = appSettings.JwtOptions.Issuer,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(appSettings.JwtOptions.Expire),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}