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
        public async Task<IActionResult> Get(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return BadRequest("Image name is required");
            }

            // Use configuration to get base path
            string basePath = _configuration["ImagePath"];
            string filePath = Path.Combine(basePath, imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            // Sanitize file path to prevent directory traversal attacks
            if (!filePath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid image path");
            }

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = GetContentType(extension);

            if (contentType == null)
            {
                return StatusCode(415, "Unsupported image type");
            }

            // Use FileStreamResult for better performance
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }

        // Using a dictionary for better performance
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

        private static string GetContentType(string extension)
        {
            return ContentTypes.TryGetValue(extension, out string contentType) ? contentType : null;
        }
    }
}