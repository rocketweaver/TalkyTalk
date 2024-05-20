<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="editUser.aspx.cs" Inherits="Admin_Page_Web.edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid center-vertically">
        <div class="col-lg-6">
            <div class="tab-content">
                <div class="tab-pane active" id="1">
                    <asp:Literal ID="response" runat="server"></asp:Literal>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="User ID" AssociatedControlID="id_user"></asp:Label>
                        <asp:TextBox ID="id_user" runat="server" class="form-control" placeholder="halo..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Username" AssociatedControlID="txtUsername"></asp:Label>
                        <asp:TextBox ID="txtUsername" runat="server" class="form-control" placeholder="hai..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="hai..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label4" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
                        <asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="hai..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server" Text="Level" AssociatedControlID="level"></asp:Label>
                        <asp:TextBox ID="level" runat="server" class="form-control" placeholder="hai..." OnTextChanged="level_TextChanged"></asp:TextBox>
                    </div>
                    <asp:Button ID="btsimpan" runat="server" Text="Simpan" class="btn btn-primary" OnClick="btsimpan_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
