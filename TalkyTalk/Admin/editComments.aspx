<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="editComments.aspx.cs" Inherits="Admin_Page_Web.editComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <style>
        .form-group {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="form-group">
        <asp:Label ID="lblCommentId" runat="server" Text="Comment ID:" />
        <asp:TextBox ID="txtIdComment" runat="server" ReadOnly="true" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Label ID="lblCommentText" runat="server" Text="Comment Text:" />
        <asp:TextBox ID="txtCommentText" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-primary" />
    </div>
    <div>
        <asp:Literal ID="response" runat="server"></asp:Literal>
    </div>
</asp:Content>
