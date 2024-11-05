namespace PimsApp
{
    public partial class Home
    {
        // Use nullable reference types for better null-safety
        // Comment: Enabling nullable reference types helps catch potential null reference exceptions at compile-time
        #nullable enable

        // Use auto-implemented properties instead of fields
        // Comment: Auto-implemented properties provide a more concise syntax and encapsulation
        public HtmlForm Form { get; set; } = default!;

        public Label? LblWelcome { get; set; }

        public Button? BtnLogout { get; set; }

        public HtmlGenericControl? PageTitle { get; set; }

        public Label? LblSuccessMessage { get; set; }

        public Button? BtnRegisterComplaint { get; set; }

        public GridView? GvComplaints { get; set; }

        // Remove unnecessary XML comments for auto-generated properties
        // Comment: Removing redundant comments improves code readability

        // Use nameof operator for property names in attributes
        // Comment: Using nameof ensures compile-time checking of property names
        [System.ComponentModel.DataAnnotations.Display(Name = nameof(Form))]
        protected global::System.Web.UI.HtmlControls.HtmlForm form1;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(LblWelcome))]
        protected global::System.Web.UI.WebControls.Label lblWelcome;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(BtnLogout))]
        protected global::System.Web.UI.WebControls.Button btnLogout;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(PageTitle))]
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl pageTitle;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(LblSuccessMessage))]
        protected global::System.Web.UI.WebControls.Label lblSucessMessage;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(BtnRegisterComplaint))]
        protected global::System.Web.UI.WebControls.Button btnRegisterComplaint;

        [System.ComponentModel.DataAnnotations.Display(Name = nameof(GvComplaints))]
        protected global::System.Web.UI.WebControls.GridView gvComplaints;
    }
}