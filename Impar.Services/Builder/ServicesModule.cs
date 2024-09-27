using Impar.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Impar.Services.Builder
{
    public static class ServicesModule
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICardService, CardService>();

            return services;
        }
    }
}
