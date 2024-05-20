using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalkyTalk.Models.Default;

namespace TalkyTalk
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/SignForm.aspx");
            }
            else
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            PostsModel posts = new PostsModel();

            DataSet ds;

            if (Request.QueryString["MostLiked"] == "True")
            {
                ds = posts.OrderByLike();
            }
            else if (Request.QueryString["MostShared"] == "True")
            {
                ds = posts.OrderByShare();
            }
            else if (Request.QueryString["MostCommented"] == "True")
            {
                ds = posts.OrderByCommented();
            }
            else
            {
                ds = posts.Read();
            }

            PostsRepeater.DataSource = ds.Tables["posts"];
            PostsRepeater.DataBind();
        }

        protected void SearchBtn_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(SearchTxt.Text))
            {
                PostsModel posts = new PostsModel();
                DataSet ds = posts.Search(SearchTxt.Text);

                PostsRepeater.DataSource = ds.Tables["posts"];
                PostsRepeater.DataBind();
            }
        }

        protected void MostLikedBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx?MostLiked=True");
        }

        protected void MostSharedBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx?MostShared=True");
        }

        protected void MostCommentedBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx?MostCommented=True");
        }
    }
}