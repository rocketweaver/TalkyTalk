<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultUser.Master" AutoEventWireup="true" CodeBehind="EditProfileForm.aspx.cs" Inherits="TalkyTalk.EditProfileForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div
        class="modal fade"
        id="DeleteAccModal"
        tabindex="-1"
        aria-labelledby="DeleteAccLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="DeleteAccLabel">Delete Account</h1>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this account?
                </div>
                <div class="modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:LinkButton ID="DeleteAccBtn" CssClass="btn btn-danger" runat="server" OnClick="DeleteAccBtn_Click" CausesValidation="false">Delete</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <% 
        if (Request.QueryString["IsProfileUpdated"] == "True")
        {
    %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        Your profile was successfully updated!
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% 
        }
        else if (Request.QueryString["IsProfileUpdated"] == "False")
        {
    %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        Failed to update the profile.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% }
    %>
    <section id="edit-profile" class="bg-white rounded shadow p-5">
        <h1 class="jua text-center">
            <%
                if (Request.QueryString["ChangePin"] == "True")
                { %>
                Change Your Pin
            <% 
                }
                else if (Request.QueryString["ChangePassword"] == "True")
                {
            %>
                Change Your Password
            <% 
                }
                else
                {
            %>
                Edit Your Profile
            <% 
                }
            %>
        </h1>
        <% if (Request.QueryString["ChangePin"] == "True")
            { %>
        <div class="mb-3">
            <label for="pin" class="form-label">PIN:</label>
            <asp:TextBox ID="NewPinTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" CssClass="text-validator" ErrorMessage="Please enter your PIN." ControlToValidate="NewPinTxt" Display="Dynamic" ValidationGroup="ChangePinValidation"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="text-validator" runat="server" ValidationExpression="^\d{6}$" ErrorMessage="PIN must be exactly 6 digits." ControlToValidate="NewPinTxt" Display="Dynamic" ValidationGroup="ChangePinValidation"></asp:RegularExpressionValidator>
        </div>
        <div class="mb-3">
            <label for="repeat-pin" class="form-label">Repeat PIN:</label>
            <asp:TextBox ID="RepeatNewPinTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" CssClass="text-validator" ErrorMessage="Please enter repeat your PIN." ControlToValidate="RepeatNewPinTxt" Display="Dynamic" ValidationGroup="ChangePinValidation"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="RepeatNewPinTxt" CssClass="text-validator" ControlToCompare="NewPinTxt" ErrorMessage="Please match your PIN." ToolTip="PIN must be the same." Display="Dynamic" ValidationGroup="ChangePinValidation"></asp:CompareValidator>
        </div>
        <% }
            else if (Request.QueryString["ChangePassword"] == "True")
            { %>
        <div class="mb-3">
            <label for="password" class="form-label">Password:</label>
            <asp:TextBox ID="PasswordTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password" ValidationGroup="ProfileValidation"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" CssClass="text-validator" ErrorMessage="Please enter your password." ControlToValidate="PasswordTxt" Display="Dynamic" ValidationGroup="ChangePasswordValidation"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3">
            <label for="repeat-password" class="form-label">Repeat Password:</label>
            <asp:TextBox ID="RepeatPasswordTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="text-validator" ErrorMessage="Please enter repeat your password." ControlToValidate="RepeatPasswordTxt" Display="Dynamic" ValidationGroup="ChangePasswordValidation"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="RepeatPasswordTxt" CssClass="text-validator" ControlToCompare="PasswordTxt" ErrorMessage="Please match your password." ToolTip="Password must be the same." Display="Dynamic" ValidationGroup="ChangePasswordValidation"></asp:CompareValidator>
        </div>
        <% }
            else
            { %>
        <div class="mb-3 mt-4">
            <label for="UsernameTxt" class="form-label">Username:</label>
            <asp:TextBox ID="UsernameTxt" runat="server" CssClass="form-control text-bg-light" TextMode="SingleLine"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="text-validator" runat="server"
                ErrorMessage="White space is not allowed." ControlToValidate="UsernameTxt"
                ValidationExpression="^[^\s]+$" Display="Dynamic" ValidationGroup="ChangeProfileValidation"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-validator" runat="server" ErrorMessage="Please enter your username." ControlToValidate="UsernameTxt" Display="Dynamic" ValidationGroup="ChangeProfileValidation"></asp:RequiredFieldValidator>
        </div>

        <div class="mb-3">
            <label for="EmailTxt" class="form-label">Email address:</label>
            <asp:TextBox ID="EmailTxt" runat="server" CssClass="form-control text-bg-light" TextMode="Email"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                ErrorMessage="White space is not allowed." CssClass="text-validator" ControlToValidate="EmailTxt"
                ValidationExpression="^[^\s]+$" Display="Dynamic" ValidationGroup="ChangeProfileValidation"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-validator" runat="server" ErrorMessage="Please enter your email." ControlToValidate="EmailTxt" Display="Dynamic" ValidationGroup="ChangeProfileValidation"></asp:RequiredFieldValidator>
        </div>
        <% } %>

        <% if (Request.QueryString["ChangePin"] == "True")
            { %>
        <asp:Button ID="SubmitPinBtn" CssClass="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SubmitPinBtn_Click" ValidationGroup="ChangePinValidation" />
        <% }
            else if (Request.QueryString["ChangePassword"] == "True")
            { %>
        <asp:Button ID="SubmitPasswordBtn" CssClass="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SubmitPasswordBtn_Click" ValidationGroup="ChangePasswordValidation" />
        <% }
            else
            { %>
        <asp:Button ID="SubmitProfileBtn" CssClass="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SubmitProfileBtn_Click" ValidationGroup="ChangeProfileValidation" />
        <% } %>



        <%if (Request.QueryString["EnterPinPin"] != "True" && Request.QueryString["EnterPasswordPin"] != "True")
            {%>
        <asp:LinkButton ID="ChangePasswordPinBtn" CssClass="text-decoration-underline d-block mt-3 text-center pointer" runat="server" OnClick="ChangePasswordBtn_Click" CausesValidation="false"><small>Want to change your password?</small></asp:LinkButton>
        <asp:LinkButton ID="ChangePinPinBtn" CssClass="text-decoration-underline d-block mt-2 text-center pointer" runat="server" OnClick="ChangePinBtn_Click" CausesValidation="false"><small>Want to change your PIN?</small></asp:LinkButton>
        <a class="text-decoration-underline d-block mt-2 text-center text-danger pointer" data-bs-toggle="modal" data-bs-target="#DeleteAccModal"><small>Want to delete your account?</small></a>
        <% } %>
    </section>
</asp:Content>
