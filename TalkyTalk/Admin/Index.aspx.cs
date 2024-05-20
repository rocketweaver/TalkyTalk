using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using AdminTalkyTalky.Models;
using TalkyTalk;

namespace Admin_Page_Web
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
            string[] userData = ticket.UserData.Split('|');
            int userLevel = Convert.ToInt32(userData[2]);

            if (userLevel == 2)
            {
                // Redirect the user to the login page or another page
                if (!IsPostBack)
                {
                    BindCharts();
                }
            } else
            {
                Response.Redirect("../Index.aspx");
            }

            
        }


        private void BindCharts()
        {
            ChartAdminModel chartAdminModel = new ChartAdminModel();

            // Bind Users Chart
            DataTable usersData = chartAdminModel.ReadUsers();
            BindChart(usersData, "usersChart", "User Level", "Username", "TotalUsers");

            // Bind Comments Chart
            DataTable commentsData = chartAdminModel.ReadComments();
            BindChart(commentsData, "commentsChart", "Comment Count", "Username", "TotalComments");

            // Bind Posts Chart
            DataTable postsData = chartAdminModel.ReadPosts();
            BindChart(postsData, "postsChart", "Post Count", "Title", "TotalLikes");

            // Bind Bans Chart
            DataTable bansData = chartAdminModel.ReadBans();
            BindChart(bansData, "bansChart", "Ban Count", "user_id", "TotalBans");
        }

        private void BindChart(DataTable data, string canvasId, string label, string labelField, string dataField)
        {
            string[] labels = new string[data.Rows.Count];
            int[] dataPoints = new int[data.Rows.Count];

            for (int i = 0; i < data.Rows.Count; i++)
            {
                labels[i] = data.Rows[i][labelField].ToString();
                dataPoints[i] = Convert.ToInt32(data.Rows[i][dataField]);
            }

            string jsonLabels = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(labels);
            string jsonData = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataPoints);

            string script = $@"
                var ctx = document.getElementById('{canvasId}').getContext('2d');
                var {canvasId} = new Chart(ctx, {{
                    type: 'bar',
                    data: {{
                        labels: {jsonLabels},
                        datasets: [{{
                            label: '{label}',
                            data: {jsonData},
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(255, 159, 64, 0.2)',
                                'rgba(255, 205, 86, 0.2)',
                                'rgba(75, 192, 192, 0.2)',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(153, 102, 255, 0.2)',
                                'rgba(201, 203, 207, 0.2)'
                            ],
                            borderColor: [
                                'rgb(255, 99, 132)',
                                'rgb(255, 159, 64)',
                                'rgb(255, 205, 86)',
                                'rgb(75, 192, 192)',
                                'rgb(54, 162, 235)',
                                'rgb(153, 102, 255)',
                                'rgb(201, 203, 207)'
                            ],
                            borderWidth: 1
                        }}]
                    }},
                    options: {{
                        scales: {{
                            y: {{
                                beginAtZero: true
                            }}
                        }}
                    }}
                }});
            ";

            ScriptManager.RegisterStartupScript(this, GetType(), canvasId + "Script", script, true);
        }
    }
}
