using Game.Common.ModelsDto;
using GameServer.Common.Services.StatisticServices;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace GameServer.Controllers
{
    [Route("statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("{login}")]
        [SwaggerResponse(200, Type = typeof(StatisticDto))]
        public async Task<StatisticDto> GetStatistic(string login)
        {
            return await _statisticService.GetStatistic(login);
        }
    }
}
