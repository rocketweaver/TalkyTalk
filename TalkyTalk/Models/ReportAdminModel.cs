using System;
using System.Data;
using System.Data.SqlClient;
using TalkyTalk;

namespace ForumApp.Models
{
    class ReportAdminModel
    {
        SqlConnection myConnection = new SqlConnection();

        public DataSet Read()
        {
            DataSet ds = new DataSet();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    r.*, 
                                    p.description AS post_description, 
                                    p.user_id AS post_owner_id, 
                                    u_post.username AS post_owner_username, 
                                    c.description AS comment_description, 
                                    c.user_id AS comment_owner_id, 
                                    u_comment.username AS comment_owner_username
                                FROM 
                                    reports r
                                LEFT JOIN 
                                    posts p ON r.post_id = p.id_post
                                LEFT JOIN 
                                    comments c ON r.comment_id = c.id_comment
                                LEFT JOIN 
                                    users u_post ON p.user_id = u_post.id_user
                                LEFT JOIN 
                                    users u_comment ON c.user_id = u_comment.id_user";

                SqlDataAdapter da = new SqlDataAdapter(query, myConnection);
                da.Fill(ds, "reports");

                return ds;
            }
            catch (Exception ex)
            {
                // Log the error for later review
                // You can use a logging framework like Serilog, NLog, or log4net
                // Or you can log to a file or database directly

                // You can also display an error message on the web page
                // For example:
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
