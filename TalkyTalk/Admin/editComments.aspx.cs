using ForumApp.Models;
using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class editComments : System.Web.UI.Page
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
                    // Check if there is an ID parameter in the URL
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        // Get the comment ID from the URL parameter
                        int commentId = Convert.ToInt32(Request.QueryString["id"]);

                        // Fill comment data into form
                        FillCommentData(commentId);
                    }
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        private void FillCommentData(int commentId)
        {
            try
            {
                CommentViewModel commentViewModel = new CommentViewModel();

                // Get comment data by ID
                var commentData = commentViewModel.GetDataById(commentId);

                // Check if data is found
                if (commentData != null)
                {
                    // Fill data into controls
                    txtIdComment.Text = commentData[0].ToString();
                    txtCommentText.Text = commentData[1].ToString();
                }
                else
                {
                    response.Text = "Comment not found.";
                }
            }
            catch (Exception ex)
            {
                response.Text = "Error: " + ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int commentId = Convert.ToInt32(txtIdComment.Text);
                string commentText = txtCommentText.Text;

                CommentViewModel commentViewModel = new CommentViewModel();
                commentViewModel.UpdateComment(commentId, commentText);

                // Display a success message and redirect after a delay
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Update successful!'); window.location = 'AdminComments.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + ex.Message + "');", true);
            }
        }
    }
}
