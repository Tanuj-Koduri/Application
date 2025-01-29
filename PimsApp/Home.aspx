<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"> <!-- Added viewport meta tag for responsiveness -->
    <title>Admin Page - Dashboard</title>
    <!-- Updated Bootstrap to the latest version (5.1.3) -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <!-- Moved inline styles to a separate CSS file for better maintainability -->
    <link rel="stylesheet" href="~/Content/Styles/Home.css">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Updated navbar to Bootstrap 5 syntax -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <div class="navbar-nav ms-auto">
                    <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" CssClass="nav-item nav-link fw-bold" />
                    <asp:Button ID="btnLogout" runat="server" CssClass="btn btn-danger" Text="Logout" OnClick="btnLogout_Click" />
                </div>
            </div>
        </nav>

        <div class="container mt-5">
            <div class="banner">
                EcoSight: Ecological Incident Reporting & Monitoring
            </div>
            <h5 id="pageTitle" runat="server" class="mb-0"></h5>
            <asp:Label ID="lblSucessMessage" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
            <div class="row mb-4 align-items-center">
                <div class="text-end mb-4">
                    <asp:Button ID="btnRegisterComplaint" runat="server" CssClass="btn btn-primary" Text="Register Complaint" OnClick="btnRegisterComplaint_Click" />
                </div>
                <hr />
                <!-- Updated GridView to use Bootstrap 5 classes -->
                <asp:GridView ID="gvComplaints" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" OnRowDataBound="gvComplaints_RowDataBound" OnRowCommand="gvComplaints_RowCommand">
                    <Columns>
                        <!-- ... (columns remain the same) ... -->
                        <asp:TemplateField HeaderText="Current Status" Visible="True">
                            <ItemTemplate>
                                <div class="form-group" style="width: 150px;">
                                    <asp:DropDownList ID="ddlCurrentStatus" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrentStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="Not Started" Value="Not Started" />
                                        <asp:ListItem Text="In Progress" Value="In Progress" />
                                        <asp:ListItem Text="Resolved" Value="Resolved" />
                                        <asp:ListItem Text="Re-opened" Value="Re-opened" />
                                    </asp:DropDownList>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <!-- ... (other columns remain the same) ... -->
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>

    <!-- Updated to Bootstrap 5 and added defer attribute for performance -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous" defer></script>
</body>
</html>