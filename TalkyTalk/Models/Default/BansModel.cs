using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TalkyTalk.Models.Default
{
    public class BansModel
    {
        SqlConnection myConnection = new SqlConnection();

        public int BanId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CheckBan()
        {
            try
            {
                myConnection.ConnectionString = GlobalVariable.connString;
                myConnection.Open();

                string query = "SELECT start_date, end_date FROM ban WHERE user_id = @userId";
                SqlCommand com = new SqlCommand(query, myConnection);
                com.Parameters.AddWithValue("@userId", UserId);

                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    StartDate = ((DateTime)reader["start_date"]).Date;
                    EndDate = ((DateTime)reader["end_date"]).Date;

                    DateTime CurrentDate = DateTime.Today;

                    if (CurrentDate >= StartDate && CurrentDate <= EndDate)
                    {
                        TimeSpan remainingDays = EndDate - CurrentDate;
                        return remainingDays.Days;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
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

    }
}