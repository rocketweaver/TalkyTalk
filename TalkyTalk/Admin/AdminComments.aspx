<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AdminComments.aspx.cs" Inherits="AdminPageTalky.AdminComments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Maincontent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-6">
                <ul class="nav nav-tabs" id="myTab" role="tablist">
                 <%--   <li class="nav-item" role="presentation">
                        <button class="nav-link" id="display-tab" data-bs-toggle="tab" data-bs-target="#display" type="button" role="tab" aria-controls="display" aria-selected="true">Display</button>
                    </li>--%>

                   <%-- 
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="input-tab" data-bs-toggle="tab" data-bs-target="#input" type="button" role="tab" aria-controls="input" aria-selected="false">Input</button>
                    </li>
                    --%>
                </ul>
                <div class="tab-content">
                    
                    <div class="tab-pane active" id="input">
                        <asp:Literal ID="response" runat="server"></asp:Literal>
                        
                    <%--    <div class="form-group">
                            <asp:Label ID="lblCommentText" runat="server" AssociatedControlID="txtCommentText" Text="Comment Text"></asp:Label>
                            <asp:TextBox ID="txtCommentText" runat="server" CssClass="form-control"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server"
     ControlToValidate="txtCommentText" ErrorMessage="Comment is required." Display="Dynamic"
     CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>--%>
                        <%-- 
                        <asp:Button ID="btnUpdate" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSimpan_Click" />
                    </div>
                    --%>
                    <div class="tab-pane" id="display">
                        <asp:Literal ID="lt_table" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
       <%-- </div>
    </div>--%>
</asp:Content>
