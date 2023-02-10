using System.Reflection;
using Microsoft.Azure.Functions.Attributes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.Functions.Extensions;
public static class ServiceExtensions
{
    /// <summary>
    /// This method gets the services that are decorated with DependencyInjection attribute.
    /// </summary>
    /// <returns></returns>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        Type dependencyInjectionRegistration = typeof(DependencyInjectionAttribute);

        var serviceTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p.IsDefined(dependencyInjectionRegistration)).Select(s => new
            {
                CustomAttr = s.GetCustomAttribute<DependencyInjectionAttribute>(),
                Implementation = s,
            });

        foreach (var serviceType in serviceTypes)
        {
            var customAttr = serviceType.CustomAttr;
            if (customAttr != null)
            {
                var serviceLifetime = customAttr.ServiceType;
                services.Add(new ServiceDescriptor(customAttr.Extends ?? serviceType.Implementation.BaseType, serviceType.Implementation, serviceLifetime));
            }
        }
    }
}