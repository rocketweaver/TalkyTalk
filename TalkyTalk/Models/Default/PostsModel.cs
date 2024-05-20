using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Common;
using TalkyTalk.Models.DefaultUsers;

namespace TalkyTalk.Models.Default
{
    public class PostsModel
    {
        SqlConnection myConnection = new SqlConnection();

        public string like;
        public string id;
        public string userId;
        public string title;
        public string desc;
        public string postId;

        public DataSet Search(string keyword)
        {
            DataSet ds = new DataSet();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                /*string query = "SELECT * FROM posts WHERE title LIKE @keyword";*/
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                WHERE title LIKE @keyword
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY p.post_date DESC, p.id_post DESC";

                SqlCommand com = new SqlCommand(query, myConnection);

                if (!string.IsNullOrEmpty(keyword))
                {
                    com.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                }

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ds;
        }

        public DataSet Read()
        {
            myConnection.ConnectionString = GlobalVariable.connString;
            myConnection.Open();

            DataSet ds = new DataSet();
            try
            {
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY p.post_date DESC, p.id_post DESC";

                SqlCommand com = new SqlCommand(query, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
            return ds;
        }

        public DataRow ReadById()
        {
            DataRow row = null;
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                if (string.IsNullOrEmpty(id))
                {
                    return row;
                }

                string query = @"SELECT p.id_post, 
                                p.title, 
                                CAST(p.description AS NVARCHAR(MAX)) AS description, 
                                p.post_date, 
                                p.user_id, 
                                p.edited, 
                                u.username
                         FROM posts p 
                         JOIN users u ON p.user_id = u.id_user 
                         WHERE p.id_post = @id";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = com.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                if (dt.Rows.Count > 0)
                {
                    row = dt.Rows[0];
                }

                return row;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public DataSet ReadByUserId()
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(userId))
            {
                return ds;
            }

            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                /*string query = "SELECT * FROM posts WHERE user_id = @id";*/
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.user_id = l.user_id
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                WHERE p.user_id = @id
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY p.post_date DESC, p.id_post DESC";

                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@id", userId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "personal_posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }

        public DataSet ReadByShare()
        {
            DataSet ds = new DataSet();
            if (string.IsNullOrEmpty(userId))
            {
                return ds;
            }

            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                /*string query = @"SELECT p.* 
                         FROM posts p
                         INNER JOIN shared_posts sp ON p.id_post = sp.post_id
                         WHERE sp.user_id = @userId";*/

                /*string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                WHERE p.user_id = @id
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY p.post_date DESC, p.id_post DESC";*/

                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.user_id = l.user_id
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                WHERE s.user_id = @userId
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY p.post_date DESC, p.id_post DESC";

                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@userId", userId);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "shared_posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }

        public DataSet OrderByLike()
        {
            myConnection.ConnectionString = GlobalVariable.connString;
            myConnection.Open();

            DataSet ds = new DataSet();
            try
            {
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY like_count DESC, p.post_date DESC, p.id_post DESC;
                ";

                SqlCommand com = new SqlCommand(query, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
            return ds;
        }

        public DataSet OrderByShare()
        {
            myConnection.ConnectionString = GlobalVariable.connString;
            myConnection.Open();

            DataSet ds = new DataSet();
            try
            {
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY share_count DESC, p.post_date DESC, p.id_post DESC;
                ";

                SqlCommand com = new SqlCommand(query, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
            return ds;
        }

        public DataSet OrderByCommented()
        {
            myConnection.ConnectionString = GlobalVariable.connString;
            myConnection.Open();

            DataSet ds = new DataSet();
            try
            {
                string query = @"
                SELECT p.id_post, 
                       p.title, 
                       MAX(CONVERT(NVARCHAR(MAX), p.description)) AS description, 
                       p.post_date, 
                       p.user_id, 
                       p.edited, 
                       u.username, 
                       COUNT(DISTINCT l.id_like) AS like_count, 
                       COUNT(DISTINCT s.id_share) AS share_count, 
                       COUNT(DISTINCT c.id_comment) AS comment_count 
                FROM posts p 
                JOIN users u ON p.user_id = u.id_user 
                LEFT JOIN liked_posts l ON p.id_post = l.post_id 
                LEFT JOIN shared_posts s ON p.id_post = s.post_id 
                LEFT JOIN comments c ON p.id_post = c.post_id 
                GROUP BY p.id_post, p.title, p.post_date, p.user_id, p.edited, u.username
                ORDER BY comment_count DESC, p.post_date DESC, p.id_post DESC";

                SqlCommand com = new SqlCommand(query, myConnection);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(ds, "posts");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                myConnection.Close();
            }
            return ds;
        }

        public int GetLikeCountByPostId()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM liked_posts WHERE post_id = @postId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);

                int likeCount = (int)com.ExecuteScalar();

                return likeCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public int GetShareCountByPostId()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM shared_posts WHERE post_id = @postId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);

                int shareCount = (int)com.ExecuteScalar();

                return shareCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public int GetCommentCountByPostId()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM comments WHERE post_id = @postId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);

                int commentCount = (int)com.ExecuteScalar();

                return commentCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                myConnection.Close();
            }
        }


