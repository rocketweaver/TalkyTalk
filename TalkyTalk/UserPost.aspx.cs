using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TalkyTalk.Models.Default;

namespace TalkyTalk
{
    public partial class UserPost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/SignForm.aspx");
                }
                else
                {
                    LoadPostDetail();
                }
            }

            LoadPostsComments();
        }

        protected void LoadPostDetail()
        {
            PostsModel post = new PostsModel();
            post.id = Request.QueryString["id"];
            DataRow dr = post.ReadById();

            if (dr != null)
            {
                TitleLabel.Text = dr["title"].ToString();
                string postDate = Convert.ToDateTime(dr["post_date"]).ToString("yyyy-MM-dd");
                if (!DBNull.Value.Equals(dr["edited"]) && Convert.ToInt32(dr["edited"]) != 0)
                {
                    postDate += " (Edited)";
                }
                DateLabel.Text = postDate;
                PostAuthorBtn.CommandArgument = dr["user_id"].ToString();
                PostAuthorBtn.Text = dr["username"].ToString();
                DescLabel.Text = dr["description"].ToString();

                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string userId = userData[0];

                post.postId = Request.QueryString["id"];
                post.userId = userId;

                int likeCount = post.GetLikeCountByPostId();
                int shareCount = post.GetShareCountByPostId();

                if (post.HasLiked())
                {
                    LikeBtn.Text = "(" + likeCount + ") Unlike";
                }
                else
                {
                    LikeBtn.Text = "(" + likeCount + ") Like";
                }

                if (post.HasShared())
                {
                    ShareBtn.Text = "(" + shareCount + ") Unshare";
                }
                else
                {
                    ShareBtn.Text = "(" + shareCount + ") Share";
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void LoadPostsComments()
        {
            CommentsModel comments = new CommentsModel();
            comments.postId = Request.QueryString["id"];
            DataSet ds = comments.ReadByPostId();

            CommentsRepeater.DataSource = ds.Tables["comments"];
            CommentsRepeater.DataBind();
        }

        protected void CommentBtn_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;

            string[] userData = ticket.UserData.Split('|');

            string userId = userData[0];

            CommentsModel comment = new CommentsModel();

            comment.postId = Request.QueryString["id"];
            comment.userId = userId;
            comment.description = CommentTxt.Text;

            comment.Create();

            CommentTxt.Text = "";

            Response.Redirect("UserPost.aspx?id=" + Request.QueryString["id"]);
        }

        protected void CommentAuthorBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string userId = btn.CommandArgument;

            Response.Redirect("UserProfile.aspx?userId=" + userId);
        }

        protected void PostAuthorBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string userId = btn.CommandArgument;

            Response.Redirect("UserProfile.aspx?userId=" + userId);
        }

        protected void LikeBtn_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;

            string[] userData = ticket.UserData.Split('|');

            string userId = userData[0];

            PostsModel post = new PostsModel();

            post.postId = Request.QueryString["id"];
            post.userId = userId;

            if (!post.HasLiked())
            {
                post.Like();
            }
            else
            {
                post.Unlike();
            }

            Response.Redirect("UserPost.aspx?id=" + Request.QueryString["id"]);
        }

        protected void ShareBtn_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;

            string[] userData = ticket.UserData.Split('|');

            string userId = userData[0];

            PostsModel post = new PostsModel();

            post.postId = Request.QueryString["id"];
            post.userId = userId;

            if (!post.HasShared())
            {
                post.Share();
            }
            else
            {
                post.Unshare();
            }

            Response.Redirect("UserPost.aspx?id=" + Request.QueryString["id"]);
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PostForm.aspx?id=" + Request.QueryString["id"]);
        }

        protected void DeletePostBtn_Click(object sender, EventArgs e)
        {
            PostsModel post = new PostsModel();
            post.id = Request.QueryString["id"];

            Response.Redirect("Index.aspx?IsPostDeleted=" + post.Delete());
        }

        protected Tuple<string, string> CheckCurrentUserAndCommentUser(object commentUserId)
        {
            string currentUserId = GetCurrentUserId();

            if (currentUserId == commentUserId.ToString())
            {
                return Tuple.Create("EditCommenttBtn", "DeleteCommentBtn");
            }
            else
            {
                return null;
            }
        }

        protected void CommentsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Panel commentActionPanel = e.Item.FindControl("CommentActionPanel") as Panel;

                if (commentActionPanel != null)
                {
                    DataRowView rowView = e.Item.DataItem as DataRowView;
                    if (rowView != null)
                    {
                        string commentUserId = rowView["user_id"].ToString();
                        string currentUserId = GetCurrentUserId();

                        if (currentUserId == commentUserId)
                        {
                            HtmlGenericControl editButton = new HtmlGenericControl("img");
                            editButton.Attributes["class"] = "me-2 pointer edit-comment-btn";
                            editButton.Attributes["edit-comment-id"] = rowView["id_comment"].ToString();
                            editButton.Attributes["edit-comment-desc"] = (e.Item.FindControl("CommentDescLabel") as Label)?.Text;
                            editButton.Attributes["data-bs-toggle"] = "modal";
                            editButton.Attributes["data-bs-target"] = "#EditCommentModal";
                            editButton.Attributes["src"] = "App_Themes/MainTheme/asset/img/icon/edit-icon.svg";
                            editButton.Attributes["alt"] = "";
                            editButton.Attributes["style"] = "width: 16px";
                            commentActionPanel.Controls.Add(editButton);

                            HtmlGenericControl deleteButton = new HtmlGenericControl("img");
                            deleteButton.Attributes["class"] = "me-2 pointer delete-comment-btn";
                            deleteButton.Attributes["delete-comment-id"] = rowView["id_comment"].ToString();
                            deleteButton.Attributes["data-bs-toggle"] = "modal";
                            deleteButton.Attributes["data-bs-target"] = "#DeleteCommentModal";
                            deleteButton.Attributes["src"] = "App_Themes/MainTheme/asset/img/icon/delete-icon.svg";
                            deleteButton.Attributes["alt"] = "";
                            deleteButton.Attributes["style"] = "width: 16px";
                            commentActionPanel.Controls.Add(deleteButton);
                        }
                        else
                        {
                            HtmlGenericControl reportButton = new HtmlGenericControl("img");
                            reportButton.Attributes["src"] = "App_Themes/MainTheme/asset/img/icon/report-icon.svg";
                            reportButton.Attributes["alt"] = "";
                            reportButton.Attributes["id"] = "report-comment-btn";
                            reportButton.Attributes["class"] = "pointer report-comment-btn";
                            reportButton.Attributes["report-comment-id"] = rowView["id_comment"].ToString();
                            reportButton.Attributes["style"] = "width: 16px";
                            reportButton.Attributes["data-bs-toggle"] = "modal";
                            reportButton.Attributes["data-bs-target"] = "#ReportModal";
                            reportButton.Attributes["onclick"] = "handleReportCommentButton()";
                            commentActionPanel.Controls.Add(reportButton);
                        }
                    }
                }
            }
        }


        protected void DeleteCommentBtn_Click(object sender, EventArgs e)
        {
            CommentsModel comment = new CommentsModel();
            comment.id = DeleteIdCommentHidden.Value;

            comment.Delete();

            Response.Redirect("UserPost.aspx?id=" + Request.QueryString["id"]);
        }

        protected void EditCommentBtn_Click(object sender, EventArgs e)
        {
            CommentsModel comment = new CommentsModel();
            comment.id = EditIdCommentHidden.Value;
            comment.description = EditCommentDescTxt.Text;

            comment.Update();

            Response.Redirect("UserPost.aspx?id=" + Request.QueryString["id"]);
        }

        protected string GetCurrentUserId()
        {
            FormsIdentity identity = (FormsIdentity)User.Identity;
            FormsAuthenticationTicket ticket = identity.Ticket;

            string userData = ticket.UserData;
            string[] userDataParts = userData.Split('|');
            string userId = userDataParts[0];

            return userId;
        }

        protected void ReportBtn_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)this.Page.User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            string userId = userData[0];

            ReportsModel report = new ReportsModel();

            string postId = ReportPostIdHidden.Value;
            string commentId = ReportCommentIdHidden.Value;

            report.reporterId = Convert.ToInt32(userId);

            if (string.IsNullOrEmpty(postId))
            {
                report.commentId = commentId;
            }
            else
            {
                report.postId = postId;
            }

            report.desc = ReportDescTxt.Text;

            Response.Redirect("Index.aspx?IsReportSubmitted=" + report.Create());
        }


    }
}