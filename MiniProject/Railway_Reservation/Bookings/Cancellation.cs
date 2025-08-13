using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class Cancellation
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public Cancellation(int user)
        {
            this.user = user;
        }
        static SqlConnection GetConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-60C4D64\\SQLEXPRESS;Initial Catalog= MiniProject;" +
                "user id = sa; password = Infinite@123;");
            con.Open();
            return con;
        }

        public void viewCancellations()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from dbo.fn_getCancellations(@user_id)", con);
                cmd.Parameters.AddWithValue("@user_id", user);
                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("user id | cancellation id | reservation id | train no | refund amount |       status        | cancelled passenger | cancellation date");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"   {dr["user_id"]}        {dr["cancellation_id"]}               {dr["reservation_id"]}         {dr["train_no"]}         {Math.Ceiling(Convert.ToSingle(dr["refund_amount"]))}         {dr["refund_status"]}          {dr["cancelled_passenger_count"]}                 {dr["cancellation_date"].ToString().Substring(0, 10)}");
                }
            }
            catch (SqlException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ResetColor();
                con.Close();
            }
        }

        public void createCancellation()
        {
            try
            {
                con = GetConnection();
                SqlCommand cmd1 = new SqlCommand("proc_createCancellation", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                Reservation reservation = new Reservation(user);
                reservation.viewReservations();
                Console.Write("Enter the reservation id you want to cancel: ");
                int reservation_id = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@reservation_id", reservation_id);

                Console.Write("Enter the passenger count you want to cancel: ");
                int cancelled_passenger_count = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@cancelled_passenger_count", cancelled_passenger_count);
                cmd1.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd1.Parameters.Add(sp);

                cmd1.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ticket cancelled successfully");
            }
            catch (SqlException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ResetColor();
                con.Close();
            }
        }
    }
}