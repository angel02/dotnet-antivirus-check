using AntivirusCheck.Shared.Contracts;
using MassTransit;
using nClam;

namespace AntivirusCheck.Worker.Consumers
{
    public class FileToScanConsumer : IConsumer<FileToScan>
    {
        private readonly ILogger<FileToScanConsumer> logger;

        public FileToScanConsumer(ILogger<FileToScanConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<FileToScan> context)
        {
            var fileToScanData = context.Message;
            var address = Environment.GetEnvironmentVariable("ClamAV_Address");
            var port = int.Parse(Environment.GetEnvironmentVariable("ClamAV_Port"));

            var clamav = new ClamClient(address, port);
            var result = await clamav.SendAndScanFileAsync(fileToScanData.FullPath);

            var fileScanResponse = new FileScanResult
            {
                FileName = fileToScanData.FileName,
                FullPath = fileToScanData.FullPath
            };

            fileScanResponse.Message = result.RawResult;

            switch (result.Result)
            {
                case ClamScanResults.Clean:
                    logger.LogInformation("File clean");
                    fileScanResponse.IsClean = true;
                    break;
                case ClamScanResults.VirusDetected:
                    logger.LogWarning("Malware detected !!");
                    fileScanResponse.IsClean = false;
                    break;
                case ClamScanResults.Error:
                    logger.LogError("There was an error while scanning the file");
                    break;
                default:
                    logger.LogInformation("Something else happened");
                    break;
            }
        }
    }
}