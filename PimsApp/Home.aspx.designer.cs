using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Use properties instead of fields for better encapsulation
        // Comment: Changed from protected fields to public properties
        public HtmlForm Form1 { get; set; }
        public Label LblWelcome { get; set; }
        public Button BtnLogout { get; set; }
        public HtmlGenericControl PageTitle { get; set; }
        public Label LblSuccessMessage { get; set; } // Fixed typo in property name
        public Button BtnRegisterComplaint { get; set; }
        public GridView GvComplaints { get; set; }

        // Comment: Added method for proper control initialization
        protected void Page_Init(object sender, EventArgs e)
        {
            InitializeControls();
        }

        // Comment: Added method for control initialization
        private void InitializeControls()
        {
            Form1 = new HtmlForm();
            LblWelcome = new Label();
            BtnLogout = new Button();
            PageTitle = new HtmlGenericControl();
            LblSuccessMessage = new Label();
            BtnRegisterComplaint = new Button();
            GvComplaints = new GridView();

            // Add controls to the page
            Controls.Add(Form1);
            Form1.Controls.Add(LblWelcome);
            Form1.Controls.Add(BtnLogout);
            Form1.Controls.Add(PageTitle);
            Form1.Controls.Add(LblSuccessMessage);
            Form1.Controls.Add(BtnRegisterComplaint);
            Form1.Controls.Add(GvComplaints);
        }
    }
}