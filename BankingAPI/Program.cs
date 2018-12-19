using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BankingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("/configmaps/appsettings.json", optional: true)
                .Build();

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            Log.Logger = loggerConfiguration.CreateLogger();

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseConfiguration(config)
                .UseSerilog()
                .Build();
        }

    }
}
