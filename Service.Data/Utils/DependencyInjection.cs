using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Data.Context;
using Service.Data.Repositories.Interfaces.v1.User;
using Service.Data.Repositories.v1.User;
using Service.Data.UnitOfWork;
using Service.Data.UnitOfWork.Interfaces;
using System;

namespace Service.Data.Utils
{
    public static partial class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!)
             );

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}
