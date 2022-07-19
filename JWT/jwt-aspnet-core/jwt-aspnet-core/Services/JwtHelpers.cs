using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jwt_aspnet_core.Models;
using Microsoft.IdentityModel.Tokens;

namespace jwt_aspnet_core.Services;

public static class JwtHelpers
{
    public static IEnumerable<Claim> GetClaims(this UserTokens userTokens, Guid Id)
    {
        IEnumerable<Claim> claims = new Claim[]
        {
            new Claim("Id", userTokens.Id.ToString()),
            new Claim(ClaimTypes.Name, userTokens.UserName),
            new Claim(ClaimTypes.Email, userTokens.EmailId),
            new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString())
        };
        return claims;
    }

    public static IEnumerable<Claim> GetClaims(this UserTokens userTokens, out Guid Id)
    {
        Id = Guid.NewGuid();
        return GetClaims(userTokens, Id);
    }

    public static UserTokens GenTokenKey(UserTokens model, JwtSettings settings)
    {
        try
        {
            var userToken = new UserTokens();
            if(model == null) throw new ArgumentException(nameof(model));

            var key = Encoding.ASCII.GetBytes(settings.IssuerSigningKey);
            Guid Id = Guid.Empty;
            DateTime expired = DateTime.UtcNow.AddDays(1);
            userToken.Validaty = expired.TimeOfDay;
            var JwToken = new JwtSecurityToken(settings.ValidIssuer, settings.ValidAudience,
                GetClaims(model, out Id), new DateTimeOffset(DateTime.Now).DateTime,
                new DateTimeOffset(expired).Date
                , new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            userToken.Token = new JwtSecurityTokenHandler().WriteToken(JwToken);
            userToken.UserName = model.UserName;
            userToken.Id = model.Id;
            userToken.GuidId = Id;
            return userToken;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}