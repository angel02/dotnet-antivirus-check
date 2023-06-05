using AntivirusCheck.Shared.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AntivirusCheck.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> logger;
        private readonly IPublishEndpoint publishEndpoint;

        public FileController(ILogger<FileController> logger, IPublishEndpoint publishEndpoint)
        {
            this.logger = logger;
            this.publishEndpoint = publishEndpoint;
        }


        [HttpPost(Name = "UploadFile")]
        public async Task<string> Post(IFormFile file)
        {
            var filesPath = Environment.GetEnvironmentVariable("FilePath");
            var fileName = Guid.NewGuid().ToString();
            var fullPath = Path.Combine(filesPath, fileName);

            using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            await file.CopyToAsync(fileStream);

            await this.publishEndpoint.Publish(new FileToScan
            {
                FullPath = fullPath,
                FileName = fileName
            });

            return "Uploaded";
        }
    }
}