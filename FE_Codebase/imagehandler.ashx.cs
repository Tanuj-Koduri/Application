using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PimsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ImageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string imageName)
        {
            // Use IConfiguration instead of ConfigurationManager
            string basePath = _configuration["ImagePath"];
            
            // Use Path.Combine for cross-platform compatibility
            string filePath = Path.Combine(basePath, imageName);

            // Validate file path to prevent directory traversal attacks
            if (!filePath.StartsWith(basePath, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid image name");
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

            // Use FileStreamResult for better performance
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }

        private static string GetContentType(string extension)
        {
            // Use switch expression for more concise code
            return extension switch
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
}