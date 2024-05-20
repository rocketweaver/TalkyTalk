<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultUser.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="TalkyTalk.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function redirectToPostDetails(containerId) {
            var postId = containerId.replace("PostContainer_", "");
            window.location.href = "UserPost.aspx?id=" + postId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div
        class="modal fade"
        id="CheckPinModal"
        tabindex="-1"
        aria-labelledby="CheckPinLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 jua" id="CheckPintLabel">Enter Your PIN</h1>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:TextBox ID="PinTxt" CssClass="form-control text-bg-light" runat="server" TextMode="Password" placeholder="Type your pin here..."></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-validator" runat="server" ErrorMessage="Please type your pin." ControlToValidate="PinTxt" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="text-validator" runat="server" ErrorMessage="You can only use number." ValidationExpression="\d+" ControlToValidate="PinTxt" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        data-bs-dismiss="modal">
                        Close
                    </button>
                    <asp:Button ID="CheckPinBtn" CssClass="btn btn-primary" runat="server" Text="Submit" OnClick="CheckPinBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <div
        class="user-profile-container w-100 bg-white border border-secondary-subtle shadow py-5 rounded">
        <div class="container px-5">
            <div
                class="d-flex justify-content-center align-items-center flex-column">
                <div class="profile-pict-lg rounded"></div>
                <asp:Panel ID="ProfileUsernamePanel" CssClass="profile-username mt-3" runat="server"></asp:Panel>
            </div>
        </div>
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link text-black active"
                    id="personal-post"
                    data-bs-toggle="tab"
                    data-bs-target="#personal-post-tab-pane"
                    type="button"
                    role="tab"
                    aria-controls="personal-post-tab-pane"
                    aria-selected="true">
                    Personal Post
                </button>
            </li>
            <li class="nav-item" role="presentation">
                <button
                    class="nav-link text-black"
                    id="shared-post-tab"
                    data-bs-toggle="tab"
                    data-bs-target="#shared-post-tab-pane"
                    type="button"
                    role="tab"
                    aria-controls="shared-post-tab-pane"
                    aria-selected="false">
                    Shared Post
                </button>
            </li>
        </ul>
        <div class="tab-content" id="myTabContent">
            <div
                class="tab-pane fade show active"
                id="personal-post-tab-pane"
                role="tabpanel"
                aria-labelledby="personal-post-tab"
                tabindex="0">
                <div class="container p-5">
                    <asp:Repeater ID="PersonalPostsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="post-container w-100 border border-secondary-subtle p-5 rounded bg-white mt-5 pointer" id='<%# "PostContainer_" + Eval("id_post") %>' onclick="redirectToPostDetails(this.id)">
                                <div class="d-flex justify-content-between">
                                    <asp:Label ID="PP_TitleLabel" CssClass="fw-bold fs-3" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    <asp:Label ID="PP_DateLabel" CssClass="text-secondary fs-6" runat="server" Text='<%# Convert.ToDateTime(Eval("post_date")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="PP_DescLabel" CssClass="post-desc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                <div class="d-flex mt-4">
                                    <div class="d-flex">
                                        <asp:Label ID="LikeCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("like_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/like-black-icon.svg" class="ms-1" alt="Like icon" style="width: 16px" />
                                    </div>
                                    <div class="d-flex ms-3">
                                        <asp:Label ID="ShareCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("share_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/share-black-icon.svg" class="ms-1" alt="Share icon" style="width: 16px; margin-top: 3px" />
                                    </div>
                                    <div class="d-flex ms-3">
                                        <asp:Label ID="CommentCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("comment_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/comment-black-icon.svg" class="ms-1" alt="like icon" style="width: 16px; margin-top: 5px" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div
                class="tab-pane fade"
                id="shared-post-tab-pane"
                role="tabpanel"
                aria-labelledby="shared-post-tab"
                tabindex="0">
                <div class="container p-5">
                    <asp:Repeater ID="SharedPostsRepeater" runat="server">
                        <ItemTemplate>
                            <div class="post-container w-100 border border-secondary-subtle p-5 rounded bg-white mt-5 pointer" id='<%# "PostContainer_" + Eval("id_post") %>' onclick="redirectToPostDetails(this.id)">
                                <div class="d-flex justify-content-between">
                                    <asp:Label ID="SP_TitleLabel" CssClass="fw-bold fs-3" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                    <asp:Label ID="SP_DateLabel" CssClass="text-secondary fs-6" runat="server" Text='<%# Convert.ToDateTime(Eval("post_date")).ToString("yyyy-MM-dd") %>'></asp:Label>
                                </div>
                                <br />
                                <asp:Label ID="SP_DescLabel" CssClass="post-desc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                <div class="d-flex mt-4">
                                    <div class="d-flex">
                                        <asp:Label ID="LikeCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("like_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/like-black-icon.svg" class="ms-1" alt="Like icon" style="width: 16px" />
                                    </div>
                                    <div class="d-flex ms-3">
                                        <asp:Label ID="ShareCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("share_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/share-black-icon.svg" class="ms-1" alt="Share icon" style="width: 16px; margin-top: 3px" />
                                    </div>
                                    <div class="d-flex ms-3">
                                        <asp:Label ID="CommentCountLabel" CssClass="mt-1" runat="server" Text='<%# Eval("comment_count") %>'></asp:Label>
                                        <img src="App_Themes/MainTheme/asset/img/icon/comment-black-icon.svg" class="ms-1" alt="like icon" style="width: 16px; margin-top: 5px" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
