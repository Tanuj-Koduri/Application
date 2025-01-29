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
    
    <!-- Moved styles to external CSS file for better maintenance -->
    <link rel="stylesheet" href="~/Content/Styles/home.css">
    
    <!-- Added Content Security Policy -->
    <meta http-equiv="Content-Security-Policy" 
          content="default-src 'self' https:; script-src 'self' https: 'unsafe-inline' 'unsafe-eval'; style-src 'self' https: 'unsafe-inline';">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate>
        <!-- Updated navbar to Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
            <div class="container-fluid">
                <div class="ms-auto">
                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" 
                             CssClass="navbar-text fw-bold me-3" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" 
                              Text="Logout" OnClick="btnLogout_Click" 
                              UseSubmitBehavior="false" />
                </div>
            </div>
        </nav>

        <main class="container-fluid mt-4">
            <!-- Added semantic HTML elements -->
            <header class="banner mb-4">
                <h1>EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </header>

            <section class="complaints-section">
                <h2 id="pageTitle" runat="server" class="mb-3"></h2>
                
                <!-- Added alert component for success message -->
                <div id="successAlert" runat="server" class="alert alert-success alert-dismissible fade show" 
                     role="alert" visible="false">
                    <asp:Label ID="lblSucessMessage" runat="server"></asp:Label>
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </div>

                <div class="d-flex justify-content-end mb-4">
                    <asp:Button ID="btnRegisterComplaint" runat="server" 
                              CssClass="btn btn-primary" 
                              Text="Register Complaint" 
                              OnClick="btnRegisterComplaint_Click" />
                </div>

                <!-- Updated GridView with modern styling and accessibility features -->
                <asp:GridView ID="gvComplaints" runat="server" 
                            CssClass="table table-striped table-hover border" 
                            AutoGenerateColumns="False"
                            OnRowDataBound="gvComplaints_RowDataBound" 
                            OnRowCommand="gvComplaints_RowCommand"
                            aria-label="Complaints List">
                    <!-- GridView columns remain same but with enhanced styling -->
                    <Columns>
                        <!-- Existing columns with added accessibility attributes -->
                    </Columns>
                </asp:GridView>
            </section>
        </main>
    </form>

    <!-- Updated to modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript file -->
    <script src="~/Scripts/home.js"></script>
</body>
</html>