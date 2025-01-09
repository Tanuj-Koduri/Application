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
        private readonly IConfiguration _configuration; // Added for dependency injection

        // Constructor for dependency injection
        public Home(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Use nullable reference types
            List<string>? roles = Session["Roles"] as List<string>;

            if (!IsPostBack)
            {
                // Use pattern matching and null-coalescing operator
                if (roles is { } && roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles"))
                {
                    bool isAdmin = roles.Contains("Admin");
                    bool isBoth = roles.Contains("BothRoles");

                    // Use LINQ with null-conditional operator
                    var actionTakenField = gvComplaints.Columns
                        .OfType<TemplateField>()
                        .FirstOrDefault(f => f.HeaderText == "Action Taken");

                    if (actionTakenField != null)
                    {
                        // Use switch expression
                        actionTakenField.HeaderText = (isAdmin, isBoth) switch
                        {
                            (true, _) => "UpdateProgress",
                            (_, true) => "UpdateProgress",
                            _ => "Current Status"
                        };
                    }

                    // Use string interpolation
                    pageTitle.InnerText = isAdmin || isBoth 
                        ? "Admin Dashboard - Complaints Management" 
                        : "My Complaints";

                    gvComplaints.Columns[9].Visible = isAdmin || isBoth;

                    // Use null-coalescing operator
                    string? email = Session["Email"] as string ?? "Guest";
                    lblWelcome.Text = $"Welcome, {email}!";

                    BindComplaints();
                    DisplaySuccessMessage();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void DisplaySuccessMessage()
        {
            // Use null-conditional operator and null-coalescing operator
            string? successMessage = Session["SuccessMessage"] as string;

            if (!string.IsNullOrEmpty(successMessage))
            {
                lblSucessMessage.Text = successMessage;
                lblSucessMessage.Visible = true;

                Session["SuccessMessage"] = null;
            }
        }

        private void BindComplaints()
        {
            // Use IConfiguration for connection string
            string connectionString = _configuration.GetConnectionString("YourConnectionString");

            List<string>? roles = Session["Roles"] as List<string>;
            string? email = Session["Email"] as string;

            using var conn = new SqlConnection(connectionString);
            string query = roles?.Contains("Admin") ?? false || roles?.Contains("BothRoles") ?? false
                ? "SELECT Id, FirstName + ' ' + LastName AS Name, EmpId, Email, ContactNumber, DateTimeCapture, PictureCaptureLocation + ' ' + StreetAddress1 + ' ' + City + ', ' + Zip + ' ' + State AS PictureCaptureLocation, Comments, PictureUpload, ComplaintId, CurrentStatus, Status FROM Complaints ORDER BY Id DESC"
                : "SELECT Id, FirstName + ' ' + LastName AS Name, EmpId, Email, ContactNumber, DateTimeCapture, PictureCaptureLocation + ' ' + StreetAddress1 + ' ' + City + ', ' + Zip + ' ' + State AS PictureCaptureLocation, Comments, PictureUpload, ComplaintId, CurrentStatus, Status FROM Complaints WHERE Email = @Email ORDER BY Id DESC";

            using var cmd = new SqlCommand(query, conn);
            
            if (roles?.Contains("NormalUser") ?? false)
            {
                cmd.Parameters.AddWithValue("@Email", email ?? string.Empty);
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
                    Name = reader["Name"].ToString(),
                    EmpId = reader["EmpId"].ToString(),
                    Email = reader["Email"].ToString(),
                    ContactNumber = reader["ContactNumber"].ToString(),
                    DateTimeCapture = Convert.ToDateTime(reader["DateTimeCapture"]),
                    PictureCaptureLocation = reader["PictureCaptureLocation"].ToString(),
                    Comments = reader["Comments"].ToString(),
                    Status = reader["Status"].ToString(),
                    PictureUploads = reader["PictureUpload"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(Path.GetFileName).ToArray(),
                    CurrentStatus = reader["CurrentStatus"].ToString(),
                });
            }

            gvComplaints.DataSource = complaints;
            gvComplaints.DataBind();
        }

        // Rest of the code remains largely unchanged, with minor improvements in syntax and null handling
        // ...

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();

            Response.Redirect("Login.aspx");
        }

        protected string GetUserRoleClass()
        {
            return User.IsInRole("Admin") || User.IsInRole("BothRoles") ? "admin" : string.Empty;
        }
    }
}