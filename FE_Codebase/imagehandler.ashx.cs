using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PimsApp
{
    // Modernized: Changed to ASP.NET Core API Controller
    [ApiController]
    [Route("[controller]")]
    public class ImageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Modernized: Constructor injection for IConfiguration
        public ImageHandlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Modernized: Async method, using IActionResult for better response handling
        [HttpGet]
        public async Task<IActionResult> GetImage([FromQuery] string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                return BadRequest("Image name is required.");
            }

            // Modernized: Using GetValue<string> for type-safe configuration access
            string basePath = _configuration.GetValue<string>("ImagePath");
            
            // Modernized: Using Path.Combine for safe path concatenation
            string filePath = Path.Combine(basePath, imageName);

            // Security: Prevent path traversal attacks
            if (!filePath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid image path.");
            }

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = GetContentType(extension);

            if (contentType == null)
            {
                return StatusCode(415, "Unsupported image type");
            }

            // Modernized: Using FileStreamResult for efficient file streaming
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }

        // Modernized: Using switch expression for more concise code
        private static string GetContentType(string extension) => extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".tiff" or ".tif" => "image/tiff",
            ".ico" => "image/x-icon",
            ".svg" => "image/svg+xml",
            ".jfif" => "image/jfif",
            _ => null
        };
    }
}