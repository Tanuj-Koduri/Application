using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        // Inject configuration using dependency injection
        private readonly IConfiguration _configuration;
        private readonly ILogger<Login> _logger;

        public Login(IConfiguration configuration, ILogger<Login> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear any existing session
                Session.Clear();
            }
        }

        // Converted to async method for better performance
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString");

            try
            {
                await using var conn = new SqlConnection(connString);
                const string query = "SELECT Role FROM EmpDetails WHERE Email = @username";
                
                await using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = email;

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user roles for email: {Email}", email);
                throw;
            }

            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true);
        }

        // Converted to async
        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Input validation
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter both email and password.");
                    return;
                }

                var roles = await GetUserRolesAsync(email);

                if (!roles.Any())
                {
                    ShowError("Invalid username or password.");
                    return;
                }

                if (await AuthenticateUserAsync(email, HashPassword(password), roles))
                {
                    // Store minimal information in session
                    Session["Email"] = email;
                    Session["Roles"] = string.Join(",", roles);
                    
                    // Redirect with anti-forgery token
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
                ShowError("An error occurred during login. Please try again.");
            }
        }

        // Converted to async with security improvements
        private async Task<bool> AuthenticateUserAsync(string username, string hashedPassword, List<string> roles)
        {
            var connString = _configuration.GetConnectionString("YourConnectionString");
            
            await using var conn = new SqlConnection(connString);
            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));

            var query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                AND Password = @password
                AND ({roleConditions})";

            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = hashedPassword;

            // Add role parameters
            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.Add($"@role{i}", SqlDbType.NVarChar).Value = $"%{roles[i]}%";
            }

            try
            {
                await conn.OpenAsync();
                var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error for user: {Username}", username);
                throw;
            }
        }

        // Added password hashing
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

            return $"{Convert.ToBase64String(salt)}.{hashed}";
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