using System;
using System.Data.SqlClient;
using System.Data;

namespace codeChallenge6
{
    class Program
    {
        static void Main(string[] args)
        {
            Questions questions = new Questions();
            Console.WriteLine("Insert employee question");
            questions.insertEmployee();
            Console.WriteLine("Updating employee salary question");
            questions.updateEmployeeSalary();
            Console.ReadLine();
        }

    }
    class Questions
    {
        static SqlConnection con = null;
        static SqlCommand cmd = null;
        static SqlConnection GetConnection()
        {
            con = new SqlConnection("Data Source=ICS-LT-60C4D64\\SQLEXPRESS;Initial Catalog=codechallenge;" +
                "user id = sa; password = Infinite@123;");
            con.Open();
            return con;
        }
        public void insertEmployee()
        {
            try
            {
                con = GetConnection();
                cmd = new SqlCommand("proc_insertEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                Console.WriteLine("Enter the Employee Name: ");
                string empName = Console.ReadLine();
                SqlParameter name = new SqlParameter();
                name.ParameterName = "@Name";
                name.Value = empName;
                name.DbType = DbType.String;
                name.Direction = ParameterDirection.Input;

                Console.WriteLine("Enter the Employee Salary: ");
                float empSalary = Convert.ToSingle(Console.ReadLine());
                SqlParameter salary = new SqlParameter();
                salary.ParameterName = "@Salary";
                salary.Value = empSalary;
                salary.DbType = DbType.Single;
                salary.Direction = ParameterDirection.Input;

                Console.WriteLine("Enter the Employee Gender: ");
                string empGender = Console.ReadLine();
                SqlParameter gender = new SqlParameter();
                gender.ParameterName = "@Gender";
                gender.Value = empGender;
                gender.DbType = DbType.String;
                gender.Direction = ParameterDirection.Input;

                SqlParameter empId = new SqlParameter();
                empId.ParameterName = "@Empid";
                empId.DbType = DbType.Int32;
                empId.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(name);
                cmd.Parameters.Add(salary);
                cmd.Parameters.Add(gender);
                cmd.Parameters.Add(empId);

                cmd.ExecuteNonQuery();
                con.Close();
                Console.WriteLine("The generated emp id is: " + empId.Value);
                try
                {
                    Console.WriteLine("Displaying all employee records: ");
                    con.Open();
                    cmd = new SqlCommand("select * from employee_details", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Console.WriteLine($"Empid: {dr["empid"]} Name: {dr["name"]} Salary: {dr["salary"]} Net_Salary: {dr["Net_Salary"]} Gender: {dr["Gender"]}");
                    }
                }
                catch(SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void updateEmployeeSalary()
        {
            try
            {
                Console.WriteLine("Enter the Employee Id: ");

                int empId = Convert.ToInt32(Console.ReadLine());
                con = GetConnection();
                cmd = new SqlCommand("updateEmployeeSalaryByEmpId", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter();
                id.ParameterName = "@EmpId";
                id.Value = empId;
                id.DbType = DbType.Int32;
                id.Direction = ParameterDirection.Input;
                
                SqlParameter salary = new SqlParameter();
                salary.ParameterName = "@Updated_Salary";
                salary.DbType = DbType.Single;
                salary.Direction = ParameterDirection.Output;


                cmd.Parameters.Add(id);
                cmd.Parameters.Add(salary);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"The updated salary for emp {empId}: {salary.Value}");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
