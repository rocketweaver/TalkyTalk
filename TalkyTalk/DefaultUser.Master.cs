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
    public partial class DefaultUser : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)this.Page.User.Identity).Ticket;
                string[] userData = ticket.UserData.Split('|');
                string username = userData[1];

                UsernameLabel.Text = username;
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("SignForm.aspx");
        }

        protected void ProfileButton_Click(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)this.Page.User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            string userId = userData[0];

            Response.Redirect("UserProfile.aspx?userId=" + userId);
        }
    }
}