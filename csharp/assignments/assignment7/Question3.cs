using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment7
{
    /*
     * 3.	Create a list of employees with following property EmpId, EmpName, EmpCity, EmpSalary. Populate some data
            Write a program for following requirement
            a.	To display all employees data
            b.	To display all employees data whose salary is greater than 45000
            c.	To display all employees data who belong to Bangalore Region
            d.	To display all employees data by their names is Ascending order
     */
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpCity { get; set; }
        public int salary { get; set; }
        public Employee(int EmpId, string EmpName, string EmpCity, int salary)
        {
            this.EmpId = EmpId;
            this.EmpName = EmpName;
            this.EmpCity = EmpCity;
            this.salary = salary;
        }
        public void Display()
        {
            Console.WriteLine($"EmpId: {EmpId} EmpName: {EmpName} EmpCity: {EmpCity} salary: {salary}");
        }
    }
    class Question3
    {
        static void DisplayEmployees(List<Employee> employeeList)
        {
            foreach (Employee e in employeeList)
                e.Display();
        }
        static void Main(string[] args)
        {
            Console.Write("Enter no of employees: ");
            int n = Convert.ToInt32(Console.ReadLine());
            List<Employee> employeeList = new List<Employee>();
            for (int i = 0; i < n; i++)
            {
                try
                {
                    Console.Write("Enter the emp id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter the emp name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter the emp city: ");
                    string city = Console.ReadLine();
                    Console.Write("Enter the salary: ");
                    int salary = Convert.ToInt32(Console.ReadLine());
                    Employee newEmployee = new Employee(id, name, city, salary);
                    employeeList.Add(newEmployee);
                }
                catch (Exception e)
                {
                    i--;
                    Console.WriteLine(e.Message);
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("a. To display all employees data" +
                                      "\nb. To display all employees data whose salary is greater than desired minimum salary" +
                                      "\nc. To display all employees data who belong to Desired Region" +
                                      "\nd. To display all employees data by their names is Ascending order" +
                                      "\n any other key to exit"
                                      );
                    char ch = Convert.ToChar(Console.ReadLine());
                    if (ch == 'a')
                    {
                        DisplayEmployees(employeeList);
                    }
                    else if (ch == 'b')
                    {
                        Console.Write("Enter the minimum salary: ");
                        int minSalary = Convert.ToInt32(Console.ReadLine());
                        List<Employee> list = employeeList.FindAll(e => e.salary > minSalary);
                        DisplayEmployees(list);
                    }
                    else if (ch == 'c')
                    {
                        Console.Write("Enter the desired city: ");
                        string city = Console.ReadLine().ToLower();
                        List<Employee> list = employeeList.FindAll(e => e.EmpCity.ToLower() == city);
                        DisplayEmployees(list);
                    }
                    else if (ch == 'd')
                    {
                        List<Employee> list = employeeList.OrderBy(e => e.EmpName).ToList();
                        DisplayEmployees(list);
                    }
                    else
                        break;
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.Read();
        }
    }
}
