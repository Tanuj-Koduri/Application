using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // Added for secure password hashing

namespace PimsApp
{
    public partial class Login : Page
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Removed empty if statement
        }

        // Simplified method using async/await pattern
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
            Response.Redirect("RegisterComplaint.aspx", false); // Added 'false' to prevent thread abortion
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
            using (var conn = new SqlConnection(_connectionString))
            {
                string roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE @role{index}"));
                string query = $@"
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
                        var storedHash = await cmd.ExecuteScalarAsync() as string;
                        return storedHash != null && VerifyPassword(password, storedHash);
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

        // New method for secure password hashing
        private bool VerifyPassword(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var computedHash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);
            return computedHash.SequenceEqual(hashBytes.Skip(16));
        }

        private void ShowErrorMessage(string message)
        {
            lblMessage.Visible = true;
            lblMessage.Text = message;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", false); // Changed to correct page and added 'false'
        }
    }
}