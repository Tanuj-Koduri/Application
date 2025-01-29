using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using Microsoft.Extensions.Configuration; // Added for modern configuration management

namespace PimsApp
{
    public partial class Home : Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration

        // Constructor injection for IConfiguration
        public Home(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Use pattern matching and null-coalescing operator
                if (Session["Roles"] is List<string> roles && roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles"))
                {
                    SetupPageBasedOnRole(roles);
                    BindComplaints();
                    DisplaySuccessMessage();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void SetupPageBasedOnRole(List<string> roles)
        {
            bool isAdmin = roles.Contains("Admin");
            bool isBoth = roles.Contains("BothRoles");

            // Use nullable reference types
            TemplateField? actionTakenField = gvComplaints.Columns
                .OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken");

            if (actionTakenField != null)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }

            pageTitle.InnerText = (isAdmin || isBoth) ? "Admin Dashboard - Complaints Management" : "My Complaints";
            gvComplaints.Columns[9].Visible = isAdmin || isBoth;

            // Use string interpolation
            lblWelcome.Text = $"Welcome, {Session["Email"] as string ?? "User"}!";
        }

        private void DisplaySuccessMessage()
        {
            if (Session["SuccessMessage"] is string successMessage)
            {
                lblSucessMessage.Text = successMessage;
                lblSucessMessage.Visible = true;
                Session["SuccessMessage"] = null;
            }
        }

        private void BindComplaints()
        {
            string connectionString = _configuration.GetConnectionString("YourConnectionString");
            List<string> roles = Session["Roles"] as List<string> ?? new List<string>();
            string email = Session["Email"] as string ?? string.Empty;

            using var conn = new SqlConnection(connectionString);
            string query = roles.Contains("Admin") || roles.Contains("BothRoles")
                ? "SELECT * FROM Complaints ORDER BY Id DESC"
                : "SELECT * FROM Complaints WHERE Email = @Email ORDER BY Id DESC";

            using var cmd = new SqlCommand(query, conn);
            if (roles.Contains("NormalUser"))
            {
                cmd.Parameters.AddWithValue("@Email", email);
            }

            conn.Open();
            using var reader = cmd.ExecuteReader();
            var complaints = new List<ComplaintViewModel>();

            while (reader.Read())
            {
                complaints.Add(new ComplaintViewModel
                {
                    Id = reader["Id"].ToString(),
                    ComplaintId = reader["ComplaintId"].ToString(),
                    Name = $"{reader["FirstName"]} {reader["LastName"]}",
                    EmpId = reader["EmpId"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNumber = reader["ContactNumber"].ToString(),
                    DateTimeCapture = Convert.ToDateTime(reader["DateTimeCapture"]),
                    PictureCaptureLocation = $"{reader["PictureCaptureLocation"]} {reader["StreetAddress1"]} {reader["City"]}, {reader["Zip"]} {reader["State"]}",
                    Comments = reader["Comments"].ToString(),
                    Status = reader["Status"].ToString(),
                    PictureUploads = reader["PictureUpload"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries),
                    CurrentStatus = reader["CurrentStatus"].ToString(),
                });
            }

            gvComplaints.DataSource = complaints;
            gvComplaints.DataBind();
        }

        // ... (rest of the code remains largely unchanged)

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected string GetUserRoleClass() => User.IsInRole("Admin") || User.IsInRole("BothRoles") ? "admin" : string.Empty;
    }
}