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
    <link rel="stylesheet" href="/styles/main.css">
    
    <!-- Added Font Awesome for icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <!-- Updated navbar with modern Bootstrap classes -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
            <div class="container">
                <span class="navbar-brand">EcoSight Dashboard</span>
                <div class="ms-auto">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <asp:Label ID="lblWelcome" runat="server" CssClass="nav-link fw-bold" />
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
            <!-- Added card component for better organization -->
            <div class="card shadow-sm">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">Ecological Incident Management System</h5>
                </div>
                <div class="card-body">
                    <!-- Added toast for success messages -->
                    <div class="toast-container position-fixed top-0 end-0 p-3">
                        <asp:Panel ID="successToast" runat="server" CssClass="toast" Visible="false">
                            <div class="toast-header">
                                <i class="fas fa-check-circle text-success me-2"></i>
                                <strong class="me-auto">Success</strong>
                                <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
                            </div>
                            <div class="toast-body">
                                <asp:Label ID="lblSucessMessage" runat="server"></asp:Label>
                            </div>
                        </asp:Panel>
                    </div>

                    <!-- Updated button with icon -->
                    <div class="mb-4">
                        <asp:Button ID="btnRegisterComplaint" runat="server" 
                                  CssClass="btn btn-primary" 
                                  Text="<i class='fas fa-plus'></i> Register Complaint" 
                                  OnClick="btnRegisterComplaint_Click" />
                    </div>

                    <!-- Updated GridView with modern styling -->
                    <asp:GridView ID="gvComplaints" runat="server" 
                                CssClass="table table-hover table-striped" 
                                AutoGenerateColumns="False"
                                OnRowDataBound="gvComplaints_RowDataBound" 
                                OnRowCommand="gvComplaints_RowCommand"
                                HeaderStyle-CssClass="table-dark">
                        <!-- GridView columns remain same but with updated styling -->
                        <!-- ... existing columns ... -->
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>

    <!-- Updated to modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript -->
    <script src="/scripts/main.js"></script>
</body>
</html>