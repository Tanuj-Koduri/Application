using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PimsApp
{
    [ApiController]
    [Route("[controller]")]
    public class ImageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImageHandlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string imageName)
        {
            // Use IConfiguration instead of ConfigurationManager
            string basePath = _configuration["ImagePath"];
            string filePath = Path.Combine(basePath, imageName);

            // Use Path.Combine for better cross-platform support
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            // Use FileExtensionContentTypeProvider for more robust content type detection
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out string contentType))
            {
                return StatusCode(415, "Unsupported image type");
            }

            // Use FileStreamResult for better performance with large files
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }
    }
}