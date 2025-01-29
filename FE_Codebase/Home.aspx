<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <title>Admin Dashboard - EcoSight</title> <!-- Updated meaningful title -->
    
    <!-- Updated to latest Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" 
          integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" 
          crossorigin="anonymous">
    
    <!-- Moved styles to external CSS file: styles.css -->
    <link rel="stylesheet" href="~/Content/styles.css">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <!-- Updated navbar with Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
            <div class="container">
                <div class="navbar-collapse justify-content-end">
                    <ul class="navbar-nav">
                        <li class="nav-item me-3">
                            <asp:Label ID="lblWelcome" runat="server" CssClass="navbar-text fw-bold" />
                        </li>
                        <li class="nav-item">
                            <!-- Added confirmation dialog -->
                            <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" 
                                      Text="Logout" OnClick="btnLogout_Click" 
                                      OnClientClick="return confirm('Are you sure you want to logout?');" />
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="container mt-4">
            <!-- Added alert component for messages -->
            <div class="alert alert-success alert-dismissible fade show" role="alert" id="successAlert" runat="server" visible="false">
                <asp:Label ID="lblSucessMessage" runat="server" />
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </div>

            <!-- Updated banner with modern design -->
            <div class="banner p-4 mb-4 rounded shadow-sm">
                <h1 class="display-6 mb-0">EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <!-- Updated GridView with modern styling -->
            <div class="card shadow-sm">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5 id="pageTitle" runat="server" class="mb-0"></h5>
                    <asp:Button ID="btnRegisterComplaint" runat="server" 
                              CssClass="btn btn-primary" 
                              Text="Register Complaint" 
                              OnClick="btnRegisterComplaint_Click" />
                </div>
                <div class="card-body">
                    <asp:GridView ID="gvComplaints" runat="server" 
                                CssClass="table table-hover" 
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvComplaints_RowDataBound" 
                                OnRowCommand="gvComplaints_RowCommand"
                                HeaderStyle-CssClass="table-light">
                        <!-- GridView columns remain same but with updated styling -->
                        <!-- ... existing columns ... -->
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <!-- Updated to latest versions of JS libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript for enhanced functionality -->
    <script src="~/Scripts/site.js"></script>
</body>
</html>