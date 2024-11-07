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
            if (!IsPostBack)
            {
                var roles = Session["Roles"] as List<string>;

                if (roles?.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)) == true)
                {
                    SetupUserInterface(roles);
                    BindComplaints();
                    DisplaySuccessMessage();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void SetupUserInterface(List<string> roles)
        {
            bool isAdmin = roles.Contains("Admin");
            bool isBoth = roles.Contains("BothRoles");

            var actionTakenField = gvComplaints.Columns
                .OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken");

            if (actionTakenField != null)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }

            pageTitle.InnerText = (isAdmin || isBoth) ? "Admin Dashboard - Complaints Management" : "My Complaints";
            gvComplaints.Columns[9].Visible = isAdmin || isBoth;

            string email = Session["Email"] as string;
            lblWelcome.Text = $"Welcome, {email}!";
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
            var roles = Session["Roles"] as List<string>;
            string email = Session["Email"] as string;

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(GetComplaintsQuery(roles), conn);

            if (!roles.Contains("Admin") && !roles.Contains("BothRoles"))
            {
                cmd.Parameters.AddWithValue("@Email", email);
            }

            conn.Open();
            using var reader = cmd.ExecuteReader();
            var complaints = new List<ComplaintViewModel>();

            while (reader.Read())
            {
                complaints.Add(CreateComplaintViewModel(reader));
            }

            gvComplaints.DataSource = complaints;
            gvComplaints.DataBind();
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

        private ComplaintViewModel CreateComplaintViewModel(SqlDataReader reader)
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
                    .Select(System.IO.Path.GetFileName).ToArray(),
                CurrentStatus = reader["CurrentStatus"].ToString(),
            };
        }

        // Other methods (gvComplaints_RowDataBound, gvComplaints_RowCommand, etc.) remain largely unchanged
        // but can be refactored for better separation of concerns and reduced complexity

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected string GetUserRoleClass() => User.IsInRole("Admin") || User.IsInRole("BothRoles") ? "admin" : string.Empty;
    }
}