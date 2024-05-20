using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalkyTalk.Models.Default;
using TalkyTalk.Models.DefaultUsers;

namespace TalkyTalk
{
    public partial class PostForm : System.Web.UI.Page
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
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        LoadPostData();
                    }
                }
            }
        }

        protected void LoadPostData()
        {
            PostsModel post = new PostsModel();
            post.id = Request.QueryString["id"];
            DataRow dr = post.ReadById();

            if (dr != null)
            {
                TitleTxt.Text = dr["title"].ToString();
                DescTxt.Text = dr["description"].ToString();
                IdPostTxt.Value = dr["id_post"].ToString();
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string userId = ticket.UserData.Split('|')[0];

            PostsModel post = new PostsModel();
            post.title = TitleTxt.Text;
            post.desc = DescTxt.Text;
            post.id = IdPostTxt.Value;
            post.userId = userId;

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Response.Redirect("Index.aspx?IsPostUpdated=" + post.Update());
            }
            else
            {
                Response.Redirect("Index.aspx?IsPostSubmitted=" + post.Create());
            }
        }

    }
}