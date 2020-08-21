using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PingMyVM
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(60000, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            PowerHelper.ForceSystemAwake();
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            PowerHelper.ResetSystemDefault();
            await base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            // DO YOUR STUFF HERE
        }
    }
}
