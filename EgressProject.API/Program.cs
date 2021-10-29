using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;

namespace EgressProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                
                // Configure Serilog with PostgreSQL
                .ConfigureAppConfiguration((HostingAbstractionsHostExtensions, config) => {
                    var configurationRoot = config.Build();

                    Serilog.Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.PostgreSQL(
                            connectionString: configurationRoot.GetConnectionString("EgressDbPostgreSQL"), 
                            tableName: "Logs",
                            needAutoCreateTable: true
                            ).CreateLogger();
                })
                .UseSerilog()

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
