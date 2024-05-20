using System;
using System.Web.UI;
using ForumApp;
using ForumApp.Models;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class deleteUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            int userLevel = Convert.ToInt32(userData[2]);

            if (userLevel == 2)
            {
                // Periksa apakah ada parameter id dalam URL
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    // Ambil nilai id dari parameter URL
                    int userIdToDelete = Convert.ToInt32(Request.QueryString["id"]);

                    // Tampilkan konfirmasi penghapusan
                    ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmDelete", $"confirmDelete({userIdToDelete});", true);
                }
            }
            else
            {
                Response.Redirect("../Index.aspx");
            }

        }

        // Metode untuk menghapus pengguna
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int userId = Convert.ToInt32(Request.QueryString["id"]);

                    UsersView userView = new UsersView();
                    string result = userView.Delete(userId);

                    // Tampilkan pesan hasil menggunakan SweetAlert
                    string script = "";
                    if (result == "Data has been deleted")
                    {
                        script = $@"<script>
                                        swal.fire({{
                                            title: 'Success',
                                            text: 'User has been deleted successfully.',
                                            icon: 'success',
                                            confirmButtonText: 'OK'
                                        }}).then((result) => {{
                                            if (result.isConfirmed) {{
                                                window.location.href = 'AdminUser.aspx'; // Redirect ke halaman AdminUser.aspx setelah pengguna dikonfirmasi
                                            }}
                                        }});
                                    </script>";
                    }
                    else
                    {
                        script = $@"<script>
                                        swal.fire({{
                                            title: 'Error',
                                            text: '{result}',
                                            icon: 'error',
                                            confirmButtonText: 'OK'
                                        }});
                                    </script>";
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "deleteResult", script, false);
                }
                else
                {
                    // Tampilkan pesan kesalahan jika parameter id tidak ada dalam URL
                    litScript.Text = @"<script>alert('User ID not found in URL.');</script>";
                }
            }
            catch (Exception ex)
            {
                // Tampilkan pesan kesalahan menggunakan SweetAlert
                string errorScript = $@"<script>
                                            swal.fire({{
                                                title: 'Error',
                                                text: '{ex.Message}',
                                                icon: 'error',
                                                confirmButtonText: 'OK'
                                            }});
                                        </script>";

                ScriptManager.RegisterStartupScript(this, GetType(), "deleteError", errorScript, false);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Redirect ke halaman AdminUser.aspx saat tombol "Cancel" ditekan
            Response.Redirect("AdminUser.aspx");
        }
    }
}
