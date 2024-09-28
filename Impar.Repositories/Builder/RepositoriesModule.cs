using Impar.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Impar.Repositories.Builder
{
    public static class RepositoriesModule
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICardRepository, CardRepository>();
            return services;
        }
    }
}
