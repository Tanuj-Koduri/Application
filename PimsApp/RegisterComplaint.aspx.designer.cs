using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class RegisterComplaint
    {
        // Use nullable reference types for better null-safety
        protected HtmlForm? form1; // Modernized: Added nullable reference type

        protected Label? lblWelcome; // Modernized: Added nullable reference type

        protected Label? lblMessage; // Modernized: Added nullable reference type

        protected Panel? pnlComplaint; // Modernized: Added nullable reference type

        // Use more specific input types for better validation
        protected TextBox? txtFirstName; // Modernized: Added nullable reference type
        protected TextBox? txtLastName; // Modernized: Added nullable reference type
        protected TextBox? txtEmpId; // Modernized: Added nullable reference type
        protected TextBox? txtEmail; // Consider using ASP.NET Core's EmailAddressAttribute for validation
        protected TextBox? txtContactNumber; // Consider using a masked input for phone numbers

        protected RequiredFieldValidator? ref_date; // Modernized: Added nullable reference type

        // Use DatePicker control for better date input
        protected TextBox? txtDateTimeCapture; // Modernized: Consider using DatePicker control

        protected TextBox? txtStreetAddress1; // Modernized: Added nullable reference type
        protected TextBox? txtStreetAddress2; // Modernized: Added nullable reference type
        protected TextBox? txtCity; // Modernized: Added nullable reference type
        protected TextBox? txtZipcode; // Modernized: Added nullable reference type
        protected TextBox? txtState; // Modernized: Added nullable reference type

        protected RequiredFieldValidator? req_image; // Modernized: Added nullable reference type

        // Consider using modern file upload controls with client-side validation
        protected FileUpload? fileUpload; // Modernized: Added nullable reference type

        protected TextBox? txtComments; // Modernized: Added nullable reference type

        protected Button? btnSubmit; // Modernized: Added nullable reference type
        protected Button? btnCancel; // Modernized: Added nullable reference type

        // Modernized: Add method to initialize controls
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControls();
        }

        // Modernized: Separate method for control initialization
        private void InitializeControls()
        {
            // Initialize controls here
            // This helps with testing and separation of concerns
        }

        // Modernized: Add method for form submission
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Implement form submission logic
            // Consider using model binding and validation attributes
        }

        // Modernized: Add method for form cancellation
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Implement cancellation logic
        }
    }
}