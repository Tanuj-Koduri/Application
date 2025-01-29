<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <meta name="description" content="EcoSight Login - Ecological Incident Reporting & Monitoring"> <!-- Added meta description for SEO -->
    <title>Login - EcoSight</title>

    <!-- Updated to latest versions of CSS frameworks -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" 
          integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" 
          crossorigin="anonymous">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" 
          rel="stylesheet"
          integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
          crossorigin="anonymous">

    <!-- Moved styles to external CSS file -->
    <link href="/styles/login.css" rel="stylesheet">
</head>
<body>
    <form id="loginForm" runat="server" class="needs-validation" novalidate>
        <!-- Added CSRF protection -->
        <asp:AntiForgeryToken runat="server" />
        
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4 mb-0">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2>
                <div class="card-container">
                    <div class="form-icon">
                        <i class="fas fa-user" aria-hidden="true"></i>
                    </div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <!-- Added input validation and security measures -->
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" 
                                        CssClass="form-control" 
                                        Placeholder="Enter your username"
                                        required="required"
                                        MaxLength="50"
                                        autocomplete="username">
                            </asp:TextBox>
                            <div class="invalid-feedback">Please enter your username.</div>
                        </div>
                        
                        <div class="form-group">
                            <label for="txtPassword" class="form-label">Password</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPassword" runat="server" 
                                            CssClass="form-control" 
                                            TextMode="Password" 
                                            Placeholder="Enter your password"
                                            required="required"
                                            MaxLength="100"
                                            autocomplete="current-password">
                                </asp:TextBox>
                                <button type="button" class="btn btn-outline-secondary" id="togglePassword">
                                    <i class="far fa-eye" aria-hidden="true"></i>
                                </button>
                            </div>
                            <div class="invalid-feedback">Please enter your password.</div>
                        </div>

                        <!-- Added reCAPTCHA protection -->
                        <div class="g-recaptcha mb-3" 
                             data-sitekey="your-site-key">
                        </div>

                        <asp:Button ID="btnLoginUser" runat="server" 
                                  CssClass="btn btn-primary w-100" 
                                  Text="Login" 
                                  OnClick="btnLoginUser_Click"
                                  UseSubmitBehavior="true" />

                        <div class="forgot-password mt-3">
                            <a href="ForgotPassword.aspx" class="text-decoration-none">Forgot Password?</a>
                        </div>
                    </div>
                </div>
                
                <!-- Improved message display -->
                <asp:Panel ID="messagePanel" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </form>

    <!-- Added JavaScript resources -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
            crossorigin="anonymous"></script>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script src="/scripts/login.js"></script>
</body>
</html>