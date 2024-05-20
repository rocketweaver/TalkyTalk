<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminUser.aspx.cs" Inherits="Admin_Page_Web.AdminUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid center-vertically">
        <div class="col-lg-6">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="display-tab" data-bs-toggle="tab" data-bs-target="#display" type="button" role="tab" aria-controls="profile" aria-selected="true">Display</button>
                </li>

                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="input-tab" data-bs-toggle="tab" data-bs-target="#input" type="button" role="tab" aria-controls="profile" aria-selected="false">Input</button>
                </li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="input">
                    <asp:Literal ID="response" runat="server"></asp:Literal>

                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Username" AssociatedControlID="txtUsername"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter email" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Enter password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Level" AssociatedControlID="level"></asp:Label>
                        <asp:TextBox ID="level" runat="server" CssClass="form-control" placeholder="Enter user level" ReadOnly="true">1</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="level" ErrorMessage="Level is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>

                    <asp:Button ID="btsimpan" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btsimpan_Click" />
                </div>
              
                <div class="tab-pane" id="display">
                    <asp:Literal ID="lt_table" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
