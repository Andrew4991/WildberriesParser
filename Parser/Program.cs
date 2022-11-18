using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models.Api;
using Parser.BL.Data.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();

            Work(services);

            Console.ReadKey();
        }

        private static IServiceCollection ConfigureServices()
        {
            //configurations
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();

            var services = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging();

            //options
            services.Configure<ApiOptions>(configuration.GetSection(nameof(ApiOptions)));

            //DI
            services.AddSingleton<IParserService, ApiParserService>();
            services.AddSingleton<IWorkerService, WorkerService>();

            //autoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static void Work(IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var parser = scope.ServiceProvider.GetService<IParserService>();
            var gdfg = scope.ServiceProvider.GetService<IOptions<ApiOptions>>().Value;

            var xxc = parser.GetProductsAsync("Игрушки").GetAwaiter().GetResult();
        }
    }
}
