<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added viewport meta for responsiveness -->
    <meta name="description" content="EcoSight Login - Ecological Incident Reporting & Monitoring"> <!-- Added meta description for SEO -->
    <title>Login - EcoSight</title> <!-- Updated title for better SEO -->
    
    <!-- Updated to latest Bootstrap version and added SRI hash -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" 
          crossorigin="anonymous">
    
    <!-- Updated Font Awesome to latest version -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" 
          rel="stylesheet"
          integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA=="
          crossorigin="anonymous">

    <!-- Moved styles to external CSS file: styles.css -->
    <link href="/css/styles.css" rel="stylesheet">
</head>
<body>
    <form id="loginForm" runat="server" class="needs-validation" novalidate> <!-- Added form validation -->
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4 mb-0">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2>
                <div class="card-container">
                    <div class="form-icon">
                        <i class="fas fa-user-circle fa-3x"></i> <!-- Updated icon -->
                    </div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" 
                                       runat="server" 
                                       CssClass="form-control" 
                                       placeholder="Enter username"
                                       required="required"
                                       autocomplete="username"> <!-- Added security attributes -->
                            </asp:TextBox>
                            <div class="invalid-feedback">Please enter your username.</div>
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
                                           autocomplete="current-password"
                                           pattern="^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"> <!-- Added password requirements -->
                                </asp:TextBox>
                                <button class="btn btn-outline-secondary" 
                                        type="button" 
                                        id="togglePassword">
                                    <i class="far fa-eye"></i>
                                </button>
                            </div>
                            <div class="invalid-feedback">Password must be at least 8 characters long.</div>
                        </div>
                        
                        <asp:Button ID="btnLoginUser" 
                                  runat="server" 
                                  CssClass="btn btn-primary w-100" 
                                  Text="Login" 
                                  OnClick="btnLoginUser_Click"
                                  UseSubmitBehavior="true" />

                        <div class="forgot-password mt-3">
                            <asp:HyperLink ID="forgotPasswordLink" 
                                         runat="server" 
                                         NavigateUrl="~/ForgotPassword.aspx"
                                         CssClass="text-primary">
                                Forgot Password?
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
                
                <asp:Label ID="lblMessage" 
                          runat="server" 
                          CssClass="alert alert-danger mt-3" 
                          Visible="false"
                          Role="alert"> <!-- Added ARIA role -->
                </asp:Label>
            </div>
        </div>
    </form>

    <!-- Added modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
            crossorigin="anonymous"></script>
    
    <!-- Added custom JavaScript for form validation and password toggle -->
    <script src="/js/login.js"></script>
</body>
</html>