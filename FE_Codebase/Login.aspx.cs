using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Microsoft.Extensions.Configuration; // Updated for modern configuration management

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
            // Empty method removed as it's not necessary
        }

        // Async method for better performance
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString");
            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn);
            cmd.Parameters.AddWithValue("@username", email);
            
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
            }
            
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true); // Added 'true' for security
        }

        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            var roles = await GetUserRolesAsync(email);

            if (roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles"))
            {
                if (await AuthenticateUserAsync(email, password, roles))
                {
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", true);
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

        private async Task<bool> AuthenticateUserAsync(string username, string password, List<string> roles)
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
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashing password

            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
            }

            try
            {
                await conn.OpenAsync();
                var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                ShowErrorMessage("An error occurred: " + ex.Message);
                return false;
            }
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        // Simple password hashing (consider using a more robust method in production)
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
    }
}