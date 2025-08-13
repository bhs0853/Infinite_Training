using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class Reservation
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public Reservation(int user)
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
        public void createReservation()
        {
            try
            {
                con = GetConnection();
                SqlCommand cmd1 = new SqlCommand("proc_createReservation", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                Train train = new Train(user);
                train.viewAvailableTrains();
                Console.Write("Enter the train no you want to book: ");
                int train_no = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@train_no", train_no);

                TrainClassCapacity classCapacity = new TrainClassCapacity(user);
                classCapacity.getTrainClassCapacity(train_no);
                Console.Write("Enter the class id you want to book: ");
                int class_id = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@class_id", class_id);

                Console.Write("Enter the passenger count: ");
                int passenger_count = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@passenger_count", passenger_count);

                Console.Write("Enter the journey date: ");
                string journey_date = Console.ReadLine();
                cmd1.Parameters.AddWithValue("@journey_date", journey_date);
                cmd1.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd1.Parameters.Add(sp);

                cmd1.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ticket booked successfully");
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
        public void viewReservations()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from dbo.fn_getReservations(@user_id)", con);
                cmd.Parameters.AddWithValue("@user_id", user);

                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("user_id | reservation_id | train_no | status | class_name | Total Kms | Fare | Passengers | booking_date | journey_date | isActive");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"   {dr["user_id"]}         {dr["reservation_id"]}        {dr["train_no"]}       {dr["reservation_status"]}      {dr["class_name"]}       {dr["total_kms"]}         {dr["total_fare"]}           {dr["passenger_count"]}         {dr["booking_date"].ToString().Substring(0, 10)}       {dr["journey_date"].ToString().Substring(0, 10)}  {dr["isActive"]}");
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
    }
}