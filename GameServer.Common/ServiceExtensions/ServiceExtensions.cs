using Microsoft.Extensions.DependencyInjection;
using GameServer.Common.Services.SigninServices;
using Data.Services.UserDataService;

namespace GameServer.Common.ServiceExtensions
{
    public static class ServiceExtensions
    {
        //AddTransient
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ISigninService, SigninService>();
            return services;
        }

        //AddScoped
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IUserDataService, UserDataService>();
            return services;
        }
    }
}
