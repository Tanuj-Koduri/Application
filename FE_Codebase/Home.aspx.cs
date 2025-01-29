using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity; // Modern authentication
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc; // Modern web framework

namespace PimsApp
{
    // Modernized to use ASP.NET Core MVC pattern
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IComplaintService _complaintService; // Added service layer

        // Constructor injection for dependencies
        public HomeController(
            IConfiguration configuration,
            ILogger<HomeController> logger,
            IComplaintService complaintService)
        {
            _configuration = configuration;
            _logger = logger;
            _complaintService = complaintService;
        }

        // Async/await pattern for better performance
        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = HttpContext.Session.GetObject<List<string>>("Roles");
                var email = HttpContext.Session.GetString("Email");

                if (!roles?.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)) ?? true)
                {
                    return RedirectToAction("Login", "Account");
                }

                var viewModel = new HomeViewModel
                {
                    IsAdmin = roles.Contains("Admin"),
                    IsBoth = roles.Contains("BothRoles"),
                    Email = email,
                    Complaints = await _complaintService.GetComplaintsAsync(roles, email)
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");
                return RedirectToAction("Error", "Home");
            }
        }

        // Modernized complaint binding with async/await
        private async Task<List<ComplaintViewModel>> BindComplaintsAsync(List<string> roles, string email)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            using var connection = new SqlConnection(connectionString);
            var query = BuildComplaintQuery(roles);
            
            await using var command = new SqlCommand(query, connection);
            
            if (!roles.Contains("Admin") && !roles.Contains("BothRoles"))
            {
                command.Parameters.AddWithValue("@Email", email);
            }

            await connection.OpenAsync();
            
            using var reader = await command.ExecuteReaderAsync();
            return await MapComplaintsFromReader(reader);
        }

        // Modernized complaint model
        public class ComplaintViewModel
        {
            public string Id { get; set; }
            public string ComplaintId { get; set; }
            public string Name { get; set; }
            public string EmpId { get; set; }
            public string Email { get; set; }
            public string ContactNumber { get; set; }
            public DateTime DateTimeCapture { get; set; }
            public string PictureCaptureLocation { get; set; }
            public string Comments { get; set; }
            public IEnumerable<string> PictureUploads { get; set; }
            public string Status { get; set; }
            public string CurrentStatus { get; set; }
        }

        // Added security for status updates
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(string complaintId, string status)
        {
            try
            {
                if (!User.IsInRole("Admin") && !User.IsInRole("BothRoles"))
                {
                    return Forbid();
                }

                await _complaintService.UpdateStatusAsync(complaintId, status);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating complaint status");
                return BadRequest();
            }
        }

        // Secure logout implementation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}