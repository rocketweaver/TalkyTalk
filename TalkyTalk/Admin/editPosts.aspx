<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="editPosts.aspx.cs" Inherits="AdminTalkyTalky.Admin.editPosts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div>
        <asp:Label ID="lblIdPost" runat="server" Text="Post ID:"></asp:Label>
        <asp:TextBox ID="txtIdPost" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label ID="lblTitle" runat="server" Text="Title:"></asp:Label>
        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label ID="lblDescription" runat="server" Text="Description:"></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" CssClass="btn btn-primary" />
    </div>
    <div>
        <asp:Literal ID="response" runat="server"></asp:Literal>
    </div>
</asp:Content>
