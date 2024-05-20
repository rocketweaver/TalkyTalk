<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultUser.Master" AutoEventWireup="true" CodeBehind="PostForm.aspx.cs" Inherits="TalkyTalk.PostForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="edit-profile" class="bg-white rounded shadow p-5">
        <h1 class="jua text-center">Write Your Post Here!</h1>
        <div class="mb-3 mt-4">
            <%
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
            %>
            <asp:HiddenField ID="IdPostTxt" runat="server" />
            <%
                } 
            %>
            <label for="TitleTxt" class="form-label">Title:</label>
            <asp:TextBox ID="TitleTxt" CssClass="form-control text-bg-light" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-validator" runat="server" ErrorMessage="Please enter the title." ControlToValidate="TitleTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="mb-3">
            <asp:TextBox class="form-control text-bg-light resize-none" ID="DescTxt" runat="server" TextMode="MultiLine" Placeholder="Type here..."></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-validator" runat="server" ErrorMessage="Please write down your post." ControlToValidate="DescTxt" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <asp:Button ID="SubmitBtn" CssClass="btn btn-primary w-100 mt-2" runat="server" Text="Submit" OnClick="SubmitBtn_Click" CausesValidation="false" />
    </section>
</asp:Content>
