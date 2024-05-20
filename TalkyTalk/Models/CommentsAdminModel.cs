using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.WebControls;
using TalkyTalk;

namespace ForumApp.Models
{
    class CommentViewModel
    {
        SqlConnection myConnection = new SqlConnection();

        public DataTable ReadComments()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"select * from dbo.comments";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading comments: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }


        public void CreateComment(string commentText)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"INSERT INTO dbo.comments (description, comment_date) 
                                 VALUES (@comment_text, @comment_date)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@comment_text", commentText);
                command.Parameters.AddWithValue("@comment_date", DateTime.Now);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    throw new Exception("Failed to create comment.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating comment: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void UpdateComment(int commentId, string commentText)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"UPDATE comments SET description = @comment_text 
                                 WHERE id_comment = @commentId";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@comment_text", commentText);
                command.Parameters.AddWithValue("@commentId", commentId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    throw new Exception("Failed to update comment.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating comment: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void DeleteComment(int commentId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string deleteCommentQuery = "DELETE FROM comments WHERE id_comment = @commentId";
                SqlCommand deleteCommentCommand = new SqlCommand(deleteCommentQuery, myConnection);
                deleteCommentCommand.Parameters.AddWithValue("@commentId", commentId);
                deleteCommentCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting comment: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public ArrayList GetDataById(int commentId)
        {
            ArrayList data = new ArrayList();
            SqlDataReader dr = null;
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                            c.id_comment, 
                            c.description AS comment_text
                        FROM 
                            comments c
                        WHERE
                            c.id_comment = @commentId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@commentId", commentId);

                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    data.Add(dr["id_comment"].ToString());
                    data.Add(dr["comment_text"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr = null;
                throw new Exception($"Error getting comment data: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
            return data;
        }


    }
}
