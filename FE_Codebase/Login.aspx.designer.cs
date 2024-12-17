using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Login
    {
        // Use 'public' instead of 'protected' for better encapsulation
        // Use 'required' keyword to ensure non-null values
        public required HtmlForm Form { get; set; } // Renamed from 'form1' for clarity

        // Use nullable reference types for better null handling
        public TextBox? UsernameTextBox { get; set; } // Renamed from 'txtUsername' for clarity

        public TextBox? PasswordTextBox { get; set; } // Renamed from 'txtPassword' for clarity

        public Button? LoginButton { get; set; } // Renamed from 'btnLoginUser' for clarity

        public Label? MessageLabel { get; set; } // Renamed from 'lblMessage' for clarity

        // Add a constructor to initialize the controls
        public Login()
        {
            InitializeComponent();
        }

        // Add a method to initialize the controls
        private void InitializeComponent()
        {
            Form = new HtmlForm();
            UsernameTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            LoginButton = new Button();
            MessageLabel = new Label();

            // Set up the password textbox for secure input
            PasswordTextBox.TextMode = TextBoxMode.Password;

            // Add event handlers
            LoginButton.Click += OnLoginButtonClick;
        }

        // Add an event handler for the login button
        private void OnLoginButtonClick(object sender, EventArgs e)
        {
            // Implement login logic here
        }
    }
}