// This file is auto-generated. Manual changes may be overwritten.
// Consider using partial classes for custom logic in a separate file.

namespace PimsApp
{
    public partial class Home
    {
        // Use nullable reference types for better null-safety
        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.HtmlControls.HtmlForm? form1;

        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.WebControls.Label? lblWelcome;

        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.WebControls.Button? btnLogout;

        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl? pageTitle;

        // Modernized: Changed to nullable reference type and fixed typo in name
        protected global::System.Web.UI.WebControls.Label? lblSuccessMessage;

        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.WebControls.Button? btnRegisterComplaint;

        // Modernized: Changed to nullable reference type
        protected global::System.Web.UI.WebControls.GridView? gvComplaints;

        // Modernized: Added a method to ensure all controls are properly initialized
        protected void EnsureControlsInitialized()
        {
            if (form1 == null) throw new InvalidOperationException("form1 is not initialized.");
            if (lblWelcome == null) throw new InvalidOperationException("lblWelcome is not initialized.");
            if (btnLogout == null) throw new InvalidOperationException("btnLogout is not initialized.");
            if (pageTitle == null) throw new InvalidOperationException("pageTitle is not initialized.");
            if (lblSuccessMessage == null) throw new InvalidOperationException("lblSuccessMessage is not initialized.");
            if (btnRegisterComplaint == null) throw new InvalidOperationException("btnRegisterComplaint is not initialized.");
            if (gvComplaints == null) throw new InvalidOperationException("gvComplaints is not initialized.");
        }
    }
}