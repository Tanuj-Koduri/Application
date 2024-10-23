#nullable enable // Modernization: Enable nullable reference types for the entire file

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Modernization: Use nullable reference types for better null safety
        // Add the nullable annotation to all reference types

        /// <summary>
        /// form1 control.
        /// </summary>
        protected HtmlForm? form1;

        /// <summary>
        /// lblWelcome control.
        /// </summary>
        protected Label? lblWelcome;

        /// <summary>
        /// btnLogout control.
        /// </summary>
        protected Button? btnLogout;

        /// <summary>
        /// pageTitle control.
        /// </summary>
        protected HtmlGenericControl? pageTitle;

        /// <summary>
        /// lblSuccessMessage control.
        /// </summary>
        // Modernization: Fixed typo in variable name
        protected Label? lblSuccessMessage;

        /// <summary>
        /// btnRegisterComplaint control.
        /// </summary>
        protected Button? btnRegisterComplaint;

        /// <summary>
        /// gvComplaints control.
        /// </summary>
        protected GridView? gvComplaints;

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

        // Modernization: Add a method to ensure all controls are initialized before use
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ValidateControls();
        }
    }
}