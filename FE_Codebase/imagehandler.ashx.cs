using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PimsApp
{
    // Converted to ASP.NET Core API controller
    [ApiController]
    [Route("api/[controller]")]
    public class ImageHandlerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Dependency injection for IConfiguration
        public ImageHandlerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Converted to async method
        [HttpGet]
        public async Task<IActionResult> GetImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return BadRequest("Image name is required.");
            }

            // Use IConfiguration to get the base path
            string basePath = _configuration["ImagePath"];
            string filePath = Path.Combine(basePath, imageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image not found");
            }

            // Get file extension and content type
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = GetContentType(extension);

            if (contentType == null)
            {
                return StatusCode(StatusCodes.Status415UnsupportedMediaType, "Unsupported image type");
            }

            // Use FileStreamResult for better performance
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(fileStream, contentType);
        }

        // Updated content type dictionary
        private static readonly Dictionary<string, string> ContentTypes = new Dictionary<string, string>
        {
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".png", "image/png"},
            {".gif", "image/gif"},
            {".bmp", "image/bmp"},
            {".tiff", "image/tiff"},
            {".tif", "image/tiff"},
            {".ico", "image/x-icon"},
            {".svg", "image/svg+xml"},
            {".jfif", "image/jfif"}
        };

        // Simplified GetContentType method
        private static string GetContentType(string extension)
        {
            return ContentTypes.TryGetValue(extension, out string contentType) ? contentType : null;
        }
    }
}