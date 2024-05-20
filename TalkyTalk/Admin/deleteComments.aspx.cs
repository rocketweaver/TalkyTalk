using System;
using System.Web;
using System.Web.Security;
using ForumApp.Models;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class deleteComments : System.Web.UI.Page
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
                    // Check if the id parameter is in the URL and if confirmation is enabled
                    if (!string.IsNullOrEmpty(Request.QueryString["id"]) && Request.QueryString["confirm"] == "true")
                    {
                        // Get the id value from the URL parameter
                        int commentId = Convert.ToInt32(Request.QueryString["id"]);

                        // Perform the comment deletion
                        DeleteComment(commentId);
                    }
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }

            
        }

        private void DeleteComment(int commentId)
        {
            try
            {
                // Create an instance of the CommentViewModel class
                CommentViewModel commentViewModel = new CommentViewModel();

                // Call the DeleteComment method to delete the comment based on the ID
                commentViewModel.DeleteComment(commentId);

                // Display a success message using SweetAlert
                string script = @"<script>
                                    Swal.fire({
                                        title: 'Success',
                                        text: 'Comment deleted successfully',
                                        icon: 'success',
                                        confirmButtonText: 'OK'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            window.location.href = 'AdminComments.aspx'; // Redirect to comment list page
                                        }
                                    });
                                </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "DeleteSuccess", script);
            }
            catch (Exception ex)
            {
                // Display an error message using SweetAlert
                string script = $@"<script>
                                    Swal.fire({{
                                        title: 'Error',
                                        text: 'Error deleting comment: {ex.Message}',
                                        icon: 'error',
                                        confirmButtonText: 'OK'
                                    }}).then((result) => {{
                                        if (result.isConfirmed) {{
                                            window.location.href = 'AdminComment.aspx'; // Redirect to comment list page
                                        }}
                                    }});
                                </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "DeleteError", script);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Confirm the deletion
            Response.Redirect($"deleteComments.aspx?id={Request.QueryString["id"]}&confirm=true");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel the deletion and redirect to the comment list page
            Response.Redirect("AdminComments.aspx");
        }
    }
}
