using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using GameClient.Common.Shared;
using Game.Common.ModelsDto;
using GameClient.Common.ApiMethods;
using GameClient.Common.Services.ApiServices;

namespace GameClient.Common.Pages
{
    [Route("hub")]
    [Layout(typeof(EmptyLayout))]
    public partial class GameHub
    {
    }
}
