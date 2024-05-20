using ForumApp;
using ForumApp.Models;
using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class AdminBan : System.Web.UI.Page
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
                    DisplayUserData();
                }
            } else
            {
                Response.Redirect("Index.aspx");
            }

            
        }

        protected void btsimpan_Click(object sender, EventArgs e)
        {
            try
            {
                BanAdminModel banViewModel = new BanAdminModel();

                // Get input values from textboxes
                int userId = Convert.ToInt32(txtUserID.Text);
                DateTime startDate = DateTime.TryParseExact(txtStartDate.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate) ? startDate : DateTime.MinValue;
                DateTime endDate = DateTime.TryParseExact(txtEndDate.Text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate) ? endDate : DateTime.MinValue;

                // Call method to create a ban
                banViewModel.CreateBan(userId, startDate, endDate);

                // Display success message
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('User Has Ban successful!');", true);

                // Refresh the gridview with updated data
                DisplayUserData();

                // Clear input fields
                ClearInputFields();
            }
            catch (Exception ex)
            {
                // Display error message
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Insert failed: " + ex.Message + "');", true);
            }
        }

        private void ClearInputFields()
        {
            // Clear input fields
            txtUserID.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
        }

        protected void DisplayUserData()
        {
            try
            {
                BanAdminModel banViewModel = new BanAdminModel();

                // Fetch bans data
                DataTable bansData = banViewModel.ReadBans();

                // Check if the DataTable is not null and has rows
                if (bansData != null && bansData.Rows.Count > 0)
                {
                    string display = "<table id='table' class='table table-striped'><thead><tr>";

                    // Add column headers
                    display += "<th>Ban Id</th>";
                    display += "<th>User ID</th>";
                    display += "<th>Username</th>";
                    display += "<th>Start Date</th>";
                    display += "<th>End Date</th>";
                    display += "<th>Edit</th>"; // Add Edit column header
                    display += "<th>Delete</th>"; // Add Delete column header

                    display += "</tr></thead><tbody>";

                    // Iterate through each row in the DataTable
                    foreach (DataRow dr in bansData.Rows)
                    {
                        display += "<tr>";
                        // Add data for each column
                        display += "<td>" + dr["id_ban"] + "</td>";
                        display += "<td>" + dr["user_id"] + "</td>";
                        display += "<td>" + dr["username"] + "</td>";
                        display += "<td>" + dr["start_date"] + "</td>";
                        display += "<td>" + dr["end_date"] + "</td>";
                        // Add links to Edit and Delete pages, passing Ban ID as parameter
                        display += "<td><a href='editBans.aspx?id=" + dr["id_ban"] + "' class='btn btn-outline-danger'>Edit</a></td>";
                        display += "<td><a href='deleteBan.aspx?id=" + dr["id_ban"] + "' class='btn btn-outline-warning'>Delete</a></td>";

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

    }
}
