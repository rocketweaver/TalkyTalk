<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultUser.Master" AutoEventWireup="true" CodeBehind="UserPost.aspx.cs" Inherits="TalkyTalk.UserPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            let reportPostButton = document.getElementById('report-post-btn');
            if (reportPostButton) {
                reportPostButton.addEventListener('click', function () {
                    let postIdHidden = document.getElementById('<%= ReportPostIdHidden.ClientID %>');
                    postIdHidden.value = '<%= Request.QueryString["id"] %>';

                    let commentIdHidden = document.getElementById('<%= ReportCommentIdHidden.ClientID %>');
                    commentIdHidden.value = '';
                });
            }

            let reportCommentButtons = document.querySelectorAll('.report-comment-btn');
            if (reportCommentButtons.length > 0) {
                reportCommentButtons.forEach(function (button) {
                    button.addEventListener('click', function () {
                        let commentId = this.getAttribute('report-comment-id');
                        let commentIdHidden = document.getElementById('<%= ReportCommentIdHidden.ClientID %>');
                        commentIdHidden.value = commentId;

                        let postIdHidden = document.getElementById('<%= ReportPostIdHidden.ClientID %>');
                        postIdHidden.value = '';
                    });
                });
            }

            let editCommentButtons = document.querySelectorAll('.edit-comment-btn');
            if (editCommentButtons.length > 0) {
                editCommentButtons.forEach(function (button) {
                    button.addEventListener('click', function () {
                        let commentId = this.getAttribute('edit-comment-id');
                        let commentDesc = this.getAttribute('edit-comment-desc');

                        let idCommentHidden = document.getElementById('<%= EditIdCommentHidden.ClientID %>');
                        let editCommentDescTxt = document.getElementById('<%= EditCommentDescTxt.ClientID %>');
                        idCommentHidden.value = commentId;
                        editCommentDescTxt.value = commentDesc;
                    });
                });
            }

            let deleteCommentButtons = document.querySelectorAll('.delete-comment-btn');
            if (deleteCommentButtons.length > 0) {
                deleteCommentButtons.forEach(function (button) {
                    button.addEventListener('click', function () {
                        let commentId = this.getAttribute('delete-comment-id');

                        let idCommentHidden = document.getElementById('<%= DeleteIdCommentHidden.ClientID %>');
                        idCommentHidden.value = commentId;
                    });
                });
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Report modals -->
    <div
        class="modal fade"
        id="ReportModal"
        tabindex="-1"
        aria-labelledby="ReportLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="ReportLabel">Report</h1>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:TextBox ID="ReportDescTxt" CssClass="form-control text-bg-light" runat="server" TextMode="MultiLine" placeholder="Type report here..."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ReportDescTxt" runat="server" ErrorMessage="Type something before submit the report." Display="Dynamic" ValidationGroup="Report"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="ReportPostIdHidden" runat="server" />
                        <asp:HiddenField ID="ReportCommentIdHidden" runat="server" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:Button ID="ReportBtn" CssClass="btn btn-danger" runat="server" Text="Report" OnClick="ReportBtn_Click"  ValidationGroup="Report"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Edit comment modal -->
    <div
        class="modal fade"
        id="EditCommentModal"
        tabindex="-1"
        aria-labelledby="EditCommentLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="EditCommentLabel">Rewrite Your Comment</h1>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:HiddenField ID="EditIdCommentHidden" runat="server" />
                        <asp:TextBox ID="EditCommentDescTxt" CssClass="form-control text-bg-light" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="EditCommentDescTxt" ValidationGroup="EditComment"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:Button ID="EditCommentBtn" CssClass="btn btn-primary text-white" runat="server" Text="Submit" OnClick="EditCommentBtn_Click" ValidationGroup="EditComment"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete post modal -->
    <div
        class="modal fade"
        id="DeletePostModal"
        tabindex="-1"
        aria-labelledby="DeletePosttLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="DeletePostLabel">Delete Post</h1>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this post?
                </div>
                <div class="modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:LinkButton ID="DeletePostBtn" CssClass="btn btn-danger" runat="server" OnClick="DeletePostBtn_Click" CausesValidation="false">Delete</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete comment modal -->
    <div
        class="modal fade"
        id="DeleteCommentModal"
        tabindex="-1"
        aria-labelledby="DeleteCommenttLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="DeleteCommentLabel">Delete Comment</h1>
                </div>
                <div class="modal-body">
                    Are you sure you want to delete this comment?
                </div>
                <div class="modal-footer">
                    <asp:HiddenField ID="DeleteIdCommentHidden" runat="server" />
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:LinkButton ID="DeleteCommentBtn" CssClass="btn btn-danger" runat="server" OnClick="DeleteCommentBtn_Click" CausesValidation="false">Delete</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <div
        class="user-post-container w-100 bg-white border border-secondary-subtle shadow py-5 rounded">
        <div class="container px-5">
            <div class="d-flex justify-content-between">
                <asp:Label ID="TitleLabel" CssClass="fw-bold fs-2" runat="server"></asp:Label>
                <asp:Label ID="PostDetailIdLabel" runat="server"></asp:Label>
                <asp:Label ID="DateLabel" CssClass="text-secondary text-sm" runat="server" Text="Label"></asp:Label>
            </div>
            <asp:Label ID="PostAuthorLabel" CssClass="text-secondary text-sm" runat="server" Text="by "></asp:Label>
            <asp:LinkButton ID="PostAuthorBtn" CssClass="text-sm" runat="server" CommandArgument='<%# Eval("user_id") %>' OnClick="PostAuthorBtn_Click" CausesValidation="false"><%# Eval("username") %></asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="DescLabel" CssClass="user-post-desc mt-4" runat="server" Text="Label"></asp:Label>
        </div>
        <div
            class="post-action container px-5 py-3 mt-5 bg-secondary-subtle d-flex justify-content-around">
            <div class="d-flex">
                <asp:LinkButton ID="LikeBtn" CssClass="text-decoration-none text-primary" runat="server" OnClick="LikeBtn_Click" CausesValidation="false">Like</asp:LinkButton>
                <img
                    src="App_Themes/MainTheme/asset/img/icon/like-icon.svg"
                    alt=""
                    class="ms-2 mb-1"
                    style="width: 20px" />
            </div>

            <% 
                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string usernameFromTicket = userData[1];

                if (PostAuthorBtn.Text != usernameFromTicket)
                {
            %>
            <div class="d-flex">
                <asp:LinkButton ID="ShareBtn" CssClass="text-decoration-none text-success" runat="server" OnClick="ShareBtn_Click" CausesValidation="false">Share</asp:LinkButton>
                <img src="App_Themes/MainTheme/asset/img/icon/share-icon.svg" alt="" class="ms-2" style="width: 20px" />
            </div>
            <div class="d-flex">
                <button type="button" id="report-post-btn" class="text-decoration-none text-danger border-0 bg-none" data-bs-toggle="modal" data-bs-target="#ReportModal">Report</button>
                <img src="App_Themes/MainTheme/asset/img/icon/report-icon.svg" alt="" class="ms-2" style="width: 20px" />
            </div>
            <% }
                else
                {
            %>
            <div class="d-flex">
                <asp:LinkButton ID="EditBtn" CssClass="text-decoration-none text-warning" runat="server" CausesValidation="false" OnClick="EditBtn_Click">Edit</asp:LinkButton>
                <img src="App_Themes/MainTheme/asset/img/icon/edit-icon.svg" alt="" class="ms-2" style="width: 20px" />
            </div>
            <div class="d-flex">
                <button type="button" id="delete-post-btn" class="text-decoration-none text-danger border-0 bg-none" data-bs-toggle="modal" data-bs-target="#DeletePostModal">Delete</button>
                <img src="App_Themes/MainTheme/asset/img/icon/delete-icon.svg" alt="" class="ms-2" style="width: 20px" />
            </div>
            <% } %>
        </div>
        <div class="container p-5">
            <div class="d-flex">
                <asp:TextBox class="form-control me-2 bg-light resize-none" ID="CommentTxt" runat="server" TextMode="MultiLine"></asp:TextBox>
                <asp:Button ID="CommentBtn" CssClass="btn btn-primary py-2 px-4" runat="server" Text="Comment" OnClick="CommentBtn_Click" />
            </div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-validator" runat="server" ErrorMessage="You can't leave an empty comment." ControlToValidate="CommentTxt" Display="Dynamic" Va></asp:RequiredFieldValidator>
            <br />
            <asp:Repeater ID="CommentsRepeater" runat="server" OnItemDataBound="CommentsRepeater_ItemDataBound">
                <ItemTemplate>
                    <div
                        class="comment-container border border-secondary-subtle p-5 rounded bg-white mt-5">
                        <div class="d-flex justify-content-between">
                            <asp:LinkButton ID="CommentAuthorBtn" CssClass="fw-semibold fs-6 text-decoration-none" runat="server" CommandArgument='<%# Eval("user_id") %>' OnClick="CommentAuthorBtn_Click" CausesValidation="false"><%# Eval("username") %></asp:LinkButton>
                            <asp:Label ID="CommentDateLabel" CssClass="text-secondary text-sm ms-4" runat="server" Text='<%# Convert.ToDateTime(Eval("comment_date")).ToString("yyyy-MM-dd") + ((Convert.ToInt32(Eval("edited")) == 1) ? " (Edited)" : "") %>'></asp:Label>
                        </div>
                        <p class="comment-desc mt-3">
                            <asp:Label ID="CommentDescLabel" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                        </p>
                        <asp:Panel ID="CommentActionPanel" CssClass="d-flex justify-content-end mt-3" runat="server">
                        </asp:Panel>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
