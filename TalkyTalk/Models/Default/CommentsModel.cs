using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TalkyTalk.Models.Default
{
    public class CommentsModel
    {
        public string id;
        public string userId;
        public string postId;
        public string description;
        public string commentDate;

        SqlConnection myConnection = new SqlConnection();

        public DataSet ReadByPostId()
        {
            DataSet ds = new DataSet();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT c.*, u.username 
                                FROM comments c
                                JOIN users u ON c.user_id = u.id_user
                                WHERE c.post_id = @post_id
                                ORDER BY c.id_comment";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@post_id", postId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "comments");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        public void Create()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "INSERT INTO comments (user_id, post_id, description, comment_date) " +
                                "VALUES (@user_id, @post_id, @desc, @date);";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@user_id", userId);
                com.Parameters.AddWithValue("@post_id", postId);
                com.Parameters.AddWithValue("@desc", description);
                com.Parameters.AddWithValue("@date", DateTime.Now);
                int i = com.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void Update()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "UPDATE comments SET description = @desc, comment_date = @date, " +
                                "edited = 1 WHERE id_comment = @id;";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@date", DateTime.Now);
                com.Parameters.AddWithValue("@desc", description);
                com.Parameters.AddWithValue("@id", id);
                int i = com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void Delete()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                using (SqlTransaction transaction = myConnection.BeginTransaction())
                {
                    try
                    {
                        string deleteReportsQuery = "DELETE FROM reports WHERE comment_id = @id";
                        SqlCommand deleteReportsCommand = new SqlCommand(deleteReportsQuery, myConnection, transaction);
                        deleteReportsCommand.Parameters.AddWithValue("@id", id);
                        deleteReportsCommand.ExecuteNonQuery();

                        string deleteCommentQuery = "DELETE FROM comments WHERE id_comment = @id";
                        SqlCommand deleteCommentCommand = new SqlCommand(deleteCommentQuery, myConnection, transaction);
                        deleteCommentCommand.Parameters.AddWithValue("@id", id);
                        deleteCommentCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}