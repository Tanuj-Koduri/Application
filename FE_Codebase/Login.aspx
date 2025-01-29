<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> <!-- Added IE compatibility -->
    <title>Login - EcoSight</title> <!-- Updated title for better SEO -->
    
    <!-- Updated to latest Bootstrap version with SRI hash -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" 
          crossorigin="anonymous">
    
    <!-- Updated Font Awesome to latest version with SRI hash -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" 
          rel="stylesheet"
          integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
          crossorigin="anonymous">

    <!-- Moved styles to external CSS file for better maintainability -->
    <link href="/css/login.css" rel="stylesheet">
</head>
<body>
    <form id="loginForm" runat="server" class="needs-validation" novalidate>
        <!-- Added CSRF protection -->
        <asp:AntiForgeryToken runat="server" />
        
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4 text-primary mb-0">EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2>
                <div class="card-container">
                    <div class="form-icon">
                        <i class="fas fa-user-circle fa-3x"></i>
                    </div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <!-- Added input validation and security measures -->
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" 
                                       runat="server" 
                                       CssClass="form-control" 
                                       placeholder="Enter username"
                                       required="required"
                                       MaxLength="50"
                                       autocomplete="username">
                            </asp:TextBox>
                            <div class="invalid-feedback">Please enter a username.</div>
                        </div>
                        
                        <div class="form-group">
                            <label for="txtPassword" class="form-label">Password</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtPassword" 
                                           runat="server" 
                                           CssClass="form-control" 
                                           TextMode="Password" 
                                           placeholder="Enter password"
                                           required="required"
                                           pattern="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"
                                           autocomplete="current-password">
                                </asp:TextBox>
                                <button class="btn btn-outline-secondary" 
                                        type="button" 
                                        id="togglePassword">
                                    <i class="far fa-eye"></i>
                                </button>
                                <div class="invalid-feedback">
                                    Password must be at least 8 characters long and contain letters and numbers.
                                </div>
                            </div>
                        </div>

                        <!-- Added reCAPTCHA protection -->
                        <div class="g-recaptcha mb-3" 
                             data-sitekey="YOUR_RECAPTCHA_SITE_KEY">
                        </div>

                        <asp:Button ID="btnLoginUser" 
                                  runat="server" 
                                  CssClass="btn btn-primary w-100" 
                                  Text="Login" 
                                  OnClick="btnLoginUser_Click"
                                  UseSubmitBehavior="true" />

                        <div class="forgot-password mt-3">
                            <a href="ForgotPassword.aspx" 
                               class="text-decoration-none">Forgot Password?</a>
                        </div>
                    </div>
                </div>
                
                <!-- Updated alert messaging -->
                <asp:Panel ID="alertMessage" 
                         runat="server" 
                         CssClass="alert alert-danger mt-3" 
                         Visible="false">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </form>

    <!-- Added modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script src="/js/login.js"></script>
</body>
</html>