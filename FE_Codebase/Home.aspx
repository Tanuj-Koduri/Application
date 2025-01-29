<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta for responsiveness -->
    <title>Admin Dashboard - EcoSight</title> <!-- Updated meaningful title -->
    
    <!-- Updated to Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" 
          integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" 
          crossorigin="anonymous">
    
    <!-- Moved styles to separate CSS file -->
    <link rel="stylesheet" href="~/Content/styles.css">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <!-- Updated navbar to Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
            <div class="container-fluid">
                <div class="ms-auto d-flex align-items-center">
                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" 
                             CssClass="navbar-text me-3 fw-bold" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" 
                              Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </nav>

        <main class="container mt-4"> <!-- Changed div to semantic main tag -->
            <!-- Added alert component for messages -->
            <div class="alert alert-success alert-dismissible fade show" role="alert" 
                 id="successAlert" runat="server" visible="false">
                <asp:Label ID="lblSucessMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>

            <div class="banner shadow-sm">
                <h1 class="h3">EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <!-- Updated button container -->
            <div class="d-flex justify-content-end mb-4">
                <asp:Button ID="btnRegisterComplaint" runat="server" 
                          CssClass="btn btn-primary" 
                          Text="Register Complaint" 
                          OnClick="btnRegisterComplaint_Click" />
            </div>

            <!-- Updated GridView with modern styling -->
            <div class="table-responsive"> <!-- Added for horizontal scrolling on mobile -->
                <asp:GridView ID="gvComplaints" runat="server" 
                            CssClass="table table-hover table-striped align-middle"
                            AutoGenerateColumns="False" 
                            OnRowDataBound="gvComplaints_RowDataBound"
                            OnRowCommand="gvComplaints_RowCommand">
                    <!-- GridView columns remain same but with updated styling -->
                    <Columns>
                        <!-- ... existing columns ... -->
                    </Columns>
                </asp:GridView>
            </div>
        </main>
    </form>

    <!-- Updated to latest versions of JS libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript file -->
    <script src="~/Scripts/site.js"></script>
</body>
</html>