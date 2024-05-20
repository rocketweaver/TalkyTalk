using Admin_Page_Web;
using ForumApp.Models;
using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TalkyTalk;

namespace AdminTalkyTalky.Admin
{
    public partial class deletePosts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the "id" parameter exists in the URL and if the deletion confirmation is enabled
                if (!string.IsNullOrEmpty(Request.QueryString["id"]) && Request.QueryString["confirm"] == "true")
                {
                    try
                    {
                        // Get the value of the "id" parameter from the URL
                        int postId = Convert.ToInt32(Request.QueryString["id"]);
                        // Perform the deletion of the post
                        DeletePost(postId);
                    }
                    catch (FormatException ex)
                    {
                        ShowErrorMessage($"Invalid post ID format: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage($"Error deleting post: {ex.Message}");
                    }
                }
            }
        }

        private void DeletePost(int postId)
        {
            try
            {
                // Create an instance of the PostAdminModel class
                PostAdminModel postViewModel = new PostAdminModel();

                // Call the DeletePost method to delete the post based on the ID
                postViewModel.DeletePost(postId);

                // Display a success message using SweetAlert
                string script = @"<script>
                                    Swal.fire({
                                        title: 'Success',
                                        text: 'Post deleted successfully',
                                        icon: 'success',
                                        confirmButtonText: 'OK'
                                    }).then((result) => {
                                        if (result.isConfirmed) {
                                            window.location.href = 'AdminPost.aspx'; // Redirect to post list page
                                        }
                                    });
                                </script>";
                ClientScript.RegisterStartupScript(this.GetType(), "DeleteSuccess", script);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error deleting post: {ex.Message}");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if the "id" parameter exists in the URL
            /*if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                try
                {
                    // Get the value of the "id" parameter from the URL
                    int postId = Convert.ToInt32(Request.QueryString["id"]);

                    // Display confirmation message before deletion
                    string script = $@"<script>
                                            Swal.fire({{
                                                title: 'Are you sure?',
                                                text: 'You are about to delete this post. This action cannot be undone.',
                                                icon: 'warning',
                                                showCancelButton: true,
                                                confirmButtonColor: '#d33',
                                                cancelButtonColor: '#3085d6',
                                                confirmButtonText: 'Yes, delete it!'
                                            }}).then((result) => {{
                                                if (result.isConfirmed) {{
                                                    window.location.href = 'deletePosts.aspx?id={postId}&confirm=true'; // Proceed with deletion
                                                }}
                                            }});
                                        </script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "DeleteConfirm", script);
                }
                catch (FormatException ex)
                {
                    ShowErrorMessage($"Invalid post ID format: {ex.Message}");
                }
                catch (Exception ex)
                {
                    ShowErrorMessage($"Error displaying confirmation: {ex.Message}");
                }
            }*/
            Response.Redirect($"deletePosts.aspx?id={Request.QueryString["id"]}&confirm=true");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect to the post list page
            Response.Redirect("AdminPost.aspx");
        }

        private void ShowErrorMessage(string message)
        {
            string script = $@"<script>
                                Swal.fire({{
                                    title: 'Error',
                                    text: '{message}',
                                    icon: 'error',
                                    confirmButtonText: 'OK'
                                }}).then((result) => {{
                                    if (result.isConfirmed) {{
                                        window.location.href = 'AdminPost.aspx'; // Redirect to post list page
                                    }}
                                }});
                            </script>";
            ClientScript.RegisterStartupScript(this.GetType(), "DeleteError", script);
        }
    }
}
