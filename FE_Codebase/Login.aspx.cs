using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Extensions.Configuration; // Added for modern configuration management

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IConfiguration _configuration; // Added for dependency injection

        // Constructor for dependency injection
        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No changes needed here
        }

        private List<string> GetUserRoles(string email)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString"); // Use IConfiguration

            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn);
            cmd.Parameters.AddWithValue("@username", email);

            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)); // Use modern string splitting
            }

            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true); // Added 'true' for security
        }

        protected void btnLoginUser_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ShowError("Username and password are required.");
                return;
            }

            List<string> roles = GetUserRoles(email);

            if (roles.Any(r => r == "Admin" || r == "NormalUser" || r == "BothRoles")) // Use LINQ for cleaner role check
            {
                if (AuthenticateUser(email, password, roles))
                {
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", true); // Added 'true' for security
                }
                else
                {
                    ShowError("Invalid username or password.");
                }
            }
            else
            {
                ShowError("Invalid username or password.");
            }
        }

        private bool AuthenticateUser(string username, string password, List<string> roles)
        {
            var connString = _configuration.GetConnectionString("YourConnectionString");
            using var conn = new SqlConnection(connString);

            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
            var query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                  AND Password = @password
                  AND ({roleConditions})";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hash the password

            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
            }

            try
            {
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                ShowError("An error occurred during authentication.");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true); // Changed to a more appropriate page
        }

        private void ShowError(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }
    }
}