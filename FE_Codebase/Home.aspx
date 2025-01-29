<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta for responsiveness -->
    <title>Admin Dashboard - EcoSight</title> <!-- Updated meaningful title -->
    
    <!-- Updated to latest Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" 
          crossorigin="anonymous">
    
    <!-- Moved styles to external CSS file: styles.css -->
    <link rel="stylesheet" href="~/Content/styles.css">
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <!-- Updated navbar with modern Bootstrap 5 classes -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light shadow-sm">
            <div class="container">
                <span class="navbar-brand">EcoSight Dashboard</span>
                <div class="d-flex align-items-center">
                    <asp:Label ID="lblWelcome" runat="server" CssClass="me-3 fw-bold" />
                    <asp:Button ID="btnLogout" runat="server" 
                              CssClass="btn btn-danger" 
                              Text="Logout" 
                              OnClick="btnLogout_Click"
                              OnClientClick="return confirm('Are you sure you want to logout?');" /> <!-- Added confirmation -->
                </div>
            </div>
        </nav>

        <div class="container mt-4">
            <!-- Added alert for success message -->
            <asp:Panel ID="successAlert" runat="server" CssClass="alert alert-success alert-dismissible fade show" 
                     role="alert" Visible="false">
                <asp:Label ID="lblSucessMessage" runat="server"></asp:Label>
                <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
            </asp:Panel>

            <!-- Updated header section -->
            <div class="d-flex justify-content-between align-items-center mb-4">
                <h1 id="pageTitle" runat="server" class="h3"></h1>
                <asp:Button ID="btnRegisterComplaint" runat="server" 
                          CssClass="btn btn-primary" 
                          Text="Register Complaint" 
                          OnClick="btnRegisterComplaint_Click" />
            </div>

            <!-- Modernized GridView with responsive table -->
            <div class="table-responsive">
                <asp:GridView ID="gvComplaints" runat="server" 
                            CssClass="table table-striped table-hover"
                            AutoGenerateColumns="False"
                            OnRowDataBound="gvComplaints_RowDataBound"
                            OnRowCommand="gvComplaints_RowCommand"
                            DataKeyNames="ComplaintId"> <!-- Added DataKeyNames for better security -->
                    <Columns>
                        <!-- Updated columns with modern styling -->
                        <asp:BoundField DataField="ComplaintId" HeaderText="ID" HeaderStyle-CssClass="fw-bold" />
                        <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-CssClass="fw-bold" />
                        <!-- ... other columns ... -->

                        <!-- Updated template fields -->
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlCurrentStatus" runat="server" 
                                                CssClass="form-select"
                                                AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCurrentStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="Not Started" Value="1" />
                                    <asp:ListItem Text="In Progress" Value="2" />
                                    <asp:ListItem Text="Resolved" Value="3" />
                                    <asp:ListItem Text="Re-opened" Value="4" />
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>

    <!-- Updated to latest Bootstrap 5 and added defer -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" 
            crossorigin="anonymous" defer></script>
    
    <!-- Added custom JavaScript file -->
    <script src="~/Scripts/site.js" defer></script>
</body>
</html>