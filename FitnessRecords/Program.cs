using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace FitnessRecords
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.Sentry("https://074d9f90cfc44b52a2d3c1d1d604842b@o572490.ingest.sentry.io/5721941")
                        .WriteTo.Console()
                        .Enrich.FromLogContext()
                        .CreateLogger();

                    webBuilder.UseSentry();
                    webBuilder.ConfigureLogging((context, loggingBuilder) =>
                    {
                        loggingBuilder.AddSerilog();
                    });
                    webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
