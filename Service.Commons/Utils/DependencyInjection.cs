using Service.Commons.Notification.Interface;
using Service.Commons.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Service.Commons.Utils;

public static class DependencyInjection
{
    public static IServiceCollection AddCommons(this IServiceCollection services)
    {
        services.AddScoped<INotificationContext, NotificationContext>();

        return services;
    }
}
