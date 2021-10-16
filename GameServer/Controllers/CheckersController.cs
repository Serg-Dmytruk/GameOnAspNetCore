using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServer.Common.Services.TableService;

namespace GameServer.Controllers
{
    [Route("ckeckers")]
    public class CheckersController : ControllerBase
    {
        private readonly ITableService _tableService;

        public CheckersController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet("tables")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<string>))]
        public async Task<IEnumerable<string>> GetTables()
        {
            return await _tableService.GetAwailableTables();
        }
    }
}
