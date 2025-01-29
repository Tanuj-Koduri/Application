using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PimsApp
{
    public partial class Home : System.Web.UI.Page
    {
        // Added dependency injection for configuration
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        // Constructor with dependency injection
        public Home(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("YourConnectionString");
        }

        protected async Task Page_Load(object sender, EventArgs e)
        {
            // Using strongly typed session management
            var roles = HttpContext.Session.Get<List<string>>("Roles");

            if (!IsPostBack)
            {
                await InitializePageAsync(roles);
            }
        }

        // Separated page initialization logic
        private async Task InitializePageAsync(List<string> roles)
        {
            if (roles?.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)) == true)
            {
                await ConfigureUIBasedOnRoles(roles);
            }
            else
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        // Using async/await for database operations
        private async Task BindComplaintsAsync()
        {
            var roles = HttpContext.Session.Get<List<string>>("Roles");
            var email = HttpContext.Session.GetString("Email");

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(GetComplaintsQuery(roles), connection);

            if (roles.Contains("NormalUser"))
            {
                command.Parameters.AddWithValue("@Email", email);
            }

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            
            var complaints = new List<ComplaintViewModel>();
            while (await reader.ReadAsync())
            {
                complaints.Add(MapComplaintFromReader(reader));
            }

            gvComplaints.DataSource = complaints;
            await gvComplaints.DataBindAsync();
        }

        // Using record for immutable data
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

        // Using secure string interpolation
        protected async Task UpdateComplaintStatusAsync(string complaintId, string status)
        {
            const string query = "UPDATE Complaints SET Status = @Status WHERE ComplaintId = @ComplaintId";

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);
            
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@ComplaintId", complaintId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        // Using modern authentication
        protected async Task btnLogout_ClickAsync(object sender, EventArgs e)
        {
            await HttpContext.SignOutAsync();
            Response.Redirect("Login.aspx", true);
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