using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient; // Modern SQL Client library

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration
        private readonly ILogger<Login> _logger; // Adding logging
        private readonly IPasswordHasher<string> _passwordHasher; // For secure password handling

        // Constructor with dependency injection
        public Login(IConfiguration configuration, ILogger<Login> logger, IPasswordHasher<string> passwordHasher)
        {
            _configuration = configuration;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Clear any existing session
                Session.Clear();
            }
        }

        // Converted to async
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString");

            try
            {
                await using var conn = new SqlConnection(connString);
                const string query = "SELECT Role FROM EmpDetails WHERE Email = @username";
                
                await using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@username", email));

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();
                
                while (await reader.ReadAsync())
                {
                    roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries));
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
            Response.Redirect("RegisterComplaint.aspx", true); // Added endResponse parameter
        }

        // Converted to async
        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Username and password are required.");
                    return;
                }

                var roles = await GetUserRolesAsync(email, password);

                if (!roles.Any())
                {
                    ShowError("Invalid username or password.");
                    return;
                }

                if (await AuthenticateUserAsync(email, password, roles))
                {
                    // Store minimal information in session
                    Session["Email"] = email;
                    Session["Roles"] = string.Join(",", roles);
                    
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

        // Converted to async with secure password handling
        private async Task<bool> AuthenticateUserAsync(string username, string password, List<string> roles)
        {
            var connString = _configuration.GetConnectionString("YourConnectionString");
            
            await using var conn = new SqlConnection(connString);
            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));

            var query = $@"
                SELECT PasswordHash
                FROM EmpDetails
                WHERE Email = @username
                AND ({roleConditions})";

            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add(new SqlParameter("@username", username));

            // Add parameters for each role
            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.Add(new SqlParameter($"@role{i}", $"%{roles[i]}%"));
            }

            try
            {
                await conn.OpenAsync();
                var storedHash = await cmd.ExecuteScalarAsync() as string;
                
                return storedHash != null && 
                       _passwordHasher.VerifyHashedPassword(null, storedHash, password) != PasswordVerificationResult.Failed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Authentication error for user {Username}", username);
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