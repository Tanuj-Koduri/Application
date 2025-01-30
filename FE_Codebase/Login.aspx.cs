using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Web.UI;

namespace PimsApp
{
    public partial class Login : Page
    {
        // Add dependency injection for configuration
        private readonly IConfiguration _configuration;
        private readonly ILogger<Login> _logger;

        public Login(IConfiguration configuration, ILogger<Login> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No need for IsPostBack check if empty
        }

        // Convert to async method
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            
            // Use connection string from user secrets or environment variables
            string connString = _configuration.GetConnectionString("YourConnectionString");
            
            await using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand(
                "SELECT Role FROM EmpDetails WHERE Email = @username", conn);
            
            cmd.Parameters.AddWithValue("@username", email);
            
            try
            {
                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user roles");
                throw;
            }
            
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true);
        }

        // Convert to async
        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Validate input
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter both username and password.");
                    return;
                }

                var roles = await GetUserRolesAsync(email, password);

                if (!roles.Any())
                {
                    ShowError("Invalid username or password.");
                    return;
                }

                if (await AuthenticateUserAsync(email, HashPassword(password), roles))
                {
                    // Use secure session management
                    Session.Timeout = 20; // 20 minutes
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    Response.Redirect("Home.aspx", true);
                }
                else
                {
                    ShowError("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error");
                ShowError("An error occurred during login.");
            }
        }

        // Helper method to hash passwords
        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        private async Task<bool> AuthenticateUserAsync(string username, string hashedPassword, List<string> roles)
        {
            string connString = _configuration.GetConnectionString("YourConnectionString");
            await using var conn = new SqlConnection(connString);
            
            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
            
            var query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                AND Password = @password
                AND ({roleConditions})";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", hashedPassword);

            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
            }

            try
            {
                await conn.OpenAsync();
                return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error");
                throw;
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true);
        }
    }
}