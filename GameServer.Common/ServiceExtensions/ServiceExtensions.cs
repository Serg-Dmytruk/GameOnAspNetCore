using Microsoft.Extensions.DependencyInjection;
using GameServer.Common.Services.SigninServices;
using Data.Services.UserDataService;
using Data.Services.GameDataService;
using GameServer.Common.Services.StatisticServices;
using GameServer.Common.Services.GameServices;
using GameServer.Common.Services.TableService;

namespace GameServer.Common.ServiceExtensions
{
    public static class ServiceExtensions
    {
        //AddTransient
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISigninService, SigninService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddSingleton<ITableService, TableService>();
            services.AddSingleton<GameService>();
            return services;
        }

        //AddScoped
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IGameDataService, GameDataService>();
            return services;
        }
    }
}
