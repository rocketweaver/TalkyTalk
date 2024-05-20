using ForumApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalkyTalk.Models.Default;
using TalkyTalk.Models.DefaultUsers;

namespace TalkyTalk
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("Index.aspx");
            }

            R_UsernameTxt.Attributes.Add("maxlength", "30");
            R_PinTxt.Attributes.Add("maxlength", "6");
        }

        protected void SignupSubmitBtn_Click(object sender, EventArgs e)
        {
            UsersModel user = new UsersModel();

            user.username = R_UsernameTxt.Text;
            user.email = R_EmailTxt.Text;
            user.password = R_PasswordTxt.Text;
            user.pin = R_PinTxt.Text;

            /*R_UsernameTxt.Text = "";
            R_EmailTxt.Text = "";
            R_PasswordTxt.Text = "";
            R_PinTxt.Text = "";*/

            Response.Redirect("SignForm.aspx?IsSignup=" + user.Create());
        }

        protected void SigninSubmitBtn_Click(object sender, EventArgs e)
        {
            UsersModel user = new UsersModel();

            user.email = L_EmailTxt.Text;
            user.password = L_PasswordTxt.Text;


            if (user.Login())
            {
                BansModel bans = new BansModel();
                bans.UserId = UsersModel.UserId;

                if (bans.CheckBan() == 0)
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    UsersModel.Email,
                    DateTime.Now,
                    DateTime.MaxValue,
                    true,
                    $"{UsersModel.UserId}|{UsersModel.Username}|{UsersModel.Level}|{UsersModel.Email}"
                );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    authCookie.Expires = ticket.Expiration;

                    Response.Cookies.Add(authCookie);

                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        Response.Redirect(returnUrl);
                    }
                    else
                    {
                        if (UsersModel.Level == 2)
                        {
                            Response.Redirect("Admin/Index.aspx");
                        }
                        else
                        {
                            Response.Redirect("Index.aspx");
                        }
                    }
                }
                else
                {
                    /*ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You have been banned for " + bans.CheckBan() + " days')", true);*/
                    Response.Redirect("SignForm.aspx?BannedDays=" + bans.CheckBan());

                }
            }
            else
            {
                Response.Redirect("SignForm.aspx?IsSignin=False");
            }

            L_EmailTxt.Text = "";
            L_PasswordTxt.Text = "";
        }
    }
}