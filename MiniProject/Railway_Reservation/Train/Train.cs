using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    class Train
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public Train(int user)
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
        public void createTrain()
        {
            try
            {
                con = GetConnection();
                SqlCommand cmd1 = new SqlCommand("proc_createTrain");
                cmd1.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter train name: ");
                string train_name = Console.ReadLine();
                cmd1.Parameters.AddWithValue("@train_name", train_name);

                TrainType type = new TrainType(user);
                type.getAllTrainTypes();
                Console.Write("Enter the train type_id: ");
                int type_id = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@type_id", type_id);

                Console.Write("Enter Total kms: ");
                int totalKms = Convert.ToInt32(Console.ReadLine());
                cmd1.Parameters.AddWithValue("@totalKms", totalKms);

                Console.Write("Enter the source: ");
                string source = Console.ReadLine();
                cmd1.Parameters.AddWithValue("@source", source);

                Console.Write("Enter the destination: ");
                string destination = Console.ReadLine();
                cmd1.Parameters.AddWithValue("@destination", destination);

                cmd1.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd1.Parameters.Add(sp);

                cmd1.Connection = con;
                cmd1.ExecuteNonQuery();

                TrainClassCapacity trainClass = new TrainClassCapacity(user);
                trainClass.createTrainClassCapacity((int)sp.Value);
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
        public void deleteTrain()
        {
            try
            {
                viewAvailableTrains();
                Console.WriteLine();

                con = GetConnection();
                cmd = new SqlCommand("proc_deleteTrain", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter the train_no you want to delete: ");
                int train_no = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@train_no", train_no);
                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);

                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Train deleted successfully");
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
        public void viewAvailableTrains()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from dbo.fn_getAllTrains()", con);

                SqlDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("Train No |     Train Name     |   Train Type   |      Source     |   Destination   | totalKms");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["train_no"]}         {dr["train_name"]}        {dr["train_type"]}      {dr["source"]}       {dr["destination"]}           {dr["totalKms"]}");
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