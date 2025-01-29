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
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        // Constructor injection for configuration
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

        // Updated to async/await pattern
        private async Task<List<string>> GetUserRolesAsync(string email)
        {
            var roles = new List<string>();
            
            using var conn = new SqlConnection(_connectionString);
            const string query = "SELECT Role FROM EmpDetails WHERE Email = @username";
            
            using var cmd = new SqlCommand(query, conn);
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
            Response.Redirect("RegisterComplaint.aspx", endResponse: true);
        }

        // Updated to async/await and improved security
        protected async void btnLoginUser_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ShowError("Please enter both username and password.");
                    return;
                }

                var roles = await GetUserRolesAsync(email, HashPassword(password));

                if (roles.Any(r => r is "Admin" or "NormalUser" or "BothRoles"))
                {
                    if (await AuthenticateUserAsync(email, password, roles))
                    {
                        Session["Email"] = email;
                        Session["Roles"] = roles;
                        Response.Redirect("Home.aspx", endResponse: true);
                    }
                    else
                    {
                        ShowError("Invalid credentials.");
                    }
                }
                else
                {
                    ShowError("Access denied.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ShowError("An error occurred. Please try again later.");
            }
        }

        // Updated authentication method with improved security
        private async Task<bool> AuthenticateUserAsync(string username, string password, List<string> roles)
        {
            using var conn = new SqlConnection(_connectionString);
            var hashedPassword = HashPassword(password);
            
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
                var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        // New method for password hashing
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        // Helper method for displaying errors
        private void ShowError(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", endResponse: true);
        }
    }
}