using log4net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace PingMyVM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GlobalContext.Properties["LogFilePath"] = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "PingMyVM.log");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    builder.ClearProviders();
                    builder.AddProvider(new Log4NetProvider("log4net.config"));
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
    }
}
