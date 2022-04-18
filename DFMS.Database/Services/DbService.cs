using DFMS.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DFMS.Database.Services
{
    public interface IDbService { }

    public static class DbServiceHelper
    {
        public static IReadOnlyCollection<ServiceInfo> Services { get; } = GetAllServices();

        private static IReadOnlyCollection<ServiceInfo> GetAllServices()
        {
            var types = ReflectionToolbox.GetAllNonAbstractImplementingInterface(typeof(IDbService));
            var services = types.Select(type => new ServiceInfo(type)).ToList();
            return services;
        }
    }

    public class ServiceInfo
    {
        public ServiceInfo(Type serviceType)
        {
            InterfaceType = serviceType.Assembly.GetTypes().Single(t => t.IsInterface && t.Name == "I" + serviceType.Name);
            ServiceType = serviceType;
        }

        public Type InterfaceType { get; }
        public Type ServiceType { get; }
    }
}
