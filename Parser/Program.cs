using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parser.BL.Data.Interfaces;
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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();

            var services = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging();

            services.AddTransient<IParserService, ApiParserService>();

            return services;
        }

        private static void Work(IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var parser = scope.ServiceProvider.GetService<IParserService>();

            var xxc = parser.GetProductsAsync("sd").GetAwaiter().GetResult();
        }
    }
}
