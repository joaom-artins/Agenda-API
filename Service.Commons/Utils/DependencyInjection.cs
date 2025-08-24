using Service.Commons.Notification.Interface;
using Service.Commons.Notification;
using Microsoft.Extensions.DependencyInjection;
using Service.Commons.LoggedUser.Interfaces;
using Service.Commons.LoggedUser;

namespace Service.Commons.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddCommons(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();
        services.AddScoped<IGetLoggedUser, GetLoggedUser>();

        services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
