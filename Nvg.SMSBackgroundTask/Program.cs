using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Nvg.SMSBackgroundTask.Extensions;
using Serilog;
using System;
using System.IO;

namespace Nvg.SMSBackgroundTask
{
    public class Program
    {
        public static readonly string AppName = "SMS";

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateSerilogLogger(configuration);

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        //private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .CaptureStartupErrors(false)
        //        .UseStartup<Startup>()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .ConfigureServices(services => services.AddAutofac())
        //        .UseConfiguration(configuration)
        //        .UseSerilog()
        //        .Build();

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
            .CaptureStartupErrors(false)
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureServices(services => services.AddAutofac())
            .UseConfiguration(configuration);
            //.UseSerilog()
            //.Build();
            if (configuration["logsService"] == "Azure")
            {
                host.ConfigureLogging(logging =>
                {
                    logging.AddApplicationInsights(configuration["ApplicationInsights:InstrumentationKey"]);
                    logging.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information); //#you can set the logLevel here
                });
            }
            else
            {
                host.UseSerilog();
            }
            return host.Build();
        }
        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {
            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                //.WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        //TODO:make console kind of app
        public static IHost CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureAppConfiguration((host, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", optional: true);
                    builder.AddJsonFile($"appsettings.{host.HostingEnvironment.EnvironmentName}.json", optional: true);
                    builder.AddEnvironmentVariables();
                    builder.AddCommandLine(args);
                })
                .ConfigureLogging((host, builder) => builder.UseSerilog(host.Configuration).AddSerilog())
                .Build();

        public static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            return builder.Build();
        }
    }
}
