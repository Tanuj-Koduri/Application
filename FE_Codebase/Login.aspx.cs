using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using Microsoft.Extensions.Configuration; // Updated for modern configuration management
using System.Security.Cryptography; // Added for secure password hashing
using System.Text;

namespace PimsApp
{
    public partial class Login : Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration

        // Constructor for dependency injection
        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Page_Load method left empty as it was in the original code
        }

        private List<string> GetUserRoles(string email)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString"); // Using IConfiguration
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn);
            cmd.Parameters.AddWithValue("@username", email);
            
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)); // Using more efficient string splitting
            }
            
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", false); // Added 'false' to prevent throwing ThreadAbortException
        }

        protected void btnLoginUser_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            List<string> roles = GetUserRoles(email);

            if (roles.Any(r => r == "Admin" || r == "NormalUser" || r == "BothRoles")) // Using LINQ for cleaner role check
            {
                if (AuthenticateUser(email, password, roles))
                {
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    ShowErrorMessage("Invalid username or password.");
                }
            }
            else
            {
                ShowErrorMessage("Invalid username or password.");
            }
        }

        private bool AuthenticateUser(string username, string password, List<string> roles)
        {
            var connString = _configuration.GetConnectionString("YourConnectionString");
            using var conn = new SqlConnection(connString);
            
            string roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
            string query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                  AND Password = @password
                  AND ({roleConditions})";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashing the password

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
                ShowErrorMessage($"An error occurred: {ex.Message}");
                return false;
            }
        }

        private List<string> GetUserRoles(string username, string password)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString");
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username AND Password = @password", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashing the password
            
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()));
            }
            
            return roles;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", false); // Changed to a more appropriate page
        }

        // Helper method to show error messages
        private void ShowErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}