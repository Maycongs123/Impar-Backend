using Impar.Repositories.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
