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

    <!-- Moved styles to external CSS file for better maintenance -->
    <link href="~/Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="loginForm" runat="server" autocomplete="off"> <!-- Added autocomplete off for security -->
        <div class="container">
            <div class="menu-bar">
                <label aria-label="Welcome message">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</label>
            </div>

            <div class="content">
                <h1 class="display-4">Citizen Repair: Report Public Issues Here</h1> <!-- Changed h3 to h1 for SEO -->
                <div class="card-container">
                    <div class="form-icon" aria-hidden="true"><i class="fas fa-user"></i></div>
                    <h2 class="title">Login</h2> <!-- Changed h3 to h2 for proper hierarchy -->

                    <div class="form-horizontal">
                        <!-- Added form validation -->
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
                        
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label>
                            <asp:TextBox ID="txtUsername" runat="server" 
                                       CssClass="form-control" 
                                       placeholder="Enter username"
                                       required="required"
                                       MaxLength="50"> <!-- Added MaxLength for security -->
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsername" 
                                                      runat="server" 
                                                      ControlToValidate="txtUsername"
                                                      ErrorMessage="Username is required"
                                                      CssClass="text-danger">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label for="txtPassword" class="form-label">Password</label>
                            <asp:TextBox ID="txtPassword" 
                                       runat="server" 
                                       CssClass="form-control" 
                                       TextMode="Password"
                                       placeholder="Enter password"
                                       required="required"
                                       MaxLength="100"> <!-- Added MaxLength for security -->
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" 
                                                      runat="server" 
                                                      ControlToValidate="txtPassword"
                                                      ErrorMessage="Password is required"
                                                      CssClass="text-danger">
                            </asp:RequiredFieldValidator>
                        </div>

                        <!-- Added anti-forgery token -->
                        <asp:HiddenField ID="AntiForgeryToken" runat="server" />

                        <asp:Button ID="btnLoginUser" 
                                  runat="server" 
                                  CssClass="btn btn-primary" 
                                  Text="Login" 
                                  OnClick="btnLoginUser_Click"
                                  UseSubmitBehavior="true" />

                        <div class="forgot-password">
                            <asp:HyperLink ID="hlForgotPassword" 
                                         runat="server" 
                                         NavigateUrl="~/ForgotPassword.aspx"
                                         Text="Forgot Password?">
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
                
                <!-- Updated message display -->
                <asp:Panel ID="pnlMessage" runat="server" Visible="false">
                    <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </form>

    <!-- Added reCAPTCHA script -->
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    
    <!-- Added modern JavaScript libraries -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" 
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" 
            crossorigin="anonymous"></script>
</body>
</html>