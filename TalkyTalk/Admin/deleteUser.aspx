<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="deleteUser.aspx.cs" Inherits="Admin_Page_Web.deleteUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Maincontent" runat="server">
    <asp:Literal ID="litScript" runat="server"></asp:Literal>

    <asp:Button ID="btnDelete" runat="server" Text="Delete User" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" Style="margin-left: 10px;" />
</asp:Content>
