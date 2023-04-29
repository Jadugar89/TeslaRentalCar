using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RentTeslaServer.DomainLayer.Contracts;

namespace DomainLayer.HostedServices
{
    public class PurgeReservationHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<PurgeReservationHostedService> _logger;
        private readonly IServiceProvider _services;
        private Timer? _timer = null;

        public PurgeReservationHostedService(ILogger<PurgeReservationHostedService> logger,
                                             IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Purge Reservation Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromHours(1));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            _logger.LogInformation(
                "Timed Hosted Service is working.");

            var scope = _services.CreateScope();

                scope.ServiceProvider
                    .GetRequiredService<IPurgeReservationService>().MoveToHistory();
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

    }
}
