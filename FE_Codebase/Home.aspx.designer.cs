using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Use nullable reference types for better null handling
        public HtmlForm? Form { get; set; } // Renamed from form1 for clarity

        public Label? WelcomeLabel { get; set; } // Renamed from lblWelcome for consistency

        public Button? LogoutButton { get; set; } // Renamed from btnLogout for consistency

        public HtmlGenericControl? PageTitle { get; set; } // Renamed from pageTitle for consistency

        public Label? SuccessMessage { get; set; } // Renamed from lblSucessMessage and fixed typo

        public Button? RegisterComplaintButton { get; set; } // Renamed from btnRegisterComplaint for consistency

        public GridView? ComplaintsGridView { get; set; } // Renamed from gvComplaints for clarity

        // Remove auto-generated comments as they're not necessary in the code-behind file

        // Constructor to initialize controls (optional, but can improve null safety)
        public Home()
        {
            InitializeComponent();
        }

        // Method to initialize components (called in the constructor)
        private void InitializeComponent()
        {
            Form = new HtmlForm();
            WelcomeLabel = new Label();
            LogoutButton = new Button();
            PageTitle = new HtmlGenericControl();
            SuccessMessage = new Label();
            RegisterComplaintButton = new Button();
            ComplaintsGridView = new GridView();
        }
    }
}