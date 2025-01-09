<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset for proper encoding -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta for responsiveness -->
    <title>Home Page - EcoSight</title> <!-- Updated title to match the app name -->
    <!-- Updated to latest Bootstrap version -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <!-- Updated to latest Font Awesome version -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <!-- Moved styles to a separate CSS file for better organization -->
    <link href="Styles/Login.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="menu-bar">
                <h1>Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1> <!-- Changed label to h1 for semantic HTML -->
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2> <!-- Changed h3 to h2 for proper heading hierarchy -->
                <div class="card-container">
                    <div class="form-icon"><i class="fas fa-user"></i></div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" required></asp:TextBox> <!-- Added required attribute for form validation -->
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Password" required></asp:TextBox> <!-- Added required attribute for form validation -->
                        </div>
                        <asp:Button ID="btnLoginUser" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLoginUser_Click" /> <!-- Added Bootstrap button class -->
                        <div class="forgot-password">
                            <a href="ForgotPassword.aspx">Forgot Password?</a> <!-- Updated link to a separate page -->
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>
    <!-- Added JavaScript files at the end of the body for better performance -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
</body>
</html>