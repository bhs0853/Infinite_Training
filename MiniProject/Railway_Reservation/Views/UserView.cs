using System;
using System.Data.SqlClient;

namespace Railway_Reservation
{
    class UserView
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        int user = -1;
        public UserView(int user)
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
        public void viewProfile()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("select * from dbo.fn_getUser(@user_id)", con);

                cmd.Parameters.AddWithValue("@user_id", user);
                SqlDataReader dr = cmd.ExecuteReader();
                Console.WriteLine("user_id |      name      |       email        |   phone  |    dob    |");
                Console.ForegroundColor = ConsoleColor.Green;
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["user_id"]}           {dr["name"]}        {dr["email"]}  {dr["phone"]}     {dr["dob"].ToString().Substring(0, 10)}");
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
        public void viewAvailableTrains()
        {
            Train train = new Train(user);
            train.viewAvailableTrains();
        }
        public void BookTicket()
        {
            Reservation reservation = new Reservation(user);
            reservation.createReservation();
        }
        public void CancelTicket()
        {
            Cancellation cancellation = new Cancellation(user);
            cancellation.createCancellation();
        }
        public void viewReservations()
        {
            Reservation reservation = new Reservation(user);
            reservation.viewReservations();
        }
        public void viewCancellations()
        {
            Cancellation cancellation = new Cancellation(user);
            cancellation.viewCancellations();
        }
    }
}