using ForumApp.Models;
using System;
using System.Data;
using System.Web;
using System.Web.Security;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class AdminReport : System.Web.UI.Page
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
                    DisplayReportData();
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        protected void DisplayReportData()
        {
            try
            {
                ReportAdminModel reportViewModel = new ReportAdminModel();

                // Fetch report data
                DataSet reportData = reportViewModel.Read();

                // Check if the DataSet is not null and has tables
                if (reportData != null && reportData.Tables.Count > 0)
                {
                    string display = "<table id='table' class='table table-striped'><thead><tr>";

                    // Add column headers
                    display += "<th>Report ID</th>";
                    display += "<th>Report Desc</th>";
                    display += "<th>Post ID</th>";
                    display += "<th>Post Description</th>";
                    display += "<th>Post Owner ID</th>";
                    display += "<th>Post Owner Username</th>";
                    display += "<th>Comment ID</th>";
                    display += "<th>Comment Description</th>";
                    display += "<th>Comment Owner ID</th>";
                    display += "<th>Comment Owner Username</th>";
                    display += "</tr></thead><tbody>";

                    // Iterate through each row in the DataTable
                    foreach (DataRow dr in reportData.Tables[0].Rows)
                    {
                        display += "<tr>";
                        // Add data for each column
                        display += "<td>" + dr["id_report"] + "</td>";
                        display += "<td>" + dr["description"] + "</td>";
                        display += "<td>" + dr["post_id"] + "</td>";
                        display += "<td>" + dr["post_description"] + "</td>";
                        display += "<td>" + dr["post_owner_id"] + "</td>";
                        display += "<td>" + dr["post_owner_username"] + "</td>";
                        display += "<td>" + dr["comment_id"] + "</td>";
                        display += "<td>" + dr["comment_description"] + "</td>";
                        display += "<td>" + dr["comment_owner_id"] + "</td>";
                        display += "<td>" + dr["comment_owner_username"] + "</td>";
                        display += "</tr>";
                    }

                    display += "</tbody></table>";

                    // Set HTML table to Literal to be displayed on the page
                    lt_table.Text = display;
                }
                else
                {
                    // Show message if DataSet is empty
                    lt_table.Text = "<p>No data available</p>";
                }
            }
            catch (Exception ex)
            {
                // Handle error message by displaying it on Literal
                lt_table.Text = "<p>Error: " + ex.Message + "</p>";
            }
        }
    }
}
