using Microsoft.Extensions.DependencyInjection;

namespace Sb.DependencyInjection;


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TransientServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ScopedServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SingletonServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ConfigurationAttribute : Attribute
{
}