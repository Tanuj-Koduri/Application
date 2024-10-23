using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace PimsApp
{
    public partial class Home : Page
    {
        private readonly IConfiguration _configuration;

        public Home(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            // Use nullable reference types
            List<string>? roles = Session["Roles"] as List<string>;

            if (!IsPostBack)
            {
                // Use pattern matching and null-coalescing operator
                if (roles is { Count: > 0 } && roles.Intersect(new[] { "Admin", "NormalUser", "BothRoles" }).Any())
                {
                    SetupUI(roles);
                    await BindComplaintsAsync(); // Changed to async
                    DisplaySuccessMessage();
                }
                else
                {
                    Response.Redirect("Login.aspx", true);
                }
            }
        }

        private void SetupUI(List<string> roles)
        {
            bool isAdmin = roles.Contains("Admin");
            bool isBoth = roles.Contains("BothRoles");

            // Use LINQ for better readability
            var actionTakenField = gvComplaints.Columns
                .OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken");

            if (actionTakenField != null)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }

            pageTitle.InnerText = (isAdmin || isBoth) ? "Admin Dashboard - Complaints Management" : "My Complaints";
            gvComplaints.Columns[9].Visible = (isAdmin || isBoth);

            // Use null-coalescing operator and string interpolation
            string email = Session["Email"] as string ?? "Guest";
            lblWelcome.Text = $"Welcome, {email}!";
        }

        private void DisplaySuccessMessage()
        {
            // Use null-coalescing operator
            string? successMessage = Session["SuccessMessage"] as string;

            if (!string.IsNullOrEmpty(successMessage))
            {
                lblSucessMessage.Text = successMessage;
                lblSucessMessage.Visible = true;
                Session["SuccessMessage"] = null;
            }
        }

        private async Task BindComplaintsAsync() // Changed to async
        {
            // Use configuration injection instead of ConfigurationManager
            string connectionString = _configuration.GetConnectionString("YourConnectionString");

            List<string>? roles = Session["Roles"] as List<string>;
            string? email = Session["Email"] as string;

            using var conn = new SqlConnection(connectionString);
            string query = GetComplaintsQuery(roles);
            using var cmd = new SqlCommand(query, conn);
            if (roles?.Contains("NormalUser") == true)
            {
                cmd.Parameters.AddWithValue("@Email", email ?? "");
            }
            await conn.OpenAsync(); // Changed to async
            using var reader = await cmd.ExecuteReaderAsync(); // Changed to async
            gvComplaints.DataSource = await ReadComplaintsAsync(reader); // Changed to async
            gvComplaints.DataBind();
        }

        private static string GetComplaintsQuery(List<string>? roles) =>
            (roles?.Contains("Admin") == true || roles?.Contains("BothRoles") == true)
                ? "SELECT Id, FirstName + ' ' + LastName AS Name, EmpId, Email, ContactNumber, DateTimeCapture, PictureCaptureLocation + ' ' + StreetAddress1 + ' ' + City + ', ' + Zip + ' ' + State AS PictureCaptureLocation, Comments, PictureUpload, ComplaintId, CurrentStatus, Status FROM Complaints ORDER BY Id DESC"
                : "SELECT Id, FirstName + ' ' + LastName AS Name, EmpId, Email, ContactNumber, DateTimeCapture, PictureCaptureLocation + ' ' + StreetAddress1 + ' ' + City + ', ' + Zip + ' ' + State AS PictureCaptureLocation, Comments, PictureUpload, ComplaintId, CurrentStatus, Status FROM Complaints WHERE Email = @Email ORDER BY Id DESC";

        private static async Task<List<ComplaintViewModel>> ReadComplaintsAsync(SqlDataReader reader) // Changed to async
        {
            var complaints = new List<ComplaintViewModel>();
            while (await reader.ReadAsync()) // Changed to async
            {
                complaints.Add(new ComplaintViewModel
                {
                    Id = reader["Id"].ToString(),
                    ComplaintId = reader["ComplaintId"].ToString(),
                    Name = reader["Name"].ToString(),
                    EmpId = reader["EmpId"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNumber = reader["ContactNumber"].ToString(),
                    DateTimeCapture = Convert.ToDateTime(reader["DateTimeCapture"]),
                    PictureCaptureLocation = reader["PictureCaptureLocation"].ToString(),
                    Comments = reader["Comments"].ToString(),
                    Status = reader["Status"].ToString(),
                    PictureUploads = reader["PictureUpload"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(System.IO.Path.GetFileName).ToArray(),
                    CurrentStatus = reader["CurrentStatus"].ToString(),
                });
            }
            return complaints;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx", true);
        }

        protected string GetUserRoleClass() =>
            (User.IsInRole("Admin") || User.IsInRole("BothRoles")) ? "admin" : "";
    }
}