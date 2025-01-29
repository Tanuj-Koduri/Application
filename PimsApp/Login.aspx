<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PimsApp.Login" %>

<!DOCTYPE html>
<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsiveness -->
    <title>Login - EcoSight</title> <!-- Updated title for better SEO -->
    <!-- Updated to latest Bootstrap version -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous">
    <!-- Updated to latest Font Awesome version -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">

    <style>
        /* CSS styles remain largely unchanged, but consider moving to an external CSS file for better maintainability */
        /* ... (existing styles) ... */
    </style>
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate> <!-- Added form validation classes -->
        <div class="container">
            <div class="menu-bar">
                <h1 class="h4">Welcome to EcoSight: Ecological Incident Reporting & Monitoring</h1> <!-- Changed label to h1 for better semantics -->
            </div>

            <div class="content">
                <h2 class="display-4">Citizen Repair: Report Public Issues Here</h2> <!-- Changed h3 to h2 for better hierarchy -->
                <div class="card-container">
                    <div class="form-icon"><i class="fas fa-user"></i></div>
                    <h3 class="title">Login</h3>

                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtUsername" class="form-label">Username</label> <!-- Added form-label class -->
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="Username" required></asp:TextBox> <!-- Added required attribute -->
                            <div class="invalid-feedback">Please enter your username.</div> <!-- Added feedback for validation -->
                        </div>
                        <div class="form-group">
                            <label for="txtPassword" class="form-label">Password</label> <!-- Added form-label class -->
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="Password" required></asp:TextBox> <!-- Added required attribute -->
                            <div class="invalid-feedback">Please enter your password.</div> <!-- Added feedback for validation -->
                        </div>
                        <asp:Button ID="btnLoginUser" runat="server" CssClass="btn btn-primary w-100" Text="Login" OnClick="btnLoginUser_Click" /> <!-- Updated button classes -->
                        <div class="forgot-password mt-3"> <!-- Added margin-top utility class -->
                            <a href="ForgotPassword.aspx">Forgot Password?</a> <!-- Updated link to a separate page -->
                        </div>
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="message mt-3" Visible="false"></asp:Label> <!-- Added margin-top utility class -->
            </div>
        </div>
    </form>

    <!-- Added Bootstrap and custom JavaScript for form validation -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" integrity="sha384-geWF76RCwLtnZ8qwWowPQNguL3RmwHVBC9FhGdlKrxdiJJigb/j/68SIy3Te4Bkz" crossorigin="anonymous"></script>
    <script>
        // Example starter JavaScript for disabling form submissions if there are invalid fields
        (function () {
            'use strict'

            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.querySelectorAll('.needs-validation')

            // Loop over them and prevent submission
            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault()
                            event.stopPropagation()
                        }

                        form.classList.add('was-validated')
                    }, false)
                })
        })()
    </script>
</body>
</html>