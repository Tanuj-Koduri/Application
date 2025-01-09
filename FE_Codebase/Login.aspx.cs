using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace PimsApp
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var roles = GetUserRoles(username, password);

            if (roles.Any(r => r == "Admin" || r == "NormalUser" || r == "BothRoles"))
            {
                if (AuthenticateUser(username, password, roles))
                {
                    HttpContext.Session.SetString("Email", username);
                    HttpContext.Session.SetString("Roles", string.Join(",", roles));
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View("Index");
        }

        [HttpGet]
        public IActionResult RegisterComplaint()
        {
            return RedirectToAction("Index", "RegisterComplaint");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return RedirectToAction("Index", "Login");
        }

        private List<string> GetUserRoles(string username, string password)
        {
            var roles = new List<string>();
            var connString = _configuration.GetConnectionString("YourConnectionString");

            using var conn = new SqlConnection(connString);
            using var cmd = new SqlCommand("SELECT Role FROM EmpDetails WHERE Email = @username AND Password = @password", conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hash the password

            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                roles.AddRange(reader["Role"].ToString().Split(',').Select(r => r.Trim()));
            }

            return roles;
        }

        private bool AuthenticateUser(string username, string password, List<string> roles)
        {
            var connString = _configuration.GetConnectionString("YourConnectionString");
            using var conn = new SqlConnection(connString);

            var roleConditions = string.Join(" OR ", roles.Select((role, index) => $"Role LIKE '%' + @role{index} + '%'"));
            var query = $@"
                SELECT COUNT(1)
                FROM EmpDetails
                WHERE Email = @username
                  AND Password = @password
                  AND ({roleConditions})";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", HashPassword(password)); // Hash the password

            for (int i = 0; i < roles.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@role{i}", roles[i]);
            }

            try
            {
                conn.Open();
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}