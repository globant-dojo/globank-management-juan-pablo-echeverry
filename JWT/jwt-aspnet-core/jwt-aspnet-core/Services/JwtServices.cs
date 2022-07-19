using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace jwt_aspnet_core.Services;

public class JwtServices
{
    private readonly string _secret;
    private readonly string _expired;

    public JwtServices(IConfiguration config)
    {
        _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
        _expired = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;
    }

    public string GenerateSecurityToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expired)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}