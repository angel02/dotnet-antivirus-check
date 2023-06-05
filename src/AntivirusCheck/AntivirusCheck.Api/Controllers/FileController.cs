using Microsoft.AspNetCore.Mvc;

namespace AntivirusCheck.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "UploadFile")]
        public string Post(IFormFile file)
        {
            return "Uploaded";
        }
    }
}