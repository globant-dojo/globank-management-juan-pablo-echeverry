using jwt_aspnet_core.Services;
using Microsoft.AspNetCore.Mvc;

namespace jwt_aspnet_core.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public string GetRandomToken()
        {
            var jwt = new JwtServices(_config);
            var token = jwt.GenerateSecurityToken("fake@emai.com");
            return token;
        }
    }
}
