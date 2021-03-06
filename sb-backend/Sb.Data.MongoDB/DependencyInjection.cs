using Sb.Data;
using Sb.Data.MongoDB;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services, Action<MongoConfiguration> configureAction)
        {
            MongoConfiguration config = new();
            configureAction(config);
            services.AddSingleton(config);
            services.AddSingleton(typeof(IRepository<>), typeof(MongoRepository<>));
            services.AddSingleton(typeof(MongoRepository<>));
            return services;
        }
    }
}