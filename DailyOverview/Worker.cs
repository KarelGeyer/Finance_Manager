using Common.Models.Email;
using EmailService;

namespace DailyOverview
{
    public class Worker : BackgroundService, IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogger<CommunicationService> _comLogger;

        public Worker(ILogger<Worker> logger, ILogger<CommunicationService> comLogger)
        {
            _logger = logger;
            _comLogger = comLogger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                Email email =
                    new()
                    {
                        Body = "Test",
                        Subject = "Test",
                        To = "karelgeyer@gmail.com",
                    };

                EmailConfigration config =
                    new()
                    {
                        Address = "",
                        Smtp = "",
                        Port = 567,
                        ShouldUseSSL = true,
                        SSL = "",
                        Username = "Test",
                        Password = "Test",
                    };

                CommunicationService service = new(config, _comLogger);
                service.SendEmail(email);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker started at: {DateTimeOffset.Now}");
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Worker ended at: {DateTimeOffset.Now}");
            return base.StopAsync(cancellationToken);
        }
    }
}
