using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace jwt_aspnet_core.Middleware;

public static class AuthenticationMiddleware
{
    public static IServiceCollection AddTokenAuthentication(this IServiceCollection service, IConfiguration confg)
    {
        var secret = confg.GetSection("JwtConfig").GetSection("secret").Value;
        var key = Encoding.ASCII.GetBytes(secret);
        service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost"
                };
            });
        return service;
    }
}