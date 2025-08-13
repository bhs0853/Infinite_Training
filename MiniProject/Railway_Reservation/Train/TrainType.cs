using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class TrainType
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public TrainType(int user)
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
        public void createTrainType()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_createTrainType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter train type name: ");
                string type_name = Console.ReadLine();
                cmd.Parameters.AddWithValue("@type_name", type_name);

                Console.Write("Enter the base fare: ");
                float base_fare = Convert.ToSingle(Console.ReadLine());
                cmd.Parameters.AddWithValue("@base_fare", base_fare);

                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Creation of train type successful");
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
        public void deleteTrainType()
        {
            try
            {
                getAllTrainTypes();
                Console.WriteLine();

                con = GetConnection();
                cmd = new SqlCommand("proc_deleteTrainType", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter the train type_id you want to delete: ");
                int type_id = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@type_id", type_id);

                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Train type deleted successfully");
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
        public void getAllTrainTypes()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from train_type where isActive = 1", con);
                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine();
                Console.WriteLine("Type_id  |    Type_name    | Fare");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["type_id"]}            {dr["type_name"]}          {dr["base_fare"]}");
                }
                con.Close();
                Console.WriteLine();
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