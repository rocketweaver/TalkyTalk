<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminBan.aspx.cs" Inherits="Admin_Page_Web.AdminBan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid center-vertically">
        <div class="col-lg-6">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="display-tab" data-bs-toggle="tab" data-bs-target="#display" type="button" role="tab" aria-controls="profile" aria-selected="false">Display</button>
                </li>

                <li class="nav-item" role="presentation">
                    <button class="nav-link" id="input-tab" data-bs-toggle="tab" data-bs-target="#input" type="button" role="tab" aria-controls="profile" aria-selected="true">Input</button>
                </li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane" id="input">
                    <asp:Literal ID="response" runat="server"></asp:Literal>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="User ID"></asp:Label>
                        <asp:TextBox ID="txtUserID" runat="server" class="form-control" placeholder="User ID..."></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control" placeholder="Start Date..." TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control" placeholder="End Date..." TextMode="Date"></asp:TextBox>
                    </div>
                    <asp:Button ID="btsimpan" runat="server" Text="Save" class="btn btn-primary" OnClick="btsimpan_Click" />
                </div>
                <div class="tab-pane active" id="display">
                    <asp:Literal ID="lt_table" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
