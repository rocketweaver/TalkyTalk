using ForumApp;
using System;
using System.Data;
using System.Data.SqlClient;
using TalkyTalk;

namespace AdminTalkyTalky.Models
{
    public class ChartAdminModel
    {
        SqlConnection myConnection = new SqlConnection();

        public DataTable ReadUsers()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT Username, COUNT(*) AS TotalUsers FROM users WHERE Level = 1 GROUP BY Username";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading users data: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }

        public DataTable ReadComments()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    u.username AS Username, 
                                    COUNT(*) AS TotalComments
                                FROM 
                                    comments c
                                INNER JOIN 
                                    users u ON c.user_id = u.id_user
                                GROUP BY 
                                    u.username";

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

        public DataTable ReadPosts()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    p.title AS Title, 
                                    p.like_count AS TotalLikes
                                FROM 
                                    posts p";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading posts data: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }

        public DataTable ReadBans()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    user_id, 
                                    COUNT(*) AS TotalBans
                                FROM 
                                    ban
                                GROUP BY 
                                    user_id";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading bans data: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }
    }
}
