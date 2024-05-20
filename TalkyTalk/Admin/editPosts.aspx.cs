using ForumApp.Models;
using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TalkyTalk;

namespace AdminTalkyTalky.Admin
{
    public partial class editPosts : System.Web.UI.Page
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
                    // Check if the "id" parameter exists in the URL
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        // Get the value of the "id" parameter from the URL
                        int postId = Convert.ToInt32(Request.QueryString["id"]);

                        // Load post data based on the post ID
                        LoadPostData(postId);
                    }
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }

        }

        private void LoadPostData(int postId)
        {
            try
            {
                PostAdminModel postViewModel = new PostAdminModel();

                // Get post data by ID
                ArrayList postData = postViewModel.GetDataById(postId);

                // Check if data is found
                if (postData != null)
                {
                    // Fill data into controls
                    txtIdPost.Text = postData[0].ToString();
                    txtTitle.Text = postData[1].ToString();
                    txtDescription.Text = postData[2].ToString();
                }
                else
                {
                    response.Text = "Post not found.";
                }
            }
            catch (Exception ex)
            {
                response.Text = "Error: " + ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Get post ID from URL
                int postId = Convert.ToInt32(Request.QueryString["id"]);

                // Get title and description values from form controls
                string title = txtTitle.Text;
                string description = txtDescription.Text;

                // Update post using PostViewModel
                PostAdminModel postViewModel = new PostAdminModel();
                postViewModel.UpdatePost(postId, title, description);
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Update successful!'); window.location = 'AdminPost.aspx';", true);
            }
            catch (Exception ex)
            {
                // Display an error message using GlobalAlert
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + ex.Message + "');", true);
            }
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
