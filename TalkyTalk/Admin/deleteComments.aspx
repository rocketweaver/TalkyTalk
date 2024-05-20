<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="deleteComments.aspx.cs" Inherits="Admin_Page_Web.deleteComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid center-vertically">
        <div class="col-lg-6">
            <asp:Literal ID="lt_message" runat="server"></asp:Literal>
        </div>
        <asp:Button ID="btnDelete" runat="server" Text="Delete Comments" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" Style="margin-left: 10px;" />
    </div>
</asp:Content>
