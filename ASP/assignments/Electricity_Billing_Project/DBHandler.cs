using System.Configuration;
using System.Data.SqlClient;

namespace Electricity_Billing_Project
{
    public class DBHandler
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
            con.Open();
            return con;
        }
    }
}