        public bool HasLiked()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM liked_posts WHERE " +
                                "post_id = @postId AND user_id = @userId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);
                com.Parameters.AddWithValue("@userId", userId);

                int likeCount = (int)com.ExecuteScalar();

                return likeCount > 0;
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
        }

        public void Like()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string likeQuery = "INSERT INTO liked_posts (post_id, " +
                                    "user_id) VALUES (@postId, @userId)";
                SqlCommand likeCommand = new SqlCommand(likeQuery, myConnection);
                likeCommand.Parameters.AddWithValue("@postId", postId);
                likeCommand.Parameters.AddWithValue("@userId", userId);
                likeCommand.ExecuteNonQuery();

                /*string updateQuery = "UPDATE posts SET like_count = like_count " +
                                    "+ 1 WHERE id_post = @postId";
                SqlCommand updateCommand = new SqlCommand(updateQuery, myConnection);
                updateCommand.Parameters.AddWithValue("@postId", postId);
                updateCommand.ExecuteNonQuery();*/
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

        public void Unlike()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string unlikeQuery = "DELETE FROM liked_posts WHERE post_id = @postId AND user_id = @userId";
                SqlCommand unlikeCommand = new SqlCommand(unlikeQuery, myConnection);
                unlikeCommand.Parameters.AddWithValue("@postId", postId);
                unlikeCommand.Parameters.AddWithValue("@userId", userId);
                unlikeCommand.ExecuteNonQuery();

                string updateQuery = "UPDATE posts SET like_count = like_count - 1 WHERE id_post = @postId";
                SqlCommand updateCommand = new SqlCommand(updateQuery, myConnection);
                updateCommand.Parameters.AddWithValue("@postId", postId);
                updateCommand.ExecuteNonQuery();
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

        public bool HasShared()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM shared_posts WHERE " +
                                "post_id = @postId AND user_id = @userId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@postId", postId);
                com.Parameters.AddWithValue("@userId", userId);

                int shareCount = (int)com.ExecuteScalar();

                return shareCount > 0;
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
        }

        public void Share()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string likeQuery = "INSERT INTO shared_posts (post_id, " +
                                    "user_id) VALUES (@postId, @userId)";
                SqlCommand likeCommand = new SqlCommand(likeQuery, myConnection);
                likeCommand.Parameters.AddWithValue("@postId", postId);
                likeCommand.Parameters.AddWithValue("@userId", userId);
                likeCommand.ExecuteNonQuery();
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

        public void Unshare()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string unlikeQuery = "DELETE FROM shared_posts WHERE post_id = @postId AND user_id = @userId";
                SqlCommand unlikeCommand = new SqlCommand(unlikeQuery, myConnection);
                unlikeCommand.Parameters.AddWithValue("@postId", postId);
                unlikeCommand.Parameters.AddWithValue("@userId", userId);
                unlikeCommand.ExecuteNonQuery();
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

        public bool Create()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "INSERT INTO posts (title, description, post_date, user_id) " +
                                "VALUES (@title, @desc, @post_date, @user_id)";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@title", title);
                com.Parameters.AddWithValue("@desc", desc);
                com.Parameters.AddWithValue("@post_date", DateTime.Now.ToString("yyyy-MM-dd"));
                com.Parameters.AddWithValue("@user_id", userId);
                int i = com.ExecuteNonQuery();

                if (i > 0)
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

        public bool Update()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "UPDATE posts set title = @title, description = @desc, edited = 1 WHERE id_post = @id";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@title", title);
                com.Parameters.AddWithValue("@desc", desc);
                com.Parameters.AddWithValue("@id", id);
                int i = com.ExecuteNonQuery();

                if (i > 0)
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

        public bool Delete()
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
                        deleteShareCommand.Parameters.AddWithValue("@id", id);
                        deleteShareCommand.ExecuteNonQuery();

                        string deleteLikeQuery = "DELETE FROM liked_posts WHERE post_id = @id";
                        SqlCommand deleteLikeCommand = new SqlCommand(deleteLikeQuery, myConnection, transaction);
                        deleteLikeCommand.Parameters.AddWithValue("@id", id);
                        deleteLikeCommand.ExecuteNonQuery();

                        string deleteReportsQuery = "DELETE FROM reports WHERE post_id = @id";
                        SqlCommand deleteReportsCommand = new SqlCommand(deleteReportsQuery, myConnection, transaction);
                        deleteReportsCommand.Parameters.AddWithValue("@id", id);
                        deleteReportsCommand.ExecuteNonQuery();

                        string deleteCommentsQuery = "DELETE FROM comments WHERE post_id = @id";
                        SqlCommand deleteCommentsCommand = new SqlCommand(deleteCommentsQuery, myConnection, transaction);
                        deleteCommentsCommand.Parameters.AddWithValue("@id", id);
                        deleteCommentsCommand.ExecuteNonQuery();

                        string deletePostQuery = "DELETE FROM posts WHERE id_post = @id";
                        SqlCommand deletePostCommand = new SqlCommand(deletePostQuery, myConnection, transaction);
                        deletePostCommand.Parameters.AddWithValue("@id", id);
                        deletePostCommand.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
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
        }
    }
}