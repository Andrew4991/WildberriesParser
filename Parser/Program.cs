using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Parser.BL.Data.Exceptions;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models.Api;
using Parser.BL.Data.Models.Options;
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
            try
            {
                Console.WriteLine("Программа запущена");

                var services = ConfigureServices();

                Work(services);

                Console.WriteLine("Программа завершила работу");
            }            
            catch (InputFileException e)
            {
                Console.WriteLine("Произошла ошибка при чтении входного файла");
            }
            catch (OutputFileException e)
            {
                Console.WriteLine("Произошла ошибка при работе с выходным файлом");
            }
            catch (ParserException e)
            {
                Console.WriteLine("Произошла ошибка при работе парсера");
            }
            catch (ApiErrorException e)
            {
                Console.WriteLine($"Произошла ошибка получения данных: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла неизвестная ошибка");
            }

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
            services.Configure<ProjectOptions>(configuration.GetSection(nameof(ProjectOptions)));
            var projectOptions = configuration.GetSection(nameof(ProjectOptions)).Get<ProjectOptions>();

            //DI
            //check type of parser
            if (projectOptions.UseApiParser)
            {
                services.AddSingleton<IParserService, ApiParserService>();
            }
            else
            {
                services.AddSingleton<IParserService, HtmlParserService>();
            }

            services.AddSingleton<IWorkerService, WorkerService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IExcelService, ExcelService>();

            //autoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        private static void Work(IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var worker = scope.ServiceProvider.GetService<IWorkerService>();

            worker.RunAsync().GetAwaiter().GetResult();
        }
    }
}
