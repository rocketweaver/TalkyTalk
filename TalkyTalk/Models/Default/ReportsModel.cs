using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TalkyTalk.Models.Default
{
    public class ReportsModel
    {
        public int id;
        public int reporterId;
        public string postId;
        public string commentId;
        public string desc;

        SqlConnection myConnection = new SqlConnection();

        public bool Create()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();
                string query;

                if (string.IsNullOrEmpty(postId))
                {
                    query = "INSERT INTO reports (reporter_id, comment_id, description) " +
                            "VALUES (@reporterId, @commentId, @desc)";
                }
                else
                {
                    query = "INSERT INTO reports (reporter_id, post_id, description) " +
                            "VALUES (@reporterId, @postId, @desc)";
                }

                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@reporterId", reporterId);
                com.Parameters.AddWithValue("@desc", desc);

                if (string.IsNullOrEmpty(postId))
                {
                    com.Parameters.AddWithValue("@commentId", commentId);
                }
                else
                {
                    com.Parameters.AddWithValue("@postId", postId);
                }

                int i = com.ExecuteNonQuery();

                if(i > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                myConnection.Close();
            }

            return false;
        }
    }
}