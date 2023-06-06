using AntivirusCheck.Shared.Contracts;
using MassTransit;

namespace AntivirusCheck.Api.Consumers
{
    public class FileScanResultConsumer : IConsumer<FileScanResult>
    {
        private readonly ILogger<FileScanResultConsumer> logger;

        public FileScanResultConsumer(ILogger<FileScanResultConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<FileScanResult> context)
        {
            logger.LogInformation(context.Message.Message);
        }
    }
}
