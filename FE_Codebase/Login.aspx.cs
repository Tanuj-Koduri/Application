using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Extensions.Configuration; // Added for modern configuration management

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IConfiguration _configuration; // Added for dependency injection

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
            var connString = _configuration.GetConnectionString("YourConnectionString"); // Using IConfiguration

            using (var conn = new SqlConnection(connString))
            {
                var query = "SELECT Role FROM EmpDetails WHERE Email = @username";
                using (var cmd = new SqlCommand(query, conn))
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
            }
            return roles;
        }

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", false); // Added 'false' to prevent possible exceptions
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
                    Response.Redirect("Home.aspx", false);
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
            using (var conn = new SqlConnection(connString))
            {
                var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));

                var query = $@"
                    SELECT COUNT(1)
                    FROM EmpDetails
                    WHERE Email = @username
                      AND Password = @password
                      AND ({roleConditions})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hashing password

                    for (int i = 0; i < roles.Count; i++)
                    {
                        cmd.Parameters.AddWithValue($"@role{i}", $"%{roles[i]}%");
                    }

                    try
                    {
                        await conn.OpenAsync();
                        int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        ShowErrorMessage("An error occurred: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        // Method to hash passwords
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void ShowErrorMessage(string message)
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