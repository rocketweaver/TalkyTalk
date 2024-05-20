using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ForumApp.Models;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class deleteBan : System.Web.UI.Page
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
                    int banIdToDelete = Convert.ToInt32(Request.QueryString["id"]);

                    // Tampilkan konfirmasi penghapusan
                    ScriptManager.RegisterStartupScript(this, GetType(), "ConfirmDelete", $"confirmDelete({banIdToDelete});", true);
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int banId = Convert.ToInt32(Request.QueryString["id"]);

                    // Panggil metode DeleteBan
                    DeleteBan(banId);
                }
                else
                {
                    // Tampilkan pesan kesalahan jika parameter id tidak ada dalam URL
                    litScript.Text = @"<script>alert('Ban ID not found in URL.');</script>";
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
            // Redirect ke halaman AdminBan.aspx saat tombol "Cancel" ditekan
            Response.Redirect("AdminBan.aspx");
        }

        protected void DeleteBan(int banId)
        {
            try
            {
                // Lakukan penghapusan ban
                BanAdminModel banViewModel = new BanAdminModel();
                banViewModel.DeleteBan(banId);

                // Tampilkan pesan hasil menggunakan SweetAlert
                string script = $@"<script>
                                    swal.fire({{
                                        title: 'Success',
                                        text: 'Ban has been deleted successfully.',
                                        icon: 'success',
                                        confirmButtonText: 'OK'
                                    }}).then((result) => {{
                                        if (result.isConfirmed) {{
                                            window.location.href = 'AdminBan.aspx'; // Redirect ke halaman AdminBan.aspx setelah ban dihapus
                                        }}
                                    }});
                                </script>";

                ScriptManager.RegisterStartupScript(this, GetType(), "deleteResult", script, false);
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
    }
}
