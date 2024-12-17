using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PimsApp
{
    public partial class Home
    {
        // Use 'public' instead of 'protected' for better encapsulation
        // Use 'required' keyword to ensure non-null values
        public required HtmlForm Form1 { get; set; } // Modernized: Renamed to follow C# naming conventions

        public required Label LblWelcome { get; set; } // Modernized: Renamed to follow C# naming conventions

        public required Button BtnLogout { get; set; } // Modernized: Renamed to follow C# naming conventions

        public required HtmlGenericControl PageTitle { get; set; } // Modernized: Renamed to follow C# naming conventions

        public required Label LblSuccessMessage { get; set; } // Modernized: Renamed and fixed typo in 'Success'

        public required Button BtnRegisterComplaint { get; set; } // Modernized: Renamed to follow C# naming conventions

        public required GridView GvComplaints { get; set; } // Modernized: Renamed to follow C# naming conventions
    }
}