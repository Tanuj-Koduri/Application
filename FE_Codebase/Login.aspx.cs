using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.Data;

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

        // Updated to async method for better performance
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            
            try
            {
                // Use connection string from user secrets or environment variables
                string connString = _configuration.GetConnectionString("YourConnectionString");
                
                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand("sp_GetUserRoles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Email", email);
                
                await conn.OpenAsync();
                using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    roles.AddRange(reader["Role"].ToString()
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => r.Trim()));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user roles for {Email}", email);
                throw;
            }
            
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true);
        }

        // Updated to async
        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Input validation
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Email and password are required.");
                    return;
                }

                var roles = await GetUserRolesAsync(email);

                if (!roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles"))
                {
                    ShowError("Invalid username or password.");
                    return;
                }

                if (await AuthenticateUserAsync(email, password, roles))
                {
                    // Store minimal information in session
                    Session["Email"] = email;
                    Session["Roles"] = roles;
                    
                    // Implement anti-forgery token
                    var antiForgeryToken = GenerateAntiForgeryToken();
                    Session["AntiForgeryToken"] = antiForgeryToken;
                    
                    Response.Redirect("Home.aspx", true);
                }
                else
                {
                    ShowError("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error for user {Email}", txtUsername.Text);
                ShowError("An error occurred during login. Please try again.");
            }
        }

        // Updated authentication with secure password handling
        private async Task<bool> AuthenticateUserAsync(string email, string password, List<string> roles)
        {
            try
            {
                string connString = _configuration.GetConnectionString("YourConnectionString");
                using var conn = new SqlConnection(connString);
                using var cmd = new SqlCommand("sp_AuthenticateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(password));
                cmd.Parameters.AddWithValue("@Roles", string.Join(",", roles));

                await conn.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error for user {Email}", email);
                throw;
            }
        }

        // Secure password hashing
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

        private string GenerateAntiForgeryToken()
        {
            byte[] tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
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