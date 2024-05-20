using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TalkyTalk.Models.DefaultUsers
{
    public class UsersModel
    {
        SqlConnection myConnection = new SqlConnection();

        public static int UserId { get; private set; }
        public static string Username { get; private set; }
        public static string Email { get; private set; }
        public static int Level { get; private set; }

        public int id;
        public string username;
        public string email;
        public string password;
        public string pin;

        public bool SetUsernameAndEmail()
        {

            Username = username;
            Email = email;

            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email))
            {
                return true;
            }

            return false;
        }
        public bool Login()
        {

            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    query = "SELECT id_user, username, level FROM users WHERE email = @email";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@email", email);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        UserId = Convert.ToInt32(reader["id_user"]);
                        Username = reader["username"].ToString();
                        Email = email;
                        Level = Convert.ToInt32(reader["level"]);
                    }

                    reader.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public bool CheckPin()
        {

            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM users WHERE id_user = @id AND pin = @pin";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@pin", pin);

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public DataRow ReadById()
        {
            DataRow row = null;
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                if (id == null)
                {
                    return row;
                }

                string query = @"SELECT * FROM users WHERE id_user = @id";
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

        public bool Create()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "INSERT INTO Users (email, username, password, level, pin) " +
                                "VALUES (@email, @username, @password, @level, @pin)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@pin", pin);
                command.Parameters.AddWithValue("@level", 1);
                int i = command.ExecuteNonQuery();

                return i > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        public bool Update()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query;
                SqlCommand com;

                if (string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(pin))
                {
                    query = "UPDATE users set pin = @pin WHERE id_user = @id";
                    com = new SqlCommand(query, myConnection);
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@pin", pin);
                }
                else if (!string.IsNullOrEmpty(password) && string.IsNullOrEmpty(pin))
                {
                    query = "UPDATE users set password = @password WHERE id_user = @id";
                    com = new SqlCommand(query, myConnection);
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@password", password);
                }
                else
                {
                    query = "UPDATE users set username = @username, email = @email WHERE id_user = @id";
                    com = new SqlCommand(query, myConnection);
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@username", username);
                    com.Parameters.AddWithValue("@email", email);
                }

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
                        string deleteReportsQuery = "DELETE FROM reports WHERE " +
                            "(comment_id IN (SELECT id_comment FROM comments WHERE user_id = @id) OR " +
                            "post_id IN (SELECT id_post FROM posts WHERE user_id = @id))";
                        SqlCommand deleteReportsCommand = new SqlCommand(deleteReportsQuery, myConnection, transaction);
                        deleteReportsCommand.Parameters.AddWithValue("@id", id);
                        deleteReportsCommand.ExecuteNonQuery();

                        string deleteUserQuery = "DELETE FROM users WHERE id_user = @id";
                        SqlCommand deleteUsersCommand = new SqlCommand(deleteUserQuery, myConnection, transaction);
                        deleteUsersCommand.Parameters.AddWithValue("@id", id);
                        deleteUsersCommand.ExecuteNonQuery();

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);

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