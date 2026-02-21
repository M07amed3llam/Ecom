using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositories;
using Ecom.Infrastructure.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure
{
    public static class infrastructureRegistration
    {
        public static IServiceCollection infrastructureConfigration(this IServiceCollection services, IConfiguration configration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddScoped<IImageManagementService, ImageManagementService>();

            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configration.GetConnectionString("EcomDatabase"));
            });

            return services;
        }
    }
}