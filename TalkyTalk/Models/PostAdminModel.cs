using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TalkyTalk;

namespace ForumApp.Models
{
    public class PostAdminModel
    {
        SqlConnection myConnection = new SqlConnection();

        public DataTable ReadPosts()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    p.id_post, 
                                    p.title, 
                                    p.description, 
                                    p.post_date, 
                                    p.like_count, 
                                    u.username AS user_username, 
                                    u.level AS user_level
                                FROM 
                                    posts p
                                INNER JOIN 
                                    users u ON p.user_id = u.id_user";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                // Tangani pesan kesalahan dengan melemparkannya sebagai pengecualian
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }

        public void CreatePost(string title, string description, int userId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"INSERT INTO posts (title, description, post_date, user_id) 
                                 VALUES (@title, @description, @post_date, @user_id)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@post_date", DateTime.Now);
                command.Parameters.AddWithValue("@user_id", userId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    // Jika tidak ada baris yang terpengaruh, lemparkan pengecualian
                    throw new Exception("Failed to create post.");
                }
            }
            catch (Exception ex)
            {
                // Tangani pesan kesalahan dengan melemparkannya sebagai pengecualian
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public ArrayList GetDataById(int postId)
        {
            ArrayList data = new ArrayList();
            SqlDataReader dr = null;
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();
                string query = @"
                SELECT 
                    p.id_post, 
                    p.title, 
                    p.description, 
                    p.post_date,
                    p.user_id
                FROM 
                    posts p
                INNER JOIN 
                    users u ON p.user_id = u.id_user
                WHERE
                    p.id_post = @postId";

                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);

                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    data.Add(dr["id_post"].ToString());
                    data.Add(dr["title"].ToString());
                    data.Add(dr["description"].ToString());
                    data.Add(dr["post_date"].ToString());

                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr = null;
                throw new Exception($"Error getting post data: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
            return data;
        }


        public void UpdatePost(int postId, string title, string description)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"UPDATE posts SET title = @title, description = @description, edited = 1 
                          WHERE id_post = @postId";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@postId", postId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    // Jika tidak ada baris yang terpengaruh, lemparkan pengecualian
                    throw new Exception("Failed to update post.");
                }
            }
            catch (Exception ex)
            {
                // Tangani pesan kesalahan dengan melemparkannya sebagai pengecualian
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }


        public void DeletePost(int postId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                using (SqlTransaction transaction = myConnection.BeginTransaction())
                {
                    try
                    {
                        string deleteShareQuery = "DELETE FROM shared_posts WHERE post_id = @id";
                        SqlCommand deleteShareCommand = new SqlCommand(deleteShareQuery, myConnection, transaction);
                        deleteShareCommand.Parameters.AddWithValue("@id", postId);
                        deleteShareCommand.ExecuteNonQuery();

                        string deleteLikeQuery = "DELETE FROM liked_posts WHERE post_id = @id";
                        SqlCommand deleteLikeCommand = new SqlCommand(deleteLikeQuery, myConnection, transaction);
                        deleteLikeCommand.Parameters.AddWithValue("@id", postId);
                        deleteLikeCommand.ExecuteNonQuery();

                        string deleteReportsQuery = "DELETE FROM reports WHERE post_id = @id";
                        SqlCommand deleteReportsCommand = new SqlCommand(deleteReportsQuery, myConnection, transaction);
                        deleteReportsCommand.Parameters.AddWithValue("@id", postId);
                        deleteReportsCommand.ExecuteNonQuery();

                        string deleteCommentsQuery = "DELETE FROM comments WHERE post_id = @id";
                        SqlCommand deleteCommentsCommand = new SqlCommand(deleteCommentsQuery, myConnection, transaction);
                        deleteCommentsCommand.Parameters.AddWithValue("@id", postId);
                        deleteCommentsCommand.ExecuteNonQuery();

                        string deletePostQuery = "DELETE FROM posts WHERE id_post = @id";
                        SqlCommand deletePostCommand = new SqlCommand(deletePostQuery, myConnection, transaction);
                        deletePostCommand.Parameters.AddWithValue("@id", postId);
                        deletePostCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
