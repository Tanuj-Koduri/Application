using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Extensions.Configuration; // Updated for modern configuration management
using System.Threading.Tasks; // Added for async operations

namespace PimsApp
{
    public partial class RegisterComplaint : Page
    {
        private readonly IConfiguration _configuration; // Dependency injection for configuration

        public RegisterComplaint(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Session["Email"] as string;
                if (string.IsNullOrEmpty(email))
                {
                    Response.Redirect("Login.aspx", true); // Added true for end response
                }
                else
                {
                    await PopulateEmployeeDetailsAsync(email); // Changed to async
                    lblWelcome.Text = $"Welcome, {HttpUtility.HtmlEncode(email)}!"; // Added HTML encoding for security
                }
            }
        }

        private async Task PopulateEmployeeDetailsAsync(string email)
        {
            string connString = _configuration.GetConnectionString("YourConnectionString"); // Updated configuration access
            using (var conn = new SqlConnection(connString))
            {
                const string query = "SELECT FirstName, LastName, EmpId, Email, ContactNumber FROM EmpDetails WHERE Email = @Email";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        await conn.OpenAsync(); // Changed to async
                        using (var reader = await cmd.ExecuteReaderAsync()) // Changed to async
                        {
                            if (await reader.ReadAsync()) // Changed to async
                            {
                                txtFirstName.Text = reader["FirstName"].ToString();
                                txtLastName.Text = reader["LastName"].ToString();
                                txtEmpId.Text = reader["EmpId"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtContactNumber.Text = reader["ContactNumber"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = $"An error occurred while fetching employee details: {ex.Message}";
                    }
                }
            }
        }

        protected void btnAddDateTime_Click(object sender, EventArgs e)
        {
            txtDateTimeCapture.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx", true); // Added true for end response
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                var complaintData = new ComplaintData
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    EmpId = txtEmpId.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    ContactNumber = txtContactNumber.Text.Trim(),
                    DateTimeCapture = txtDateTimeCapture.Text.Trim(),
                    Comments = txtComments.Text.Trim(),
                    PicturePaths = await UploadPicturesAsync(),
                    ComplaintId = GenerateUniqueComplaintId(),
                    StreetAddress1 = txtStreetAddress1.Text.Trim(),
                    StreetAddress2 = txtStreetAddress2.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    State = txtState.Text.Trim(),
                    Zip = txtZipcode.Text.Trim()
                };

                await InsertComplaintAsync(complaintData);
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please fill out the Address fields.";
            }
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrEmpty(txtStreetAddress1.Text.Trim()) &&
                   !string.IsNullOrEmpty(txtCity.Text.Trim()) &&
                   !string.IsNullOrEmpty(txtZipcode.Text.Trim()) &&
                   !string.IsNullOrEmpty(txtState.Text.Trim());
        }

        private string GenerateUniqueComplaintId()
        {
            return $"CMP{DateTime.Now:yyyyMMddHHmmss}";
        }

        private async Task<string> UploadPicturesAsync()
        {
            var imagePaths = new List<string>();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string uploadDirectory = Path.Combine(desktopPath, "UploadImages");

            Directory.CreateDirectory(uploadDirectory); // This will create the directory if it doesn't exist

            if (fileUpload.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    string uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(uploadedFile.FileName)}";
                    string filePath = Path.Combine(uploadDirectory, uniqueFileName);

                    await Task.Run(() => uploadedFile.SaveAs(filePath)); // Changed to async
                    imagePaths.Add(filePath);
                }
            }

            return string.Join(",", imagePaths);
        }

        private async Task InsertComplaintAsync(ComplaintData complaintData)
        {
            string connString = _configuration.GetConnectionString("YourConnectionString");
            using (var conn = new SqlConnection(connString))
            {
                const string query = @"
                INSERT INTO Complaints (ComplaintId, FirstName, LastName, EmpId, Email, ContactNumber, DateTimeCapture, PictureCaptureLocation, PictureUpload, Comments, Status, StreetAddress1, StreetAddress2, City, Zip, State, CurrentStatus)
                VALUES (@ComplaintId, @FirstName, @LastName, @EmpId, @Email, @ContactNumber, @DateTimeCapture, @PictureCaptureLocation, @PictureUpload, @Comments, @Status, @StreetAddress1, @StreetAddress2, @City, @Zip, @State, @CurrentStatus)";

                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ComplaintId", complaintData.ComplaintId);
                    cmd.Parameters.AddWithValue("@FirstName", complaintData.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", complaintData.LastName);
                    cmd.Parameters.AddWithValue("@EmpId", complaintData.EmpId);
                    cmd.Parameters.AddWithValue("@Email", complaintData.Email);
                    cmd.Parameters.AddWithValue("@ContactNumber", complaintData.ContactNumber);
                    cmd.Parameters.AddWithValue("@DateTimeCapture", complaintData.DateTimeCapture);
                    cmd.Parameters.AddWithValue("@PictureCaptureLocation", "");
                    cmd.Parameters.AddWithValue("@PictureUpload", complaintData.PicturePaths);
                    cmd.Parameters.AddWithValue("@Comments", complaintData.Comments);
                    cmd.Parameters.AddWithValue("@Status", "Not Started");
                    cmd.Parameters.AddWithValue("@StreetAddress1", complaintData.StreetAddress1);
                    cmd.Parameters.AddWithValue("@StreetAddress2", complaintData.StreetAddress2);
                    cmd.Parameters.AddWithValue("@City", complaintData.City);
                    cmd.Parameters.AddWithValue("@Zip", complaintData.Zip);
                    cmd.Parameters.AddWithValue("@State", complaintData.State);
                    cmd.Parameters.AddWithValue("@CurrentStatus", "Not Started");

                    try
                    {
                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        string successMessage = $"Thank you for the submission. Your Complaint registered successfully. Your complaint ID is {complaintData.ComplaintId}.";
                        Session["SuccessMessage"] = successMessage;

                        Response.Redirect("Home.aspx", true);
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = $"An error occurred: {ex.Message}";
                    }
                }
            }
        }
    }

    public class ComplaintData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmpId { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string DateTimeCapture { get; set; }
        public string Comments { get; set; }
        public string PicturePaths { get; set; }
        public string ComplaintId { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}