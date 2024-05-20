<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignForm.aspx.cs" Inherits="TalkyTalk.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>TalkyTalk | Signup or signin here!</title>
    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet"
        integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="App_Themes/MainTheme/asset/style.css" />
</head>
<body class="bg-1">
    <form id="form1" runat="server">
        <% 
            if (Request.QueryString["IsSignup"] == "True")
            {
        %>
        <div class="alert alert-success alert-dismissible fade show mb-3 rounded-0" role="alert">
            You can now signin.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <% 
            }
            else if (Request.QueryString["IsSignup"] == "False")
            {
        %>
        <div class="alert alert-danger alert-dismissible fade show mb-3 rounded-0" role="alert">
            You failed to signup. Please register again.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <% } %>

        <% 
            if (Request.QueryString["IsAccountDeleted"] == "True")
            {
        %>
        <div class="alert alert-success alert-dismissible fade show mb-3 rounded-0" role="alert">
            Your account was successfully deleted!
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <% 
            }
        %>

        <% 
            if (Request.QueryString["IsSignin"] == "False")
            {
        %>
        <div class="alert alert-danger alert-dismissible fade show mb-3 rounded-0" role="alert">
            Please enter correct email and password.
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <% 
            }
        %>

        <% 
            if (!String.IsNullOrEmpty(Request.QueryString["BannedDays"]))
            {
        %>
        <div class="alert alert-danger alert-dismissible fade show mb-3 rounded-0" role="alert">
            You have been banned for <%=Request.QueryString["BannedDays"] %> days.
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <% 
            }
        %>
        <div
            class="container w-100 d-flex justify-content-between pb-5 pt-5">
            <div id="talkytalk-img">
                <img src="App_Themes/MainTheme/asset/img/illustration/logo.svg" alt="" class="w-100" />
            </div>
            <section id="signup" class="bg-white rounded shadow">
                <h1 class="jua text-center">Signup</h1>
                <div class="mb-3 mt-4">
                    <label for="username" class="form-label">Username:</label>
                    <asp:TextBox ID="R_UsernameTxt" runat="server" CssClass="form-control text-bg-light" TextMode="SingleLine"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="text-validator" runat="server"
                        ErrorMessage="White space is not allowed." ControlToValidate="R_UsernameTxt"
                        ValidationExpression="^[^\s]+$" Display="Dynamic" ValidationGroup="Signup" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-validator" runat="server" ErrorMessage="Please enter your username." ControlToValidate="R_UsernameTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email address:</label>
                    <asp:TextBox ID="R_EmailTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Email"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="White space is not allowed." CssClass="text-validator" ControlToValidate="R_EmailTxt"
                        ValidationExpression="^[^\s]+$" Display="Dynamic" ValidationGroup="Signup" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-validator" runat="server" ErrorMessage="Please enter your email." ControlToValidate="R_EmailTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password:</label>
                    <asp:TextBox ID="R_PasswordTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="text-validator" ErrorMessage="Please enter your password." ControlToValidate="R_PasswordTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="repeat-password" class="form-label">
                        Repeat Password:</label>
                    <asp:TextBox ID="R_RepeatPasswordTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="text-validator" ErrorMessage="Please enter repeat your password." ControlToValidate="R_RepeatPasswordTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="R_RepeatPasswordTxt" CssClass="text-validator" ControlToCompare="R_PasswordTxt" ErrorMessage="Please match your password." ToolTip="Password must be the same." Display="Dynamic" ValidationGroup="Signup"></asp:CompareValidator>
                </div>
                <div class="mb-3">
                    <label for="R_PinTxt" class="form-label">
                        PIN:</label>
                    <asp:TextBox ID="R_PinTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" CssClass="text-validator" runat="server" ErrorMessage="Please enter your PIN." ControlToValidate="R_PinTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="text-validator" runat="server" ValidationExpression="^\d{6}$" ErrorMessage="PIN must be exactly 6 digits." ControlToValidate="R_PinTxt" Display="Dynamic" ValidationGroup="Signup"></asp:RegularExpressionValidator>
                </div>
                <asp:Button ID="SignupBtn" class="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SignupSubmitBtn_Click" ValidationGroup="Signup" />
                <a class="text-decoration-underline d-block mt-2 text-center change-form pointer"><small>Already have an account?</small></a>
            </section>

            <section id="signin" class="bg-white rounded shadow d-none">
                <h1 class="jua text-center">Signin</h1>
                <div class="mb-3 mt-4">
                    <label for="email" class="form-label">Email address:</label>
                    <asp:TextBox ID="L_EmailTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="text-validator" runat="server" ErrorMessage="Please enter your email." ControlToValidate="L_EmailTxt" Display="Dynamic" ValidationGroup="Signin"></asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <label for="password" class="form-label">Password:</label>
                    <asp:TextBox ID="L_PasswordTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="text-validator" runat="server" ErrorMessage="Please enter your password." ControlToValidate="L_PasswordTxt" Display="Dynamic" ValidationGroup="Signin"></asp:RequiredFieldValidator>
                </div>
                <asp:Button ID="SigninBtn" class="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SigninSubmitBtn_Click" ValidationGroup="Signin" />
                <a class="text-decoration-underline d-block mt-2 text-center pointer change-form"><small>Doesn't have any account?</small></a>
            </section>
        </div>
        <script
            src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
            integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
            crossorigin="anonymous"></script>
        <script>
            const changeForms = document.querySelectorAll(".change-form");
            const signupSection = document.getElementById("signup");
            const signinSection = document.getElementById("signin");

            changeForms.forEach((changeForm) => {
                changeForm.addEventListener("click", () => {
                    signupSection.classList.toggle("d-none");
                    signinSection.classList.toggle("d-none");

                    const isSignInVisible = !signinSection.classList.contains("d-none");

                    if (isSignInVisible) {
                        signinSection.classList.add("d-flex", "justify-content-center", "flex-column");
                    } else {
                        signinSection.classList.remove("d-flex", "justify-content-center", "flex-column");
                    }

                    void signupSection.offsetWidth;
                    void signinSection.offsetWidth;

                    signupSection.style.opacity = isSignInVisible ? 0 : 1;
                    signinSection.style.opacity = isSignInVisible ? 1 : 0;
                });
            });
        </script>
    </form>
</body>
</html>
