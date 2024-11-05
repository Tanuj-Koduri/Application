<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PimsApp.Home" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"> <!-- Added viewport meta tag for responsiveness -->
    <title>Admin Page - Dashboard</title>
    <!-- Updated Bootstrap to the latest version (5.3.0) -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <!-- Moved styles to a separate CSS file for better organization -->
    <link rel="stylesheet" href="~/Content/styles.css">
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
            <asp:Label ID="lblSucessMessage" runat="server" CssClass="alert alert-success d-none" role="alert"></asp:Label>
            
            <div class="mb-4 text-end">
                <asp:Button ID="btnRegisterComplaint" runat="server" CssClass="btn btn-primary" Text="Register Complaint" OnClick="btnRegisterComplaint_Click" />
            </div>

            <!-- Updated GridView with modern Bootstrap classes and responsive design -->
            <asp:GridView ID="gvComplaints" runat="server" AutoGenerateColumns="False" 
                          CssClass="table table-striped table-bordered table-hover" 
                          OnRowDataBound="gvComplaints_RowDataBound" 
                          OnRowCommand="gvComplaints_RowCommand"
                          DataKeyNames="ComplaintId"> <!-- Added DataKeyNames for better data handling -->
                <Columns>
                    <asp:BoundField DataField="ComplaintId" HeaderText="Complaint Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="EmpId" HeaderText="Emp Id" ItemStyle-CssClass="text-nowrap" />
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
                            <asp:DropDownList ID="ddlCurrentStatus" runat="server" CssClass="form-select" 
                                              AutoPostBack="True" OnSelectedIndexChanged="ddlCurrentStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Not Started" Value="Not Started" />
                                <asp:ListItem Text="In Progress" Value="In Progress" />
                                <asp:ListItem Text="Resolved" Value="Resolved" />
                                <asp:ListItem Text="Re-opened" Value="Re-opened" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action Taken">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfComplaintId" runat="server" Value='<%# Eval("ComplaintId") %>' />
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>' CssClass="d-block mb-2" />
                            <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control mb-2" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" CssClass="btn btn-primary mb-2" 
                                        CommandName="UpdateStatus" CommandArgument="<%# Container.DataItemIndex %>" />
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-secondary mb-2" 
                                        CommandName="Edit" OnClick="btnEditComplaint_Click" 
                                        CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>

    <!-- Updated to the latest versions of Bootstrap and its dependencies -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js" integrity="sha384-fbbOQedDUMZZ5KreZpsbe1LCZPVmfTnH7ois6mU1QK+m14rQ1l2bGBq41eYeM/fS" crossorigin="anonymous"></script>
</body>
</html>