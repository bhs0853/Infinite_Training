using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class TrainClass
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;

        public TrainClass(int user)
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

        public void createTrainClass()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_createTrainClass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter train class name: ");
                string class_name = Console.ReadLine();
                cmd.Parameters.AddWithValue("@class_name", class_name);

                Console.Write("Enter the fare multiplier: ");
                float fare_multiplier = Convert.ToSingle(Console.ReadLine());
                cmd.Parameters.AddWithValue("@fare_multiplier", fare_multiplier);

                Console.Write("Enter the refund multiplier: ");
                float refund_multiplier = Convert.ToSingle(Console.ReadLine());
                cmd.Parameters.AddWithValue("@refund_multiplier", refund_multiplier);

                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Creation of train class successful");
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
        public void deleteTrainClass()
        {
            try
            {
                getAllTrainClasses();
                Console.WriteLine();

                Console.Write("Enter the train class_id you want to delete: ");
                int class_id = Convert.ToInt32(Console.ReadLine());
                con = GetConnection();
                cmd = new SqlCommand("proc_deleteTrainClass", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@class_id", class_id);
                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Train class deleted successfully");
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
        public void getAllTrainClasses()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from class where isActive = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("Class_id  |    Class_name    | Fare_multiplier | Refund_multiplier");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["class_id"]}              {dr["class_name"]}              {dr["fare_multiplier"]}                     {dr["refund_multiplier"]}");
                }
                con.Close();
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