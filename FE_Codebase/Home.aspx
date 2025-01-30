<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <title>Admin Dashboard</title>
    
    <!-- Updated to Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" 
          crossorigin="anonymous">

    <!-- Moved styles to separate CSS file -->
    <link rel="stylesheet" href="~/Content/styles.css">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Updated navbar to Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <div class="navbar-nav ms-auto">
                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" 
                             CssClass="navbar-text fw-bold me-3" />
                    <!-- Added security token -->
                    <asp:HiddenField ID="AntiForgeryToken" runat="server" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" 
                              Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </nav>

        <!-- Added alert for success messages -->
        <div class="container mt-4">
            <asp:Panel ID="alertPanel" runat="server" Visible="false" 
                      CssClass="alert alert-success alert-dismissible fade show">
                <asp:Label ID="lblSucessMessage" runat="server" />
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </asp:Panel>

            <!-- Updated banner with more modern styling -->
            <div class="banner shadow-sm">
                <h1 class="display-6">EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <!-- Added card container for better organization -->
            <div class="card mt-4">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <h5 id="pageTitle" runat="server" class="mb-0"></h5>
                    <asp:Button ID="btnRegisterComplaint" runat="server" 
                              CssClass="btn btn-primary" 
                              Text="Register Complaint" 
                              OnClick="btnRegisterComplaint_Click" />
                </div>

                <!-- Updated GridView with modern styling and responsive behavior -->
                <div class="card-body table-responsive">
                    <asp:GridView ID="gvComplaints" runat="server" 
                                AutoGenerateColumns="False" 
                                CssClass="table table-hover table-striped" 
                                OnRowDataBound="gvComplaints_RowDataBound" 
                                OnRowCommand="gvComplaints_RowCommand">
                        <!-- GridView columns remain the same but with updated styling -->
                        <Columns>
                            <!-- ... existing columns ... -->
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <!-- Updated to latest versions of JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript file -->
    <script src="~/Scripts/site.js"></script>
</body>
</html>