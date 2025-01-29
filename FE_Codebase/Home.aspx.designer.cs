using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Use 'public' instead of 'protected' for better encapsulation
        // Use 'required' keyword to ensure non-null at runtime
        public required HtmlForm Form1 { get; set; } // Renamed to follow C# naming conventions

        public required Label LblWelcome { get; set; } // Renamed to follow C# naming conventions

        public required Button BtnLogout { get; set; } // Renamed to follow C# naming conventions

        public required HtmlGenericControl PageTitle { get; set; } // Renamed to follow C# naming conventions

        public required Label LblSuccessMessage { get; set; } // Renamed and fixed typo in 'Success'

        public required Button BtnRegisterComplaint { get; set; } // Renamed to follow C# naming conventions

        public required GridView GvComplaints { get; set; } // Renamed to follow C# naming conventions

        // Consider adding a constructor to initialize these properties if needed
        public Home()
        {
            // Initialize properties here if required
        }
    }
}