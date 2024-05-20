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
    public partial class AdminUser : System.Web.UI.Page
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
            }
            else
            {
                Response.Redirect("../Index.aspx");
            }
        }

        protected void DisplayUserData()
        {
            try
            {
                // Create an instance of UsersView
                UsersView usersView = new UsersView();

                // Get DataSet from the Read method in UsersView
                DataSet dataSet = usersView.Read();

                // Check if the DataSet is not null and has a table named "users"
                if (dataSet != null && dataSet.Tables.Contains("users"))
                {
                    string display = "<table id='table' class='table table-striped'><thead><tr>";

                    // Add column headers
                    display += "<th>User ID</th>";
                    display += "<th>Username</th>";
                    display += "<th>Email</th>";
                    display += "<th>Password</th>";
                    display += "<th>Level</th>";
                    display += "<th>Edit</th>"; // Add Edit column header
                    display += "<th>Delete</th>"; // Add Delete column header

                    display += "</tr></thead><tbody>";

                    // Get data rows from the first DataTable
                    foreach (DataRow dr in dataSet.Tables["users"].Rows)
                    {
                        display += "<tr>";
                        // Add data for each column
                        display += "<td>" + dr["id_user"] + "</td>";
                        display += "<td>" + dr["username"] + "</td>";
                        display += "<td>" + dr["email"] + "</td>";
                        display += "<td>" + dr["password"] + "</td>";
                        display += "<td>" + dr["level"] + "</td>";
                        // Add links to Edit and Delete pages, passing User ID as parameter
                        display += "<td><a href='editUser.aspx?id=" + dr["id_user"] + "' class='btn btn-outline-danger'>Edit</a></td>";
                        display += "<td><a href='deleteUser.aspx?id=" + dr["id_user"] + "' class='btn btn-outline-warning'>Delete</a></td>";
                        display += "</tr>";
                    }

                    display += "</tbody></table>";

                    // Set HTML table to Literal to be displayed on the page
                    lt_table.Text = display;
                }
                else
                {
                    // Show message if DataSet is empty or doesn't have a table named "users"
                    lt_table.Text = "<p>No data available</p>";
                }
            }
            catch (Exception ex)
            {
                // Handle error message by displaying it on Literal
                lt_table.Text = "<p>Error: " + ex.Message + "</p>";
            }
        }

        protected void btsimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(level.Text))
            {
                // Jika salah satu field kosong, tampilkan pesan kesalahan
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + "');", true);
            }
            UsersView usersView = new UsersView();
            usersView.Username = txtUsername.Text;
            usersView.Email = txtEmail.Text;
            usersView.Password = txtPassword.Text;
            usersView.Level = Convert.ToInt32(level.Text);

            string result = usersView.Create();

            if (result == "Data berhasil dimasukkan")
            {
                DisplayUserData();
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Create successful!');", true);
                ClearData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert(' failed: " +"');", true);
            }
        }

        private void ClearData()
        {
            txtUsername.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            level.Text = string.Empty;
        }

        protected void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
