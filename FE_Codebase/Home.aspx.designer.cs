using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PimsApp.Controllers
{
    [Authorize] // Implement authorization
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Log page access
            _logger.LogInformation("Home page accessed");

            // Pass data to view using a view model
            var viewModel = new HomeViewModel
            {
                WelcomeMessage = "Welcome, " + User.Identity.Name,
                Complaints = GetComplaints()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterComplaint(ComplaintViewModel complaint)
        {
            if (ModelState.IsValid)
            {
                // Asynchronously register the complaint
                await RegisterComplaintAsync(complaint);
                return RedirectToAction(nameof(Index));
            }

            return View(complaint);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        // Other action methods...
    }
}