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
using Microsoft.AspNetCore.Cryptography.KeyDerivation; // Added for password hashing

namespace PimsApp
{
    public partial class Login : System.Web.UI.Page
    {
        // Use a logger instead of directly writing to lblMessage
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove empty if statement
        }

        // Removed unused GetUserRoles method

        protected void RegisterComplaint_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterComplaint.aspx", false); // Added false to prevent response caching
        }

        protected void btnLoginUser_Click(object sender, EventArgs e)
        {
            string email = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Moved roles retrieval inside AuthenticateUser method
            if (AuthenticateUser(email, password, out List<string> roles))
            {
                Session["Email"] = email;
                Session["Roles"] = roles;
                Response.Redirect("Home.aspx", false); // Added false to prevent response caching
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool AuthenticateUser(string username, string password, out List<string> roles)
        {
            roles = new List<string>();
            string connString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT Role, PasswordHash, PasswordSalt
                    FROM EmpDetails
                    WHERE Email = @username";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string storedHash = reader["PasswordHash"].ToString();
                                string salt = reader["PasswordSalt"].ToString();

                                // Verify the password
                                if (VerifyPassword(password, storedHash, salt))
                                {
                                    roles = reader["Role"].ToString().Split(',').Select(r => r.Trim()).ToList();
                                    return true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "An error occurred during user authentication");
                    }
                }
            }
            return false;
        }

        // New method for password verification
        private bool VerifyPassword(string password, string storedHash, string salt)
        {
            string computedHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return computedHash == storedHash;
        }

        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPassword.aspx", false); // Changed to a more appropriate page and added false to prevent response caching
        }
    }
}