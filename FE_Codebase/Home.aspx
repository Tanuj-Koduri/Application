<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta for responsiveness -->
    <title>Admin Dashboard</title>
    
    <!-- Updated to Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" 
          integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" 
          crossorigin="anonymous">
    
    <!-- Moved styles to separate CSS file -->
    <link rel="stylesheet" href="~/Styles/Home.css">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <!-- Updated navbar to Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light fixed-top">
            <div class="container-fluid">
                <div class="ms-auto">
                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" CssClass="navbar-text fw-bold" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger ms-3" 
                              Text="Logout" OnClick="btnLogout_Click" 
                              UseSubmitBehavior="false" /> <!-- Added UseSubmitBehavior for better UX -->
                </div>
            </div>
        </nav>

        <main class="container mt-5 pt-4"> <!-- Added padding for fixed navbar -->
            <!-- Updated banner with semantic HTML -->
            <header class="banner">
                <h1>EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </header>

            <h5 id="pageTitle" runat="server" class="mb-3 text-center"></h5>
            
            <!-- Added alert component for success message -->
            <asp:Panel ID="successAlert" runat="server" CssClass="alert alert-success" Visible="false">
                <asp:Label ID="lblSucessMessage" runat="server"></asp:Label>
            </asp:Panel>

            <div class="row mb-4">
                <div class="col-12 text-end">
                    <asp:Button ID="btnRegisterComplaint" runat="server" 
                              CssClass="btn btn-primary" 
                              Text="Register Complaint" 
                              OnClick="btnRegisterComplaint_Click" />
                </div>
            </div>

            <!-- Updated GridView with modern styling and accessibility features -->
            <asp:GridView ID="gvComplaints" runat="server" 
                         AutoGenerateColumns="False" 
                         CssClass="table table-striped table-hover table-responsive" 
                         OnRowDataBound="gvComplaints_RowDataBound" 
                         OnRowCommand="gvComplaints_RowCommand"
                         HeaderStyle-CssClass="table-dark"
                         aria-label="Complaints List">
                <!-- GridView columns remain the same but with updated styling -->
                <Columns>
                    <!-- ... existing columns ... -->
                </Columns>
            </asp:GridView>
        </main>
    </form>

    <!-- Updated to modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript file -->
    <script src="~/Scripts/Home.js"></script>
</body>
</html>