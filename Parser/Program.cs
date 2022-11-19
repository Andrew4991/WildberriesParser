﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Parser.BL.Data.Exceptions;
using Parser.BL.Data.Interfaces;
using Parser.BL.Data.Models.Options;
using Parser.BL.Data.Services;
using System;
using System.IO;

namespace Parser
{
    class Program
    {
        /// <summary>
        /// Start programm
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Logger log = LogManager.GetCurrentClassLogger();

            try
            {
                Console.WriteLine("Программа запущена");

                var services = ConfigureServices();

                Work(services);                             
            }            
            catch (InputFileException e)
            {
                log.Error(e.StackTrace);
                Console.WriteLine("Произошла ошибка при чтении входного файла");
            }
            catch (OutputFileException e)
            {
                log.Error(e.StackTrace);
                Console.WriteLine("Произошла ошибка при работе с выходным файлом");
            }
            catch (ParserException e)
            {
                log.Error(e.StackTrace);
                Console.WriteLine("Произошла ошибка при работе парсера");
            }
            catch (ApiErrorException e)
            {
                log.Error(e.StackTrace);
                Console.WriteLine($"Произошла ошибка получения данных: {e.Message}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                Console.WriteLine("Произошла неизвестная ошибка");
            }

            Console.WriteLine("Программа завершила работу");
            Console.ReadKey();
        }

        /// <summary>
        /// Startup. Inject DI
        /// </summary>
        /// <returns></returns>
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

            //autoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

        /// <summary>
        /// run worker
        /// </summary>
        /// <param name="services"> need to get service from DI</param>
        private static void Work(IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var worker = scope.ServiceProvider.GetService<IWorkerService>();

            worker.RunAsync().GetAwaiter().GetResult();
        }
    }
}
