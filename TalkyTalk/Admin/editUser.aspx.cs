using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ForumApp;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            int userLevel = Convert.ToInt32(userData[2]);

            if (userLevel == 2)
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        int userIdToUpdate = Convert.ToInt32(Request.QueryString["id"]);
                        LoadUserData(userIdToUpdate);
                    }
                }
            }
            else
            {
                Response.Redirect("../Index.aspx");
            }

        }

        private void LoadUserData(int userId)
        {
            try
            {
                UsersView user = new UsersView();
                user.UserId = userId;
                user.ReadById();
                id_user.Text = user.UserId.ToString();
                txtUsername.Text = user.Username;
                txtEmail.Text = user.Email;
                txtPassword.Text = user.Password;
                level.Text = user.Level.ToString();
            }
            catch (Exception ex)
            {
                response.Text = $"<p>Error: {ex.Message}</p>";
            }
        }

        protected void btsimpan_Click(object sender, EventArgs e)
        {
            try
            {
                UsersView user = new UsersView();
                user.UserId = Convert.ToInt32(id_user.Text);
                user.Username = txtUsername.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;
                user.Level = Convert.ToInt32(level.Text);
                string result = user.Update();
                string script = $@"<script>
                            swal.fire({{
                                title: 'Success',
                                text: '{result}',
                                icon: 'success',
                                confirmButtonText: 'OK'
                            }}).then((result) => {{
                                if (result.isConfirmed) {{
                                    window.location.href = 'AdminUser.aspx'; 
                                }}
                            }});
                          </script>";
                ScriptManager.RegisterStartupScript(this, GetType(), "updateResult", script, false);
            }
            catch (Exception ex)
            {
                response.Text = $"<p>Error: {ex.Message}</p>";
            }
        }

        protected void level_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
