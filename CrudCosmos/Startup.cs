using CrudCosmos;
using CrudCosmos.Models;
using CrudCosmos.Repositories;
using CrudCosmos.Repositories.Interfaces;
using CrudCosmos.Services;
using CrudCosmos.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]

namespace CrudCosmos
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services).BuildServiceProvider(true);
        }

        private IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            services
                .AddSingleton(new FunctionConfiguration(config));

            services
                .AddDbContext<CosmosContext>();

            services
                .AddScoped<IRepository<Book>, BookRepository>()
                .AddScoped<IBookService, BookService>();

            return services;
        }
    }
}