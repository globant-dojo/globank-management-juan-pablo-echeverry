using jwt_aspnet_core.Models;
using jwt_aspnet_core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt_aspnet_core.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _settings;

        public AccountController(JwtSettings settings)
        {
            _settings = settings;
        }

        private IEnumerable<Users> Logins = new List<Users>()
        {
            new Users
            {
                Id = Guid.NewGuid(),
                EmailId = "fake@email.com",
                UserName = "admin",
                Password = "admin"
            },
            new Users
            {
                Id = Guid.NewGuid(),
                EmailId = "fake1@email.com",
                UserName = "user1",
                Password = "admin"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var user = Logins.FirstOrDefault(x => x.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (user == null) return BadRequest("Credentials Invalid");
                var userToken = new UserTokens
                {
                    EmailId = user.EmailId,
                    GuidId = Guid.NewGuid(),
                    UserName = user.UserName,
                    Id = user.Id
                };
                var Token = JwtHelpers.GenTokenKey(userToken, _settings);
                return Ok(Token);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetList()
        {
            return Ok(Logins);
        }
    }
}
