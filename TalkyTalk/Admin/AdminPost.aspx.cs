using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ForumApp.Models;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class AdminPost : System.Web.UI.Page
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
                    BindPostsToDisplay();
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnAddPost_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string title = txtTitle.Text;
            //    string description = txtDescription.Text;

            //    // Hardcoding admin user ID for demonstration, replace with actual logic to get admin user ID
            //    int adminUserId = 1;

            //    PostAdminModel postViewModel = new PostAdminModel();
            //    postViewModel.CreatePost(title, description, adminUserId);
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Post successful!');", true);

            //    BindPostsToDisplay();

            //    txtTitle.Text = ""; // Clear title textbox after adding post
            //    txtDescription.Text = ""; // Clear description textbox after adding post
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + ex.Message + "');", true);
            //}
        }

        protected void BindPostsToDisplay()
        {
            try
            {
                PostAdminModel postViewModel = new PostAdminModel();

                // Fetch posts data
                DataTable postsData = postViewModel.ReadPosts();

                // Check if DataTable is not null and has rows
                if (postsData != null && postsData.Rows.Count > 0)
                {
                    string display = "<table id='table' class='table table-striped'><thead><tr>";

                    // Add column headers
                    display += "<th>Post ID</th>";
                    display += "<th>Title</th>";
                    display += "<th>Description</th>";
                    display += "<th>Post Date</th>";
                    display += "<th>Like Count</th>";
                    display += "<th>Username</th>";
                    display += "<th>User Level</th>";
                    display += "<th>Delete</th>"; // Add Edit column header
                    /*display += "<th>Delete</th>"; // Add Delete column header*/

                    display += "</tr></thead><tbody>";

                    // Iterate through each row in the DataTable
                    foreach (DataRow dr in postsData.Rows)
                    {
                        display += "<tr>";
                        // Add data for each column
                        display += "<td>" + dr["id_post"] + "</td>";
                        display += "<td>" + dr["title"] + "</td>";
                        display += "<td>" + dr["description"] + "</td>";
                        display += "<td>" + dr["post_date"] + "</td>";
                        display += "<td>" + dr["like_count"] + "</td>";
                        display += "<td>" + dr["user_username"] + "</td>";
                        display += "<td>" + dr["user_level"] + "</td>";
                        // Add links to Edit and Delete pages, passing Post ID as parameter
                        //display += "<td><a href='editPosts.aspx?id=" + dr["id_post"] + "' class='btn btn-outline-danger'>Edit</a></td>";
                        display += "<td><a href='deletePosts.aspx?id=" + dr["id_post"] + "' class='btn btn-outline-warning'>Delete</a></td>";

                        display += "</tr>";
                    }

                    display += "</tbody></table>";

                    // Set HTML table to Literal to be displayed on the page
                    lt_table.Text = display;
                }
                else
                {
                    // Show message if DataTable is empty
                    lt_table.Text = "<p>No data available</p>";
                }
            }
            catch (Exception ex)
            {
                // Handle error message by displaying it on Literal
                lt_table.Text = "<p>Error: " + ex.Message + "</p>";
            }
        }

        protected void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
