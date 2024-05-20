<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="editBans.aspx.cs" Inherits="AdminPageTalky.Admin.editBans" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
     <style>
     .form-group {
         margin-bottom: 20px;
     }
 </style>
</asp:Content>


   


<asp:Content ID="Content3" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="form-group">
        <asp:Label ID="lblBanId" runat="server" Text="Ban ID:" />
        <asp:TextBox ID="txtBanId" runat="server" ReadOnly="true" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" />
    </div>
    <div class="form-group">
        <asp:Label ID="lblEndDate" runat="server" Text="End Date:" />
        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" OnTextChanged="txtEndDate_TextChanged" />
    </div>
    <div class="form-group">
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
    </div>
    <div>
        <asp:Literal ID="response" runat="server"></asp:Literal>
    </div>

</asp:Content>
