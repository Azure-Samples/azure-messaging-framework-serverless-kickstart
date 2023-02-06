using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Azure.Functions.Attributes;

/// <summary>
/// Custom Attribute to register services into the Microsoft IoC container.
/// When bootstrapping the function app, this attribute will help discover the classes that needed to be registered.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true)]
public class DependencyInjectionAttribute : Attribute
{
    /// <summary>
    ///  This property is used to define the base interface details
    /// </summary>
    /// <value></value>
    public Type? Extends { get; set; }

    /// <summary>
    ///  This property is used to define the lifetime of the service
    /// </summary>
    /// <value></value>
    public ServiceLifetime ServiceType { get;  set; }
}