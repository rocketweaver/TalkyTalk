using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using TalkyTalk;

namespace ForumApp
{
    public class UsersView
    {
        SqlConnection myConnection = new SqlConnection();
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }

        public string Create()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "INSERT INTO users (Username, Email, Password, Level) VALUES (@Username, @Email, @Password, @Level)";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Level", Level);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Data berhasil dimasukkan";
                    }
                    else
                    {
                        return "Gagal memasukkan data. Tidak ada baris yang terpengaruh.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
            finally
            {
                myConnection.Close();
            }
        }


        public DataSet Read()
        {
            DataSet ds = new DataSet();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT * FROM users WHERE Level = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, myConnection);
                da.Fill(ds, "users");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            return ds;
        }

        public int GetTotalUsersCount()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(id_user) AS TotalUsers FROM users";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }


        public string Update()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "UPDATE users SET Username = @Username, Email = @Email, Password = @Password, Level = @Level WHERE id_user = @UserId AND Level = 1";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Level", Level);
                    command.Parameters.AddWithValue("@UserId", UserId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Data has been updated.";
                    }
                    else
                    {
                        return "Failed to update the data.";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void ReadById()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT * FROM users WHERE id_user = @UserId";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Ambil data dari database dan set properti objek
                        Username = reader["Username"].ToString();
                        Email = reader["Email"].ToString();
                        Password = reader["Password"].ToString();
                        Level = Convert.ToInt32(reader["Level"]);
                    }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }


        /*public string Delete(int userId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "DELETE FROM users WHERE id_user = @UserId AND Level = 1";
                using (SqlCommand command = new SqlCommand(query, myConnection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Data berhasil dihapus";
                    }
                    else
                    {
                        return "Data tidak ditemukan atau gagal dihapus";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }*/

        public string Delete(int userId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                using (SqlTransaction transaction = myConnection.BeginTransaction())
                {
                    try
                    {
                        string deleteReportsQuery = "DELETE FROM reports WHERE " +
                            "(comment_id IN (SELECT id_comment FROM comments WHERE user_id = @id) OR " +
                            "post_id IN (SELECT id_post FROM posts WHERE user_id = @id))";
                        SqlCommand deleteReportsCommand = new SqlCommand(deleteReportsQuery, myConnection, transaction);
                        deleteReportsCommand.Parameters.AddWithValue("@id", userId);
                        deleteReportsCommand.ExecuteNonQuery();

                        string deleteUserQuery = "DELETE FROM users WHERE id_user = @id AND Level = 1";
                        SqlCommand deleteUsersCommand = new SqlCommand(deleteUserQuery, myConnection, transaction);
                        deleteUsersCommand.Parameters.AddWithValue("@id", userId);
                        deleteUsersCommand.ExecuteNonQuery();

                        transaction.Commit();

                        return "Data successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();;

                        return "Data is failed to be deleted.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return "Data is failed to be deleted.";
            }
            finally
            {
                myConnection.Close();
            }
        }

    }
}
