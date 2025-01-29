<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsive design -->
    <title>Home Page - EcoSight</title> <!-- Updated title to match the application name -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet"> <!-- Updated Font Awesome version -->

    <style>
        /* CSS styles remain largely unchanged, but consider moving to an external CSS file for better separation of concerns */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="menu-bar">
                <label>Welcome to EcoSight: Ecological Incident Reporting & Monitoring</label>
            </div>

            <div class="content">
                <h1 class="display-4">Citizen Repair: Report Public Issues Here</h1> <!-- Changed h3 to h1 for better SEO -->
                <div class="card-container">
                    <div class="form-icon"><i class="fas fa-user"></i></div>
                    <h2 class="title">Login</h2> <!-- Changed h3 to h2 for better hierarchy -->

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <asp:Button ID="btnLoginUser" runat="server" CssClass="btn" Text="Login" OnClick="btnLoginUser_Click" />
                        <div class="forgot-password">
                            <asp:HyperLink ID="hlForgotPassword" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink> <!-- Updated to use ASP.NET HyperLink control -->
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>

    <!-- Added JavaScript files at the end of the body for better performance -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>