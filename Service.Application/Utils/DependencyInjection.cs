using Microsoft.Extensions.DependencyInjection;
using Service.Application.Services.Interfaces.v1;
using Service.Application.Services.v1;

namespace Service.Application.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();

        services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
