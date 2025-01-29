using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
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
                // Use HttpContext.Current.Session instead of Session for better testability
                var roles = HttpContext.Current.Session["Roles"] as List<string>;

                if (roles?.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)) == true)
                {
                    SetupUIBasedOnRole(roles);
                    BindComplaints();
                    DisplaySuccessMessage();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void SetupUIBasedOnRole(List<string> roles)
        {
            bool isAdmin = roles.Contains("Admin");
            bool isBoth = roles.Contains("BothRoles");

            // Use var for improved readability
            var actionTakenField = gvComplaints.Columns
                .OfType<TemplateField>()
                .FirstOrDefault(f => f.HeaderText == "Action Taken");

            if (actionTakenField != null)
            {
                actionTakenField.HeaderText = (isAdmin || isBoth) ? "UpdateProgress" : "Current Status";
            }

            pageTitle.InnerText = (isAdmin || isBoth) ? "Admin Dashboard - Complaints Management" : "My Complaints";
            gvComplaints.Columns[9].Visible = isAdmin || isBoth;

            var email = HttpContext.Current.Session["Email"] as string;
            lblWelcome.Text = $"Welcome, {email}!";
        }

        private void DisplaySuccessMessage()
        {
            var successMessage = HttpContext.Current.Session["SuccessMessage"] as string;
            if (!string.IsNullOrEmpty(successMessage))
            {
                lblSucessMessage.Text = successMessage;
                lblSucessMessage.Visible = true;
                HttpContext.Current.Session["SuccessMessage"] = null;
            }
        }

        private void BindComplaints()
        {
            var connectionString = _configuration.GetConnectionString("YourConnectionString");
            var roles = HttpContext.Current.Session["Roles"] as List<string>;
            var email = HttpContext.Current.Session["Email"] as string;

            using (var conn = new SqlConnection(connectionString))
            {
                var query = (roles.Contains("Admin") || roles.Contains("BothRoles"))
                    ? "SELECT ... FROM Complaints ORDER BY Id DESC"
                    : "SELECT ... FROM Complaints WHERE Email = @Email ORDER BY Id DESC";

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
                            complaints.Add(new ComplaintViewModel
                            {
                                // Populate properties
                            });
                        }
                        gvComplaints.DataSource = complaints;
                        gvComplaints.DataBind();
                    }
                }
            }
        }

        // Other methods...

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        // Use a more specific return type
        protected string GetUserRoleClass() => User.IsInRole("Admin") || User.IsInRole("BothRoles") ? "admin" : string.Empty;
    }
}