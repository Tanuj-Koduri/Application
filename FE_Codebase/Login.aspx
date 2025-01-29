<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for better accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsive design -->
    <title>Login - EcoSight</title> <!-- Updated title for better SEO -->
    <!-- Updated to latest Bootstrap version -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <!-- Updated to latest Font Awesome version -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    
    <!-- Moved styles to an external CSS file for better separation of concerns -->
    <link href="~/Styles/Login.css" rel="stylesheet" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="menu-bar">
                <h1>Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1> <!-- Changed label to h1 for better semantics -->
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2> <!-- Changed h3 to h2 for proper heading hierarchy -->
                <div class="card-container">
                    <div class="form-icon"><i class="fas fa-user"></i></div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <div class="form-group">
                            <label for="txtPassword">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" required></asp:TextBox> <!-- Added required attribute -->
                        </div>
                        <asp:Button ID="btnLoginUser" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="btnLoginUser_Click" /> <!-- Updated button class -->
                        <div class="forgot-password">
                            <asp:HyperLink ID="lnkForgotPassword" runat="server" NavigateUrl="~/ForgotPassword.aspx">Forgot Password?</asp:HyperLink> <!-- Changed to server-side control -->
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="message" Visible="false"></asp:Label>
            </div>
        </div>
    </form>

    <!-- Added Bootstrap JS and Popper.js for any potential Bootstrap functionality -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.min.js" integrity="sha384-fbbOQedDUMZZ5KreZpsbe1LCZPVmfTnH7ois6mU1QK+m14rQ1l2bGBq41eYeM/fS" crossorigin="anonymous"></script>
</body>
</html>