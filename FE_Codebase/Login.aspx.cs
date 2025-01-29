using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Data.SqlClient; // Updated SQL client library

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration
        private readonly string _connectionString;

        // Constructor with dependency injection
        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("YourConnectionString");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear any existing session
                Session.Clear();
            }
        }

        // Async method to get user roles
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT Role FROM EmpDetails WHERE Email = @username";
            
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", email);
            
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
            }
            
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true); // Added endResponse parameter
        }

        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Input validation
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

                if (roles.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)))
                {
                    if (await AuthenticateUserAsync(email, HashPassword(password), roles))
                    {
                        // Store minimal information in session
                        Session["Email"] = email;
                        Session["Roles"] = roles;
                        Response.Redirect("Home.aspx", true);
                    }
                    else
                    {
                        ShowError("Invalid username or password.");
                    }
                }
                else
                {
                    ShowError("Unauthorized access.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ShowError("An error occurred. Please try again later.");
            }
        }

        // Hash password using modern cryptography
        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private async Task<bool> AuthenticateUserAsync(string username, string hashedPassword, List<string> roles)
        {
            using var connection = new SqlConnection(_connectionString);
            var roleConditions = string.Join(" OR ", roles.Select(role => $"Role LIKE @role{role}"));

            var query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                AND Password = @password
                AND ({roleConditions})";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hashedPassword);

            foreach (var role in roles)
            {
                command.Parameters.AddWithValue($"@role{role}", $"%{role}%");
            }

            try
            {
                await connection.OpenAsync();
                int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true); // Changed to proper forgot password page
        }
    }
}