using System.Text;
using jwt_aspnet_core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace jwt_aspnet_core.Middleware;

public static class AddJWTTokenServicesExtensions
{
    public static void AddJWTTokenServices(this IServiceCollection service, IConfiguration config)
    {
        var settings = new JwtSettings();
        config.Bind("JsonWebTokenKeys", settings);
        service.AddSingleton(settings);
        service.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(opt =>
        {
            opt.RequireHttpsMetadata = false;
            opt.SaveToken = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.IssuerSigningKey)),
                ValidateIssuer = settings.ValidateIssuer,
                ValidIssuer = settings.ValidIssuer,
                ValidateAudience = settings.ValidateAudience,
                ValidAudience = settings.ValidAudience,
                RequireExpirationTime = settings.RequireExpirationTime,
                ValidateLifetime = settings.ValidateLifetime,
                ClockSkew = TimeSpan.FromDays(1)
            };
        });

    }
}