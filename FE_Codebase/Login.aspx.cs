using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient; // Updated SQL client library
using System.Web.UI;

namespace PimsApp
{
    public partial class Login : Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration
        private readonly IPasswordHasher<string> _passwordHasher; // For secure password handling

        // Constructor with dependency injection
        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<string>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize any required setup
            }
        }

        // Made async for better performance
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            string connString = _configuration.GetConnectionString("YourConnectionString"); // Modern configuration access

            await using var conn = new SqlConnection(connString); // Modern using statement
            var query = "SELECT Role FROM EmpDetails WHERE Email = @username";
            
            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", email);
            
            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                roles.AddRange(reader["Role"].ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)); // Modern string splitting
            }

            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", true); // Added endResponse parameter for security
        }

        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Email and password are required.");
                    return;
                }

                var roles = await GetUserRolesAsync(email, password);

                if (roles.Any(r => new[] { "Admin", "NormalUser", "BothRoles" }.Contains(r)))
                {
                    if (await AuthenticateUserAsync(email, password, roles))
                    {
                        // Store minimal session data and use secure session options
                        Session["Email"] = email;
                        Session["Roles"] = roles;
                        Response.Redirect("Home.aspx", true);
                    }
                    else
                    {
                        ShowError("Invalid credentials.");
                    }
                }
                else
                {
                    ShowError("Unauthorized access.");
                }
            }
            catch (Exception ex)
            {
                // Add proper logging here
                ShowError("An error occurred. Please try again later.");
            }
        }

        private async Task<bool> AuthenticateUserAsync(string username, string password, List<string> roles)
        {
            string connString = _configuration.GetConnectionString("YourConnectionString");
            await using var conn = new SqlConnection(connString);

            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
            var query = $@"
                SELECT Password
                FROM EmpDetails
                WHERE Email = @username
                AND ({roleConditions})";

            await using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);

            // Add parameters for each role
            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
            }

            try
            {
                await conn.OpenAsync();
                var hashedPassword = await cmd.ExecuteScalarAsync() as string;
                
                // Verify password using modern hashing
                return hashedPassword != null && 
                       _passwordHasher.VerifyHashedPassword(username, hashedPassword, password) != PasswordVerificationResult.Failed;
            }
            catch (Exception ex)
            {
                // Add proper logging here
                throw;
            }
        }

        private void ShowError(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = HttpUtility.HtmlEncode(message); // Prevent XSS
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", true); // Changed to proper forgot password page
        }
    }
}