using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Use nullable reference types for better null safety
        public HtmlForm? Form { get; set; } // Renamed from form1 for clarity

        // Use auto-property for concise syntax
        public Label? WelcomeLabel { get; set; } // Renamed from lblWelcome for clarity

        // Use Button instead of obsolete System.Web.UI.WebControls.Button
        public Button? LogoutButton { get; set; } // Renamed from btnLogout for clarity

        // Use more specific HtmlHeading1 instead of HtmlGenericControl
        public HtmlHeading1? PageTitle { get; set; } // Renamed from pageTitle for consistency

        public Label? SuccessMessage { get; set; } // Renamed from lblSucessMessage and fixed typo

        public Button? RegisterComplaintButton { get; set; } // Renamed from btnRegisterComplaint for clarity

        // Consider using a more modern data display component like Telerik Grid or DevExpress GridView
        public GridView? ComplaintsGrid { get; set; } // Renamed from gvComplaints for clarity

        // Add a constructor to initialize the controls
        public Home()
        {
            InitializeComponent();
        }

        // Add a method to initialize the controls
        private void InitializeComponent()
        {
            // Initialize controls here
            Form = new HtmlForm();
            WelcomeLabel = new Label();
            LogoutButton = new Button();
            PageTitle = new HtmlHeading1();
            SuccessMessage = new Label();
            RegisterComplaintButton = new Button();
            ComplaintsGrid = new GridView();
        }
    }
}