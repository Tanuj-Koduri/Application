<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsiveness -->
    <title>Login - EcoSight</title> <!-- Updated title for SEO -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">
    <!-- Moved inline styles to a separate CSS file for better maintainability -->
    <link href="/css/login.css" rel="stylesheet">
</head>
<body>
    <form id="loginForm" runat="server"> <!-- Updated form ID for clarity -->
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1> <!-- Changed label to h1 for semantic HTML -->
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2> <!-- Changed h3 to h2 for proper heading hierarchy -->
                <div class="card-container">
                    <div class="form-icon"><i class="fas fa-user" aria-hidden="true"></i></div> <!-- Added aria-hidden for accessibility -->
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Password" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <asp:Button ID="btnLoginUser" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLoginUser_Click" /> <!-- Updated button class -->
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
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>