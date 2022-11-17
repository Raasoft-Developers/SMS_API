using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Newtonsoft.Json;
using API.SMS.Extensions;
using SMSService.Data;
using Serilog;
using System;
using System.IO;

namespace API.SMS
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.') + 1);

        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = CreateLogger(configuration);

            try
            {
                var host = CreateHostBuilder(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);

                Log.Information("Seeding database...");

                string path = Directory.GetCurrentDirectory();

                host.MigrateDbContext<SMSDbContext>((context, services) =>
                {
                    var smsPoolsDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "SMSPool.json"));
                    dynamic smsPoolsData = JsonConvert.DeserializeObject<object>(smsPoolsDataFromJson);
                    var smsProviderDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "SMSProvider.json"));
                    dynamic smsProviderData = JsonConvert.DeserializeObject<object>(smsProviderDataFromJson);
                    var smsChannelsDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "SMSChannel.json"));
                    dynamic smsChannelsData = JsonConvert.DeserializeObject<object>(smsChannelsDataFromJson);
                    var smsTemplatesDataFromJson = File.ReadAllText(Path.Combine(path, "AppData", "SMSTemplate.json"));
                    dynamic smsTemplatesData = JsonConvert.DeserializeObject<object>(smsTemplatesDataFromJson);
                    new SMSDbContextSeed()
                        .SeedAsync(context, smsPoolsData, smsProviderData, smsChannelsData, smsTemplatesData)
                        .Wait();
                });
                Log.Information("Starting web host ({ApplicationContext})...", AppName);
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        
        private static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services.AddAutofac())
            .UseConfiguration(configuration)
            .CaptureStartupErrors(false)
            .UseStartup<Startup>();
            
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
        private static Serilog.ILogger CreateLogger(IConfiguration configuration)
        {
            

            var seqServerUrl = configuration["Serilog:SeqServerUrl"];
            var logstashUrl = configuration["Serilog:LogstashgUrl"];
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", AppName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
                .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
                    
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static IConfiguration GetConfiguration()
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
