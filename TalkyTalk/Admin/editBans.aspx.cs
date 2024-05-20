using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ForumApp.Models;
using TalkyTalk;

namespace AdminPageTalky.Admin
{
    public partial class editBans : System.Web.UI.Page
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
                        int banId = Convert.ToInt32(Request.QueryString["id"]);

                        // Fill ban data into form
                        FillBanData(banId);
                    }
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        private void FillBanData(int banId)
        {
            try
            {
                BanAdminModel banViewModel = new BanAdminModel();

                // Get ban data by ID
                ArrayList banData = banViewModel.GetDataById(banId);

                // Check if data is found
                if (banData.Count > 0)
                {
                    // Fill data into controls
                    txtBanId.Text = banId.ToString();
                    txtStartDate.Text = banData[0].ToString();
                    txtEndDate.Text = banData[1].ToString();
                }
                else
                {
                    response.Text = "Ban not found.";
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
                // Get ban ID from form
                int banId = Convert.ToInt32(txtBanId.Text);

                // Get start date and end date values from form controls
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                DateTime endDate = Convert.ToDateTime(txtEndDate.Text);

                // Update ban using BanViewModel
                BanAdminModel banViewModel = new BanAdminModel();
                banViewModel.UpdateBan(banId, startDate, endDate);

                // Display a success message and redirect
                ScriptManager.RegisterStartupScript(this, GetType(), "showSuccessAlert", "showSuccessAlert('Update Ban successful!'); window.location = 'AdminBan.aspx';", true);
            }
            catch (Exception ex)
            {
                // Display an error message
                ScriptManager.RegisterStartupScript(this, GetType(), "showFailAlert", "showFailAlert('Update failed: " + ex.Message + "');", true);
            }
        }

        protected void txtEndDate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
