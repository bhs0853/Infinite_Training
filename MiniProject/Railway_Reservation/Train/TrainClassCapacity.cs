using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class TrainClassCapacity
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public TrainClassCapacity(int user)
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
        public void createTrainClassCapacity(int train_no)
        {
            Console.WriteLine("Creating seats per class for " + train_no);
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_createTrainClassCapacity", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter total seats per class: ");
                int total_seats = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@total_seats", total_seats);

                cmd.Parameters.AddWithValue("@train_no", train_no);
                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Train added successfully");
            }
            catch (Exception e)
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
        public void getTrainClassCapacity(int train_no)
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from dbo.fn_getSeatsPerClassByTrain(@train_no)", con);
                cmd.Parameters.AddWithValue("@train_no", train_no);

                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("capacity_id | class_name | available_seats |   fare     |");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {

                    Console.WriteLine($"   {dr["capacity_id"]}          {dr["class_name"]}        {dr["available_seats"]}              {Math.Ceiling(Convert.ToSingle(dr["fare"]))}");
                }
            }
            catch (Exception e)
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