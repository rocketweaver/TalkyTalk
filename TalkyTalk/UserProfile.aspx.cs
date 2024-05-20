using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TalkyTalk.Models.Default;
using TalkyTalk.Models.DefaultUsers;

namespace TalkyTalk
{
    public partial class UserProfile : System.Web.UI.Page
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
                    PinTxt.Attributes.Add("maxlength", "6");
                    LoadUserData();
                    LoadPersonalPostData();
                    LoadSharedPostData();
                }
            }
        }

        protected void LoadUserData()
        {
            UsersModel user = new UsersModel();
            user.id = Convert.ToInt32(Request.QueryString["userId"]);

            DataRow dr = user.ReadById();

            if (dr != null)
            {
                string userIdTicket = ((FormsIdentity)User.Identity).Ticket.UserData.Split('|')[0];
                if (userIdTicket.Equals(Request.QueryString["userId"]))
                {
                    HtmlGenericControl reportButton = new HtmlGenericControl("a");
                    reportButton.Attributes["id"] = "check-pin-btn";
                    reportButton.Attributes["class"] = "pointer fw-bold text-decoration-none text-black";
                    reportButton.Attributes["data-bs-toggle"] = "modal";
                    reportButton.Attributes["data-bs-target"] = "#CheckPinModal";
                    reportButton.InnerHtml = dr["username"].ToString();
                    ProfileUsernamePanel.Controls.Add(reportButton);

                    HtmlGenericControl editProfileImg = new HtmlGenericControl("img");
                    editProfileImg.Attributes["class"] = "ms-1 pointer";
                    editProfileImg.Attributes["style"] = "width: 16px";
                    editProfileImg.Attributes["src"] = "App_Themes/MainTheme/asset/img/icon/edit-black-icon.svg";
                    editProfileImg.Attributes["alt"] = "";
                    reportButton.Attributes["data-bs-toggle"] = "modal";
                    reportButton.Attributes["data-bs-target"] = "#CheckPinModal";
                    ProfileUsernamePanel.Controls.Add(editProfileImg);
                }
                else
                {
                    HtmlGenericControl profileUsername = new HtmlGenericControl("p");
                    profileUsername.Attributes["id"] = "username-profile";
                    profileUsername.Attributes["class"] = "fw-bold";
                    profileUsername.InnerHtml = dr["username"].ToString();

                    ProfileUsernamePanel.Controls.Add(profileUsername);
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }


        protected void LoadPersonalPostData()
        {
            PostsModel posts = new PostsModel();
            posts.userId = Request.QueryString["userId"];
            DataSet ds = posts.ReadByUserId();

            PersonalPostsRepeater.DataSource = ds.Tables["personal_posts"];
            PersonalPostsRepeater.DataBind();
        }

        protected void LoadSharedPostData()
        {
            PostsModel posts = new PostsModel();
            posts.userId = Request.QueryString["userId"];
            DataSet ds = posts.ReadByShare();

            SharedPostsRepeater.DataSource = ds.Tables["shared_posts"];
            SharedPostsRepeater.DataBind();
        }

        protected void CheckPinBtn_Click(object sender, EventArgs e)
        {
            UsersModel user = new UsersModel();
            user.id = Convert.ToInt32(Request.QueryString["userId"]);
            user.pin = PinTxt.Text;

            if (user.CheckPin())
            {
                Session["PinStatus"] = "True";

                Response.Redirect("EditProfileForm.aspx");
            }
            else
            {
                Session["PinStatus"] = "False";

                Response.Redirect("UserProfile.aspx?userId=" + Request.QueryString["userId"]);
            }
        }
    }
}