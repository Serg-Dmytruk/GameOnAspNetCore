using Game.Common.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using GameServer.Common.Services.SigninServices;


namespace GameServer.Controllers
{
    [Route("signin")]
    public class SigninController : ControllerBase
    {
        private readonly ISigninService _signinService;
        public SigninController(ISigninService signinService)
        {
            _signinService = signinService;
        }

        [HttpPost("check")]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<LoginResponseDto> CheckLoginData(LoginModelDto request)
        {
            return await _signinService.IsUserExist(request);
        }

        [HttpPost("login")]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<LoginResponseDto> Login([FromBody] LoginModelDto request)
        {
            return await _signinService.Login(request);
        }

        [HttpPost("registration")]
        public async Task Registration([FromBody] LoginModelDto request)
        {
             await _signinService.AddUser(request);
        }
    }
}
