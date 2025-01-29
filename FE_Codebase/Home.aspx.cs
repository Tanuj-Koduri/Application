using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity; // Modern authentication
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json; // Modern JSON handling

namespace PimsApp
{
    [Authorize] // Modern authorization attribute
    public partial class Home : System.Web.UI.Page
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Home> _logger; // Added logging
        private readonly string _connectionString;

        // Constructor injection for dependencies
        public Home(IConfiguration configuration, ILogger<Home> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _connectionString = _configuration.GetConnectionString("YourConnectionString");
        }

        protected async Task Page_LoadAsync(object sender, EventArgs e) // Made async
        {
            try
            {
                var roles = HttpContext.Session.GetObject<List<string>>("Roles"); // Using extension method

                if (!IsPostBack)
                {
                    if (roles?.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)) ?? false)
                    {
                        await ConfigureUIBasedOnRoleAsync(roles);
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Page_Load");
                // Handle error appropriately
            }
        }

        private async Task ConfigureUIBasedOnRoleAsync(List<string> roles)
        {
            var isAdmin = roles.Contains("Admin");
            var isBoth = roles.Contains("BothRoles");

            // Using pattern matching
            if (gvComplaints.Columns.OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken") is TemplateField actionTakenField)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }

            pageTitle.InnerText = (isAdmin || isBoth) 
                ? "Admin Dashboard - Complaints Management" 
                : "My Complaints";

            gvComplaints.Columns[9].Visible = (isAdmin || isBoth);

            var email = HttpContext.Session.GetString("Email");
            lblWelcome.Text = $"Welcome, {email}!";
            
            await BindComplaintsAsync(); // Made async
            DisplaySuccessMessage();
        }

        private async Task BindComplaintsAsync()
        {
            var roles = HttpContext.Session.GetObject<List<string>>("Roles");
            var email = HttpContext.Session.GetString("Email");

            try
            {
                using var connection = new SqlConnection(_connectionString);
                var query = BuildComplaintsQuery(roles);
                
                using var command = new SqlCommand(query, connection);
                
                if (roles.Contains("NormalUser"))
                {
                    command.Parameters.AddWithValue("@Email", email);
                }

                await connection.OpenAsync();
                
                using var reader = await command.ExecuteReaderAsync();
                var complaints = await ParseComplaintsFromReaderAsync(reader);

                gvComplaints.DataSource = complaints;
                await gvComplaints.DataBindAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error binding complaints");
                // Handle error appropriately
            }
        }

        // New model class using record type
        public record ComplaintViewModel
        {
            public string Id { get; init; }
            public string ComplaintId { get; init; }
            public string Name { get; init; }
            public string EmpId { get; init; }
            public string Email { get; init; }
            public string ContactNumber { get; init; }
            public DateTime DateTimeCapture { get; init; }
            public string PictureCaptureLocation { get; init; }
            public string Comments { get; init; }
            public string[] PictureUploads { get; init; }
            public string Status { get; init; }
            public string CurrentStatus { get; init; }
        }

        // Added security headers
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Response.Headers.Add("X-Frame-Options", "DENY");
            Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
        }
    }
}