<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>
<!DOCTYPE html>
<html lang="en"> <!-- Added language attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0"> <!-- Added responsive viewport -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> <!-- Added IE compatibility -->
    <title>Login - EcoSight</title> <!-- Updated title for better SEO -->
    
    <!-- Updated to latest Bootstrap version and added SRI hashes -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" 
          rel="stylesheet" 
          integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" 
          crossorigin="anonymous">
    
    <!-- Updated Font Awesome to latest version -->
    <link rel="stylesheet" 
          href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" 
          integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA==" 
          crossorigin="anonymous">

    <!-- Moved styles to external stylesheet -->
    <link href="/styles/login.css" rel="stylesheet">
</head>
<body>
    <form id="loginForm" runat="server" autocomplete="off"> <!-- Added autocomplete off for security -->
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2>
                <div class="card-container">
                    <div class="form-icon">
                        <i class="fas fa-user-circle fa-3x"></i> <!-- Updated icon -->
                    </div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <!-- Added AJAX UpdatePanel for smooth form submissions -->
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="txtUsername" class="form-label">Username</label>
                                    <asp:TextBox ID="txtUsername" 
                                               runat="server" 
                                               CssClass="form-control" 
                                               placeholder="Enter username"
                                               required="required" 
                                               MaxLength="50"> <!-- Added validation -->
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
                                    <div class="input-group"> <!-- Added password visibility toggle -->
                                        <asp:TextBox ID="txtPassword" 
                                                   runat="server" 
                                                   CssClass="form-control" 
                                                   TextMode="Password" 
                                                   placeholder="Enter password"
                                                   required="required"
                                                   MaxLength="100">
                                        </asp:TextBox>
                                        <button type="button" 
                                                class="btn btn-outline-secondary" 
                                                onclick="togglePassword()">
                                            <i class="far fa-eye"></i>
                                        </button>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvPassword" 
                                                              runat="server" 
                                                              ControlToValidate="txtPassword"
                                                              ErrorMessage="Password is required"
                                                              CssClass="text-danger">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <!-- Added reCAPTCHA -->
                                <div class="g-recaptcha" 
                                     data-sitekey="your-site-key">
                                </div>

                                <asp:Button ID="btnLoginUser" 
                                          runat="server" 
                                          CssClass="btn btn-primary btn-block" 
                                          Text="Login" 
                                          OnClick="btnLoginUser_Click" />

                                <div class="forgot-password mt-3">
                                    <asp:HyperLink ID="hlForgotPassword" 
                                                 runat="server" 
                                                 NavigateUrl="~/ForgotPassword.aspx">
                                        Forgot Password?
                                    </asp:HyperLink>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                
                <!-- Added bootstrap alert for messages -->
                <asp:Panel ID="pnlMessage" 
                         runat="server" 
                         CssClass="alert alert-danger mt-3" 
                         Visible="false">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </form>

    <!-- Added JavaScript -->
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    <script>
        function togglePassword() {
            const passwordInput = document.getElementById('<%= txtPassword.ClientID %>');
            passwordInput.type = passwordInput.type === 'password' ? 'text' : 'password';
        }
    </script>
</body>
</html>