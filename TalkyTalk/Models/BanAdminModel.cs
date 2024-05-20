using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TalkyTalk;

namespace ForumApp.Models
{
    public class BanAdminModel
    {
        SqlConnection myConnection = new SqlConnection();

        public DataTable ReadBans()
        {
            DataTable dataTable = new DataTable();
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    b.id_ban, 
                                    b.user_id,
                                    u.username,
                                    b.start_date, 
                                    b.end_date
                                FROM 
                                    ban b
                                INNER JOIN 
                                    users u ON b.user_id = u.id_user";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, myConnection);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading bans: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }

            return dataTable;
        }

        public void CreateBan(int userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();
                string query;
                SqlCommand command;

                if (startDate != null && endDate != null)
                {
                    query = @"INSERT INTO ban (user_id, start_date, end_date) 
                                 VALUES (@user_id, @start_date, @end_date)";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@start_date", startDate);
                    command.Parameters.AddWithValue("@end_date", endDate);

                } else
                {
                    query = @"INSERT INTO ban (user_id) 
                                 VALUES (@user_id)";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@user_id", userId);
                }

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    throw new Exception("Failed to create ban.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating ban: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public void UpdateBan(int banId, DateTime newStartDate, DateTime newEndDate)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"UPDATE ban SET start_date = @newStartDate, end_date = @newEndDate 
                         WHERE id_ban = @banId";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@newStartDate", newStartDate);
                command.Parameters.AddWithValue("@newEndDate", newEndDate);
                command.Parameters.AddWithValue("@banId", banId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    throw new Exception("Failed to update ban.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating ban: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }

        public ArrayList GetDataById(int banId)
        {
            ArrayList data = new ArrayList();
            SqlDataReader dr = null;
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = @"SELECT 
                                    start_date, 
                                    end_date
                                 FROM 
                                    ban 
                                 WHERE
                                    id_ban = @banId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@banId", banId);

                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    data.Add(dr["start_date"].ToString());
                    data.Add(dr["end_date"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                dr = null;
                throw new Exception($"Error getting ban data: {ex.Message}");
            }
            finally
            {
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            return data;
        }

        public void DeleteBan(int banId)
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string deleteBanQuery = "DELETE FROM ban WHERE id_ban = @banId";
                SqlCommand deleteBanCommand = new SqlCommand(deleteBanQuery, myConnection);
                deleteBanCommand.Parameters.AddWithValue("@banId", banId);
                deleteBanCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting ban: {ex.Message}");
            }
            finally
            {
                myConnection.Close();
            }
        }
    }
}
