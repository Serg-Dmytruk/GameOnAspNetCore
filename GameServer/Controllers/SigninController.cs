using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Game.Common.ModelsDto; 

namespace GameServer.Controllers
{
    [Route("signin")]
    public class SigninController : ControllerBase
    {

        [HttpPost("check")]
        [SwaggerResponse(200, Type = typeof(bool))]
        public async Task<LoginResponseDto> CheckLoginData(LoginModelDto loginModel)
        {
            return await Task.Run(() => new LoginResponseDto());
        }

    }
}
