using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // Added for password hashing

namespace PimsApp
{
    public partial class Login : Page
    {
        // Use dependency injection for configuration
        private readonly string _connectionString;

        public Login(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("YourConnectionString");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove empty if statement
        }

        // Use async/await for database operations
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username", conn))
            {
                cmd.Parameters.AddWithValue("@username", email);
                await conn.OpenAsync();
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
                    }
                }
            }
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true); // Use true for end response
        }

        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            var roles = await GetUserRolesAsync(email);

            if (roles.Any(r => r == "Admin" || r == "NormalUser" || r == "BothRoles"))
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

        private void ShowErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        // Use async/await and parameterized queries
        private async Task<bool> AuthenticateUserAsync(string username, string password, List<string> roles)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
                var query = $@"
                    SELECT Password FROM EmpDetails
                    WHERE Email = @username AND ({roleConditions})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    for (int i = 0; i < roles.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
                    }

                    try
                    {
                        await conn.OpenAsync();
                        var hashedPassword = await cmd.ExecuteScalarAsync() as string;
                        return hashedPassword != null && VerifyPassword(password, hashedPassword);
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

        // Implement secure password hashing and verification
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            // Implement password verification using a secure method (e.g., bcrypt)
            // This is a placeholder and should be replaced with a proper implementation
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHash);
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true); // Changed to a more appropriate page
        }
    }
}