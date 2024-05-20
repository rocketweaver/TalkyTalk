<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminReport.aspx.cs" Inherits="Admin_Page_Web.AdminReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <!-- Letakkan referensi gaya CSS atau tag style di sini jika diperlukan -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="tab-pane" id="2">
        <div class="tab-pane fade show active" id="display" role="tabpanel" aria-labelledby="display-tab" style="overflow-x:scroll">
            <asp:Literal ID="lt_table" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
