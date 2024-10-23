//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated. 
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable // Modernization: Enable nullable reference types for the entire file

namespace PimsApp
{
    public partial class Home
    {
        // Modernization: Use nullable reference types for better null safety
        // Add the nullable annotation to all reference types

        /// <summary>
        /// form1 control.
        /// </summary>
        protected global::System.Web.UI.HtmlControls.HtmlForm? form1;

        /// <summary>
        /// lblWelcome control.
        /// </summary>
        protected global::System.Web.UI.WebControls.Label? lblWelcome;

        /// <summary>
        /// btnLogout control.
        /// </summary>
        protected global::System.Web.UI.WebControls.Button? btnLogout;

        /// <summary>
        /// pageTitle control.
        /// </summary>
        protected global::System.Web.UI.HtmlControls.HtmlGenericControl? pageTitle;

        /// <summary>
        /// lblSuccessMessage control.
        /// </summary>
        // Modernization: Fixed typo in variable name
        protected global::System.Web.UI.WebControls.Label? lblSuccessMessage;

        /// <summary>
        /// btnRegisterComplaint control.
        /// </summary>
        protected global::System.Web.UI.WebControls.Button? btnRegisterComplaint;

        /// <summary>
        /// gvComplaints control.
        /// </summary>
        protected global::System.Web.UI.WebControls.GridView? gvComplaints;

        // Modernization: Add a constructor to initialize fields if necessary
        public Home()
        {
            // Initialize fields here if needed
        }

        // Modernization: Add a method to validate that all controls are properly initialized
        protected void ValidateControls()
        {
            // Modernization: Use pattern matching to check for null
            if (form1 is null) ThrowControlNotInitializedException(nameof(form1));
            if (lblWelcome is null) ThrowControlNotInitializedException(nameof(lblWelcome));
            if (btnLogout is null) ThrowControlNotInitializedException(nameof(btnLogout));
            if (pageTitle is null) ThrowControlNotInitializedException(nameof(pageTitle));
            if (lblSuccessMessage is null) ThrowControlNotInitializedException(nameof(lblSuccessMessage));
            if (btnRegisterComplaint is null) ThrowControlNotInitializedException(nameof(btnRegisterComplaint));
            if (gvComplaints is null) ThrowControlNotInitializedException(nameof(gvComplaints));
        }

        // Modernization: Extract method to reduce code duplication
        private static void ThrowControlNotInitializedException(string controlName)
        {
            throw new InvalidOperationException($"{controlName} is not initialized");
        }
    }
}