using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.Web
{
    public static class Initializer
    {
        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {

            services.AddTransient<ProductRepository>();
            services.AddTransient<OrderRepository>();
            services.AddTransient<ClientRepository>();

            return services;
        }

    }
}
