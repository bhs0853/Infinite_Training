using System;
using System.Data.SqlClient;
using System.Data;

namespace Railway_Reservation
{
    public class UserAuth
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        static int user = -1;
        static SqlConnection GetConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-60C4D64\\SQLEXPRESS;Initial Catalog= MiniProject;" +
                "user id = sa; password = Infinite@123;");
            con.Open();
            return con;
        }
        public (int user, string role) signIn()
        {
            int user = -1;
            string role = "";
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from fn_userLogin(@email, @password)", con);

                Console.Write("Enter your email: ");
                string email = Console.ReadLine();
                cmd.Parameters.AddWithValue("@email", email);

                Console.Write("Enter your password: ");
                string password = Console.ReadLine();
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    user = (int)dr["user_id"];
                    role = (string)dr["role"];
                }
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return (user, role);
        }
        public void signUp()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_createUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                cmd.Parameters.AddWithValue("@Name", name);

                Console.Write("Enter your email: ");
                string email = Console.ReadLine();
                cmd.Parameters.AddWithValue("@email", email);

                Console.Write("Enter desired password: ");
                string password = Console.ReadLine();
                cmd.Parameters.AddWithValue("@password", password);

                Console.Write("Enter your phone: ");
                int phone = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@phone", phone);

                Console.Write("Enter your dob: ");
                string dob = Console.ReadLine();
                cmd.Parameters.AddWithValue("@dob", dob);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);
                cmd.ExecuteNonQuery();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("User creation successful");
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
        public void adminSignUp(int user)
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_createAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.Write("Enter your name: ");
                string name = Console.ReadLine();
                cmd.Parameters.AddWithValue("@Name", name);

                Console.Write("Enter your email: ");
                string email = Console.ReadLine();
                cmd.Parameters.AddWithValue("@email", email);

                Console.Write("Enter desired password: ");
                string password = Console.ReadLine();
                cmd.Parameters.AddWithValue("@password", password);

                Console.Write("Enter your phone: ");
                int phone = Convert.ToInt32(Console.ReadLine());
                cmd.Parameters.AddWithValue("@phone", phone);

                Console.Write("Enter your dob: ");
                string dob = Console.ReadLine();
                cmd.Parameters.AddWithValue("@dob", dob);

                cmd.Parameters.AddWithValue("@user_id", user);

                SqlParameter sp = new SqlParameter();
                sp.SqlDbType = SqlDbType.Int;
                sp.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(sp);
                cmd.ExecuteNonQuery();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Admin creation successful");
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