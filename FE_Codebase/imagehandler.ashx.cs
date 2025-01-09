using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PimsApp
{
    // Updated to use ASP.NET Core
    [ApiController]
    [Route("[controller]")]
    public class ImageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Dependency injection for configuration
        public ImageHandlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Async method for better performance
        [HttpGet]
        public async Task<IActionResult> GetImage(string imageName)
        {
            // Use Path.Combine for cross-platform compatibility
            string basePath = _configuration["ImagePath"];
            string filePath = Path.Combine(basePath, imageName);

            // Use FileInfo for better performance and security
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                return NotFound("Image not found");
            }

            // Validate file extension
            string extension = fileInfo.Extension.ToLowerInvariant();
            string contentType = GetContentType(extension);

            if (contentType == null)
            {
                return StatusCode(415, "Unsupported image type");
            }

            // Use FileStreamResult for efficient file streaming
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
            return new FileStreamResult(stream, contentType);
        }

        // Use a dictionary for better performance
        private static readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".png"] = "image/png",
            [".gif"] = "image/gif",
            [".bmp"] = "image/bmp",
            [".tiff"] = "image/tiff",
            [".tif"] = "image/tiff",
            [".ico"] = "image/x-icon",
            [".svg"] = "image/svg+xml",
            [".jfif"] = "image/jfif"
        };

        private string GetContentType(string extension)
        {
            return ContentTypes.TryGetValue(extension, out string contentType) ? contentType : null;
        }
    }
}