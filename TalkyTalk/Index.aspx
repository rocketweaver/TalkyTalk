<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultUser.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TalkyTalk.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function redirectToPostDetails(containerId) {
            let postId = containerId.replace("PostContainer_", "");
            window.location.href = "UserPost.aspx?id=" + postId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% 
        if (Request.QueryString["IsPostUpdated"] == "True")
        {
    %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        Your post was successfully updated!
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% 
        }
        else if (Request.QueryString["IsPostUpdated"] == "False")
        {
    %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        Failed to update the post.
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% } %>

    <% 
        if (Request.QueryString["IsPostDeleted"] == "True")
        {
    %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        Your post was successfully deleted!
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% 
        }
        else if (Request.QueryString["IsPostDeleted"] == "False")
        {
    %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        Failed to delete the post.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <%  
        }
        if (Request.QueryString["IsPostSubmitted"] == "True")
        {
    %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        Your post was successfully submitted!
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% 
        }
        else if (Request.QueryString["IsPostSubmitted"] == "False")
        {
    %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        Failed to submit the post.
    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% 
        }
        if (Request.QueryString["IsReportSubmitted"] == "True")
        {
    %>
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        Your report was successfully submitted!
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <%  }
        else if (Request.QueryString["IsReportSubmitted"] == "False")
        {
    %>
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        Failed to submit the report.
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
    <% } %>
    <a href="PostForm.aspx" class="text-decoration-none add-btn">
        <img src="App_Themes/MainTheme/asset/img/icon/add-icon.svg" alt="" /></a>
    <div class="d-flex" role="search" id="search-engine">
        <asp:TextBox ID="SearchTxt" CssClass="form-control me-2 bg-light" runat="server" placeholder="Search"
            aria-label="Search" TextMode="Search"></asp:TextBox>
        <asp:ImageButton ID="SearchBtn" CssClass="btn btn-primary py-2 px-4" runat="server" ImageUrl="~/App_Themes/MainTheme/asset/img/icon/search-icon.svg" OnClick="SearchBtn_Click" />
    </div>

    <div class="d-flex mt-4">
        <asp:LinkButton ID="MostLikedBtn" CssClass="btn btn-primary" runat="server" OnClick="MostLikedBtn_Click">
            <span>Most Liked</span>
            <img
                src="App_Themes/MainTheme/asset/img/icon/like-white-icon.svg"
                class="ms-1 mb-1"
                alt=""
                style="width: 16px" />
        </asp:LinkButton>
        <asp:LinkButton ID="MostSharedBtn" CssClass="ms-3 btn btn-success" runat="server" OnClick="MostSharedBtn_Click">
            <span>Most Shared</span>
            <img
                src="App_Themes/MainTheme/asset/img/icon/share-white-icon.svg"
                class="ms-1 mb-1"
                alt=""
                style="width: 16px; margin-bottom: 3px" />
        </asp:LinkButton>
        <asp:LinkButton ID="MostCommentedBtn" CssClass="ms-3 btn btn-dark" runat="server" OnClick="MostCommentedBtn_Click">
            <span>Most Commented</span>
            <img
                src="App_Themes/MainTheme/asset/img/icon/comment-white-icon.svg"
                class="ms-1 mb-1"
                alt=""
                style="width: 16px" />
        </asp:LinkButton>
    </div>

    <asp:Repeater ID="PostsRepeater" runat="server">
        <ItemTemplate>
            <div class="post-container w-100 border border-secondary-subtle p-5 rounded bg-white mt-5 pointer" id='<%# "PostContainer_" + Eval("id_post") %>' onclick="redirectToPostDetails(this.id)">
                <div class="d-flex justify-content-between">
                    <asp:Label ID="TitleLabel" CssClass="fw-bold fs-3" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                    <asp:Label ID="DateLabel" CssClass="text-secondary fs-6" runat="server" Text='<%# Convert.ToDateTime(Eval("post_date")).ToString("yyyy-MM-dd") %>'></asp:Label>
                </div>
                <div>
                </div>
                <br />
                <asp:Label ID="DescLabel" CssClass="post-desc" runat="server" Text='<%# Eval("description") %>'></asp:Label>
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
                        <asp:Label ID="Label2" CssClass="mt-1" runat="server" Text='<%# Eval("comment_count") %>'></asp:Label>
                        <img src="App_Themes/MainTheme/asset/img/icon/comment-black-icon.svg" class="ms-1" alt="like icon" style="width: 16px; margin-top: 5px" />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <a href="PostForm.aspx" class="text-decoration-none add-btn">
        <img src="asset/img/icon/add-icon.svg" alt="" /></a>
</asp:Content>
