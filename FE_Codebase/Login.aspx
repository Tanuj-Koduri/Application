<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <meta name="description" content="EcoSight Login - Ecological Incident Reporting & Monitoring"> <!-- Added SEO meta -->
    <title>Login - EcoSight</title>

    <!-- Updated to latest Bootstrap version and added SRI hash -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" 
          integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" 
          crossorigin="anonymous">
    
    <!-- Updated Font Awesome to latest version -->
    <link rel="stylesheet" 
          href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" 
          integrity="sha512-JEHF9/8HU4lWYxQ8YqG0Wz8D/1zEd6v2z6u8pDJkRd8fM0tD/..." 
          crossorigin="anonymous">

    <!-- Moved styles to external CSS file: styles.css -->
    <link rel="stylesheet" href="~/Content/styles.css">
</head>
<body>
    <form id="loginForm" runat="server" autocomplete="off"> <!-- Added autocomplete for security -->
        <div class="container">
            <div class="menu-bar">
                <h1 class="text-center">Welcome to EcoSight</h1> <!-- Semantic heading -->
            </div>

            <main class="content"> <!-- Added semantic main tag -->
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2>
                <div class="card-container">
                    <div class="form-icon">
                        <i class="fas fa-user" aria-hidden="true"></i> <!-- Added aria-hidden -->
                    </div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <!-- Added input validation and security measures -->
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" 
                                        CssClass="form-control" 
                                        placeholder="Enter username"
                                        required="required"
                                        MaxLength="50"
                                        autocomplete="username">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" runat="server" 
                                        CssClass="form-control" 
                                        TextMode="Password"
                                        placeholder="Enter password"
                                        required="required"
                                        MaxLength="100"
                                        autocomplete="current-password">
                            </asp:TextBox>
                        </div>

                        <!-- Added CSRF protection -->
                        <asp:HiddenField ID="AntiForgeryToken" runat="server" />
                        
                        <!-- Updated button with loading state -->
                        <asp:Button ID="btnLoginUser" runat="server" 
                                  CssClass="btn btn-primary w-100" 
                                  Text="Login" 
                                  OnClick="btnLoginUser_Click"
                                  data-loading-text="Logging in..." />

                        <div class="forgot-password mt-3">
                            <asp:HyperLink ID="lnkForgotPassword" runat="server" 
                                         NavigateUrl="~/ForgotPassword.aspx">
                                Forgot Password?
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>

                <!-- Updated message display -->
                <asp:Panel ID="messagePanel" runat="server" Visible="false" CssClass="alert mt-3" role="alert">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </main>
        </div>
    </form>

    <!-- Added JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
    <script src="~/Scripts/login.js"></script>
</body>
</html>