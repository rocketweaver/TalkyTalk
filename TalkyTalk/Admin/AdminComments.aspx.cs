using ForumApp.Models;
using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TalkyTalk;

namespace AdminPageTalky
{
    public partial class AdminComments : System.Web.UI.Page
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
                    BindCommentsToDisplay();
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CommentViewModel commentViewModel = new CommentViewModel();
            //    string commentText = txtCommentText.Text;

            //    commentViewModel.CreateComment(commentText);

            //    ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Create successful!');", true);

            //    BindCommentsToDisplay();
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + ex.Message + "');", true);
            //}
        }

        private void BindCommentsToDisplay()
        {
            try
            {
                CommentViewModel commentViewModel = new CommentViewModel();
                DataTable commentsData = commentViewModel.ReadComments();

                if (commentsData != null && commentsData.Rows.Count > 0)
                {
                    string display = "<table id='table' class='table table-striped'><thead><tr>";

                    display += "<th>Comment ID</th>";
                    display += "<th>Comment Text</th>";
                    display += "<th>Comment Date</th>";

                    display += "<th>Delete</th>";
                    /*display += "<th>Delete</th>";*/

                    display += "</tr></thead><tbody>";

                    foreach (DataRow dr in commentsData.Rows)
                    {
                        display += "<tr>";
                        display += "<td>" + dr["id_comment"] + "</td>";
                        display += "<td>" + dr["description"] + "</td>";
                        display += "<td>" + dr["comment_date"] + "</td>";

                        //display += "<td><a href='editComments.aspx?id=" + dr["id_comment"] + "' class='btn btn-outline-danger'>Edit</a></td>";
                        display += "<td><a href='deleteComments.aspx?id=" + dr["id_comment"] + "' class='btn btn-outline-warning'>Delete</a></td>";
                        display += "</tr>";
                    }

                    display += "</tbody></table>";

                    lt_table.Text = display;
                }
                else
                {
                    lt_table.Text = "<p>No data available</p>";
                }
            }
            catch (Exception ex)
            {
                lt_table.Text = "<p>Error: " + ex.Message + "</p>";
            }
        }
    }
}
