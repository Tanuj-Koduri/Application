using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PimsApp
{
    // Modernized: Using ASP.NET Core attributes for routing
    [Route("api/[controller]")]
    [ApiController]
    public class ImageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Modernized: Using dependency injection for configuration
        public ImageHandlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Modernized: Using async/await for better performance
        [HttpGet]
        public async Task<IActionResult> GetImage([FromQuery] string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return BadRequest("Image name is required.");
            }

            // Modernized: Using configuration to get base path
            string basePath = _configuration["ImagePath"];
            string filePath = Path.Combine(basePath, imageName);

            // Modernized: Using Path.Combine for cross-platform compatibility
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            // Modernized: Using Path.GetExtension for safer extension extraction
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = GetContentType(extension);

            if (contentType == null)
            {
                return StatusCode(415, "Unsupported image type");
            }

            // Modernized: Using FileStreamResult for more efficient file serving
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }

        // Modernized: Using a more concise switch expression
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