<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateRegisterComplaint.aspx.cs" Inherits="PimsApp.UpdateRegisterComplaint" %>

<!DOCTYPE html>

<html lang="en"> <!-- Added lang attribute for accessibility -->
<head runat="server">
    <meta charset="utf-8"> <!-- Added charset meta tag -->
    <meta name="viewport" content="width=device-width, initial-scale=1"> <!-- Added viewport meta tag for responsive design -->
    <title>Update your complaint/Know Resolution Status</title>
    <!-- Updated to Bootstrap 5.3.3 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <!-- Updated jQuery to 3.6.0 -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-vtXRMe3mGCbOeY7l30aIg8H9p3GdeSe4IFlP6G8JMa7o7lXvnz3GFKzPxzJdPfGK" crossorigin="anonymous"></script>
    <!-- Updated Popper.js to 2.11.6 -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/2.11.6/umd/popper.min.js" integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXiDAYTujMAeBAsjFuCZSmKbSSUnQlmh/jp3" crossorigin="anonymous"></script>
    <!-- Updated Bootstrap JS to 5.3.3 -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <!-- Updated Bootstrap Datepicker to 1.9.0 -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" integrity="sha384-CCTZv2q9I9m3UOxRLaJneXrrqKwUNOzZ6NGEUMwHtShDJ+nCoiXJCAgi05KfkLGY" crossorigin="anonymous">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js" integrity="sha384-duPanqjFWzqbOVa7YM3Rb7WIpLkXgKpZmrsKOkHFHOrTJIlM6HvV0W1ZBZn1OpM" crossorigin="anonymous"></script>
    
    <!-- Moved inline styles to a separate CSS file -->
    <link rel="stylesheet" href="styles/UpdateRegisterComplaint.css">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Updated navbar structure for better accessibility -->
        <nav class="navbar navbar-expand-lg navbar-light bg-light">
            <div class="container-fluid">
                <div class="d-flex justify-content-between w-100">
                    <div></div>
                    <div class="navbar-nav">
                        <asp:Label ID="lblWelcome" runat="server" Text="Welcome!" CssClass="navbar-text fw-bold" />
                    </div>
                </div>
            </div>
        </nav>

        <!-- Added banner for better visual hierarchy -->
        <div class="banner-container">
            <div class="banner">
                <h1 id="pageTitle">EcoSight: Ecological Incident Reporting & Monitoring</h1>
            </div>
            <h2 class="complaint-heading">Update Complaint - <asp:Label ID="lblComplaintId" runat="server" Text=""/></h2>
        </div>

        <div class="container form-container border p-4">
            <div class="row">
                <!-- Form Section (70%) -->
                <div class="col-md-9">
                    <asp:Panel ID="pnlComplaint" runat="server">
                        <!-- Removed redundant form groups and improved layout -->
                        <div class="row g-3">
                            <div class="col-md-6">
                                <label for="txtFirstName" class="form-label">First Name:</label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-6">
                                <label for="txtLastName" class="form-label">Last Name:</label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <!-- ... (other form fields) ... -->
                            
                            <!-- Updated file upload with modern practices -->
                            <div class="col-md-12">
                                <label for="fileUpload" class="form-label">Add New Images:</label>
                                <asp:FileUpload ID="fileUpload" runat="server" AllowMultiple="true" CssClass="form-control" onchange="handleFileUpload();" />
                            </div>
                            
                            <!-- ... (remaining form fields) ... -->
                            
                            <div class="col-md-12 mt-3">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="reg_valid" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>

                <!-- Preview Section (30%) -->
                <div class="col-md-3 image-preview-container" id="imagePreviewContainer">
                    <!-- Dynamic image previews will be inserted here -->
                </div>
            </div>
        </div>
    </form>

    <!-- Moved inline scripts to a separate JS file -->
    <script src="scripts/UpdateRegisterComplaint.js"></script>
</body>
</html>