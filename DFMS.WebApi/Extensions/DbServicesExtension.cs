using Core.Database.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DFMS.Database.Services
{
    public static class DbServicesExtension
    {
        public static IServiceCollection AddScopedDbServices(this IServiceCollection services)
        {
            foreach(var service in DbServiceHelper.Services)
            {
                services.AddScoped(service.InterfaceType, service.ServiceType);
            }

            return services;
        }
    }
}
