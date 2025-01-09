<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for better accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"> <!-- Added viewport meta tag for responsive design -->
    <title>Admin Page - Dashboard</title>
    <!-- Updated to Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous">
    <!-- Moved styles to a separate CSS file for better separation of concerns -->
    <link rel="stylesheet" href="/styles/home.css">
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
            <h5 id="pageTitle" runat="server" class="mb-3 text-center"></h5>
            <asp:Label ID="lblSucessMessage" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>
            
            <div class="mb-4 text-end">
                <asp:Button ID="btnRegisterComplaint" runat="server" CssClass="btn btn-primary" Text="Register Complaint" OnClick="btnRegisterComplaint_Click" />
            </div>
            
            <!-- Updated GridView with modern Bootstrap classes and aria attributes for accessibility -->
            <asp:GridView ID="gvComplaints" runat="server" AutoGenerateColumns="False" 
                          CssClass="table table-striped table-bordered table-hover" 
                          OnRowDataBound="gvComplaints_RowDataBound" 
                          OnRowCommand="gvComplaints_RowCommand"
                          aria-label="Complaints Table">
                <Columns>
                    <asp:BoundField DataField="ComplaintId" HeaderText="Complaint Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="EmpId" HeaderText="Emp Id" HeaderStyle-CssClass="nowrap-header" ItemStyle-CssClass="nowrap-item" />
                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="email-column" />
                    <asp:BoundField DataField="ContactNumber" HeaderText="Number" />
                    <asp:BoundField DataField="DateTimeCapture" HeaderText="Date/Time of Capture" DataFormatString="{0:dd-MM-yyyy HH:mm}" />
                    <asp:BoundField DataField="PictureCaptureLocation" HeaderText="Location" />
                    <asp:BoundField DataField="Comments" HeaderText="Description" />
                    
                    <asp:TemplateField HeaderText="Images/Pictures">
                        <ItemTemplate>
                            <asp:Literal ID="litImages" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Current Status">
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

                    <asp:TemplateField HeaderText="Action Taken">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfComplaintId" runat="server" Value='<%# Eval("ComplaintId") %>' />
                            <asp:Label ID="lblheader" runat="server" Text='<%# Eval("Status") %>' CssClass="d-block mb-2" />
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="d-block mb-2" />
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control mb-2" TextMode="MultiLine" Rows="2" Style="width: 100%;"></asp:TextBox>
                            <div class="button-group">
                                <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" CssClass="btn btn-primary mb-2" CommandName="UpdateStatus" CommandArgument="<%# Container.DataItemIndex %>" />
                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-secondary mb-2" CommandName="Edit" OnClick="btnEditComplaint_Click" CommandArgument="<%# Container.DataItemIndex %>" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <!-- Updated to latest versions of Bootstrap and jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</body>
</html>