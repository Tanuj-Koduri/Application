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

        public Home(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var roles = Session["Roles"] as List<string>;

            if (!IsPostBack)
            {
                if (roles?.Intersect(new[] { "Admin", "NormalUser", "BothRoles" }).Any() == true)
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

            SetActionTakenFieldHeader(isAdmin, isBoth);
            SetPageTitle(isAdmin, isBoth);
            SetColumnVisibility(roles);
            SetWelcomeMessage();
        }

        private void SetActionTakenFieldHeader(bool isAdmin, bool isBoth)
        {
            var actionTakenField = gvComplaints.Columns
                .OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken");

            if (actionTakenField != null)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }
        }

        private void SetPageTitle(bool isAdmin, bool isBoth)
        {
            pageTitle.InnerText = (isAdmin || isBoth) ? "Admin Dashboard - Complaints Management" : "My Complaints";
        }

        private void SetColumnVisibility(List<string> roles)
        {
            gvComplaints.Columns[9].Visible = roles.Contains("Admin") || roles.Contains("BothRoles");
        }

        private void SetWelcomeMessage()
        {
            string email = Session["Email"] as string;
            lblWelcome.Text = $"Welcome, {email}!";
        }

        private void DisplaySuccessMessage()
        {
            string successMessage = Session["SuccessMessage"] as string;
            if (!string.IsNullOrEmpty(successMessage))
            {
                lblSucessMessage.Text = successMessage;
                lblSucessMessage.Visible = true;
                Session["SuccessMessage"] = null;
            }
        }

        private void BindComplaints()
        {
            string connectionString = _configuration.GetConnectionString("YourConnectionString");
            List<string> roles = Session["Roles"] as List<string>;
            string email = Session["Email"] as string;

            using (var conn = new SqlConnection(connectionString))
            {
                string query = GetComplaintsQuery(roles);
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (roles.Contains("NormalUser"))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                    }
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        var complaints = new List<ComplaintViewModel>();
                        while (reader.Read())
                        {
                            complaints.Add(CreateComplaintFromReader(reader));
                        }
                        gvComplaints.DataSource = complaints;
                        gvComplaints.DataBind();
                    }
                }
            }
        }

        private string GetComplaintsQuery(List<string> roles)
        {
            const string baseQuery = @"
                SELECT Id, FirstName + ' ' + LastName AS Name, EmpId, Email, ContactNumber, 
                       DateTimeCapture, PictureCaptureLocation + ' ' + StreetAddress1 + ' ' + City + ', ' + Zip + ' ' + State AS PictureCaptureLocation, 
                       Comments, PictureUpload, ComplaintId, CurrentStatus, Status 
                FROM Complaints";

            return roles.Contains("Admin") || roles.Contains("BothRoles")
                ? $"{baseQuery} ORDER BY Id DESC"
                : $"{baseQuery} WHERE Email = @Email ORDER BY Id DESC";
        }

        private ComplaintViewModel CreateComplaintFromReader(SqlDataReader reader)
        {
            return new ComplaintViewModel
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
                PictureUploads = reader["PictureUpload"].ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(Path.GetFileName).ToArray(),
                CurrentStatus = reader["CurrentStatus"].ToString(),
            };
        }

        // ... (rest of the code remains largely the same, with minor adjustments for consistency and modern practices)
    }
}