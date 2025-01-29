using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Microsoft.Extensions.Configuration; // Updated configuration library

namespace PimsApp
{
    public partial class Login : Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration

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
            string connString = _configuration.GetConnectionString("YourConnectionString"); // Using IConfiguration

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn))
            {
                cmd.Parameters.AddWithValue("@username", email);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)); // Using more efficient string splitting
                    }
                }
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

            if (roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles")) // Using pattern matching
            {
                if (AuthenticateUser(email, password, roles))
                {
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", false);
                }
                else
                {
                    DisplayErrorMessage("Invalid username or password.");
                }
            }
            else
            {
                DisplayErrorMessage("Invalid username or password.");
            }
        }

        private bool AuthenticateUser(string username, string password, List<string> roles)
        {
            string connString = _configuration.GetConnectionString("YourConnectionString");
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
                DisplayErrorMessage($"An error occurred: {ex.Message}");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        private void DisplayErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", false); // Changed to a more appropriate page
        }
    }
}