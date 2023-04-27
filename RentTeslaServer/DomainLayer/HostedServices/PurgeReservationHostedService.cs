using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DomainLayer.Contracts;

namespace DomainLayer.HostedServices
{
    public class PurgeReservationHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<PurgeReservationHostedService> logger;
        private readonly IServiceProvider services;
        private Timer? timer = null;

        public PurgeReservationHostedService(ILogger<PurgeReservationHostedService> logger,
                                             IServiceProvider services)
        {
            this.logger = logger;
            this.services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Purge Reservation Service running.");

            timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            logger.LogInformation(
                "Timed Hosted Service is working.");

            var scope = services.CreateScope();

            var purgeReservationService =
                scope.ServiceProvider
                    .GetRequiredService<IPurgeReservationService>().MoveToHistory();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Timed Hosted Service is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

    }
}
