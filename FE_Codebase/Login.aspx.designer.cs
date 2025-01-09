// This file is auto-generated. Manual changes may be overwritten.

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Login
    {
        // Use readonly modifier for better performance and thread-safety
        // Changed from protected to private for better encapsulation
        private readonly HtmlForm form1;

        // Using nullable reference types for better null-safety
        private readonly TextBox? txtUsername;
        private readonly TextBox? txtPassword;
        private readonly Button? btnLoginUser;
        private readonly Label? lblMessage;

        public Login()
        {
            // Initialize fields in the constructor
            form1 = new HtmlForm();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLoginUser = new Button();
            lblMessage = new Label();
        }

        // Property for form1 with a nullable return type
        public HtmlForm? Form => form1;

        // Properties for other controls with nullable return types
        public TextBox? Username => txtUsername;
        public TextBox? Password => txtPassword;
        public Button? LoginButton => btnLoginUser;
        public Label? Message => lblMessage;
    }
}