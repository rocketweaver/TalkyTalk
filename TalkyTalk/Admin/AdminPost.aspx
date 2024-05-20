<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminPost.aspx.cs" Inherits="Admin_Page_Web.AdminPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid center-vertically">
        <div class="col-lg-6">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
            <%--    <li class="nav-item" role="presentation">
                    <button class="nav-link" id="display-tab" data-bs-toggle="tab" data-bs-target="#display" type="button" role="tab" aria-controls="profile" aria-selected="true">Display</button>
                </li>--%>

                <%-- 
                <li class="nav-item" role="presentation">
                    <button class="nav-link active" id="input-tab" data-bs-toggle="tab" data-bs-target="#input" type="button" role="tab" aria-controls="profile" aria-selected="false">Input</button>
                </li>
                --%>
            </ul>
            <div class="tab-content">
                
                <div class="tab-pane active" id="input">
                    <asp:Literal ID="response" runat="server"></asp:Literal>

                  <%--  <div class="form-group">
                        <asp:Label ID="LabelTitle" runat="server" Text="Title"></asp:Label>
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Enter post title..."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server"
                            ControlToValidate="txtTitle" ErrorMessage="Title is required." Display="Dynamic"
                            CssClass="text-danger"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="LabelDescription" runat="server" Text="Description"></asp:Label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Enter post description..." OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server"
                            ControlToValidate="txtDescription" ErrorMessage="Description is required." Display="Dynamic"
                            CssClass="text-danger"></asp:RequiredFieldValidator>
                    </div>
                    <asp:Button ID="btnAddPost" runat="server" Text="Add Post" CssClass="btn btn-primary" OnClick="btnAddPost_Click" />
                </div>
                --%>
                <div class="tab-pane" id="display">
                    <asp:Literal ID="lt_table" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
