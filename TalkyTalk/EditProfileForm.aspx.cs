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
    public partial class EditProfileForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/SignForm.aspx");
                }
                else if (Session["PinStatus"] == null || Session["PinStatus"].ToString() != "True")
                {
                    Response.Redirect("~/UserProfile.aspx");
                }
                else
                {
                    NewPinTxt.Attributes.Add("maxlength", "6");
                    LoadUserData();

                    Session.Remove("PinStatus");
                }
            }
        }


        protected void LoadUserData()
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            string userId = userData[0];

            UsersModel user = new UsersModel();
            user.id = Convert.ToInt32(userId);

            DataRow dr = user.ReadById();

            if (dr != null)
            {
                UsernameTxt.Text = dr["username"].ToString();
                EmailTxt.Text = dr["email"].ToString();
            }
        }

        protected void ChangePasswordBtn_Click(object sender, EventArgs e)
        {
            Session["PinStatus"] = "True";

            Response.Redirect("EditProfileForm.aspx?ChangePassword=True");
        }

        protected void ChangePinBtn_Click(object sender, EventArgs e)
        {
            Session["PinStatus"] = "True";

            Response.Redirect("EditProfileForm.aspx?ChangePin=True");
        }

        protected void SubmitPinBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UsersModel user = new UsersModel();

                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string userId = userData[0];

                user.id = Convert.ToInt32(userId);
                user.pin = NewPinTxt.Text;

                Session["PinStatus"] = "True";

                Response.Redirect("EditProfileForm.aspx?IsProfileUpdated=" + user.Update());
            }
        }

        protected void SubmitPasswordBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UsersModel user = new UsersModel();

                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string userId = userData[0];

                user.id = Convert.ToInt32(userId);
                user.password = PasswordTxt.Text;

                Session["PinStatus"] = "True";

                Response.Redirect("EditProfileForm.aspx?IsProfileUpdated=" + user.Update());
            }
        }

        protected void SubmitProfileBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UsersModel user = new UsersModel();

                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string userId = userData[0];

                user.id = Convert.ToInt32(userId);
                user.username = UsernameTxt.Text;
                user.email = EmailTxt.Text;

                if (user.Update())
                {
                    if (user.SetUsernameAndEmail())
                    {
                        FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                            1,
                            UsersModel.Email,
                            DateTime.Now,
                            DateTime.MaxValue,
                            true,
                            $"{UsersModel.UserId}|{UsersModel.Username}|{UsersModel.Level}|{UsersModel.Email}"
                        );

                        string encryptedTicket = FormsAuthentication.Encrypt(newTicket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        Response.Cookies.Add(authCookie);

                        Session["PinStatus"] = "True";

                        Response.Redirect("EditProfileForm.aspx?IsProfileUpdated=" + user.Update());
                    }
                }
            }
        }

        protected void DeleteAccBtn_Click(object sender, EventArgs e)
        {
            UsersModel user = new UsersModel();

            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            string userId = userData[0];

            user.id = Convert.ToInt32(userId);

            if(user.Delete())
            {
                FormsAuthentication.SignOut();
                Response.Redirect("SignForm.aspx?IsAccountDeleted=" + user.Delete());
            } 
            else
            {
                Session["PinStatus"] = "True";
                Response.Redirect("EditProfileForm.aspx?IsProfileUpdated=" + user.Delete());
            }
        }
    }
}