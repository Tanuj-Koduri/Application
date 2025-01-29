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
            // Use ConfigurationBuilder instead of ConfigurationManager
            var connectionString = _configuration.GetConnectionString("YourConnectionString");

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn))
            {
                cmd.Parameters.AddWithValue("@username", email);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
                    }
                }
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
                ShowErrorMessage("Email and password are required.");
                return;
            }

            List<string> roles = GetUserRoles(email);

            if (roles.Any(role => role == "Admin" || role == "NormalUser" || role == "BothRoles"))
            {
                if (AuthenticateUser(email, password, roles))
                {
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", true); // Added 'true' for security
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
            var connectionString = _configuration.GetConnectionString("YourConnectionString");
            using (var conn = new SqlConnection(connectionString))
            {
                string roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));

                string query = $@"
                    SELECT COUNT(1)
                    FROM EmpDetails
                    WHERE Email = @username
                    AND Password = @password
                    AND ({roleConditions})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashed password

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
                        ShowErrorMessage("An error occurred. Please try again later.");
                        return false;
                    }
                }
            }
        }

        private List<string> GetUserRoles(string username, string password)
        {
            var roles = new List<string>();
            var connectionString = _configuration.GetConnectionString("YourConnectionString");

            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username AND Password = @password", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashed password
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(r => r.Trim()));
                    }
                }
            }
            return roles;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true); // Changed to a more appropriate page
        }

        // Added method for hashing passwords
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        // Added method for showing error messages
        private void ShowErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }
    }
}