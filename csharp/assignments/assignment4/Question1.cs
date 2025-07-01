using System;
using System.Collections.Generic;

namespace assignment4
{
    /*
     * Scenario: Employee Management System (Console-Based)
    -----------------------------------------------------
    You are tasked with developing a simple console-based Employee Management System using C# that allows users to manage a list of employees. Use a menu-driven approach to perform CRUD operations on a List<Employee>.

    Each Employee has the following properties:

    int Id

    string Name

    string Department

    double Salary

    Functional Requirements
    Design a menu that repeatedly prompts the user to choose one of the following actions:

    ===== Employee Management Menu =====
    1. Add New Employee
    2. View All Employees
    3. Search Employee by ID
    4. Update Employee Details
    5. Delete Employee
    6. Exit
    ====================================
     */
    class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Department { get; set; }

        public double Salary { get; set; }

        public Employee(int Id, string Name, string Department, double Salary)
        {
            this.Id = Id;
            this.Name = Name;
            this.Department = Department;
            this.Salary = Salary;
        }
    }

    class EmployeeManager
    {
        List<Employee> employeeList = new List<Employee>();
        public void AddEmployee(Employee e)
        {
            employeeList.Add(e);
            Console.WriteLine("Added Employee successfully");
        }
        public void ViewAllEmployees()
        {
            Console.WriteLine("***** Data of all employees *****");
            Console.WriteLine(employeeList.Count == 0 ? "No Data" : "");
            for (int i = 0; i < employeeList.Count; i++)
            {
                Employee e = employeeList[i];
                Console.WriteLine($"employee-> id: {e.Id} Name: {e.Name} Department: {e.Department} Salary: {e.Salary} ");
            }
        }
        public void SearchEmployeeById(int id)
        {
            int i = 0;
            for (; i < employeeList.Count; i++)
            {
                Employee e = employeeList[i];
                if (e.Id == id)
                {
                    Console.WriteLine($"employee-> id: {e.Id} Name: {e.Name} Department: {e.Department} Salary: {e.Salary} ");
                    return;
                }
            }
            Console.WriteLine($"Employee with id {id} not found !!!");
        }
        public void UpdateEmployeeById(int id, Dictionary<String, String> d)
        {
            int i = 0;
            for (; i < employeeList.Count; i++)
            {
                Employee e = employeeList[i];
                if (e.Id == id)
                {
                    foreach (var item in d)
                    {
                        if (item.Key == "name")
                            e.Name = item.Value;
                        else if (item.Key == "department")
                            e.Department = item.Value;
                        else if (item.Key == "salary")
                            e.Salary = Convert.ToInt32(item.Value);
                    }
                    Console.WriteLine($"updated employee-> id: {e.Id} Name: {e.Name} Department: {e.Department} Salary: {e.Salary}");
                    return;
                }
            }
            Console.WriteLine($"Employee with id {id} not found  !!!");
        }
        public void DeleteEmployeeById(int id)
        {
            int i = 0;
            for (; i < employeeList.Count; i++)
            {
                Employee e = employeeList[i];
                if (e.Id == id)
                {
                    employeeList.RemoveAt(i);
                    Console.WriteLine($"Employee with id {id} deleted.");

                }
            }
            Console.WriteLine($"Employee with id {id} not found  !!!");
        }
    }

    class Question1
    {
        static void Main(string[] args)
        {
            EmployeeManager manager = new EmployeeManager();
            int i = 0;
            do
            {
                try
                {
                    Console.WriteLine("***** Employee Management Menu *****");
                    Console.WriteLine("1.Add New Employee");
                    Console.WriteLine("2.View All Employees");
                    Console.WriteLine("3.Search Employee by ID");
                    Console.WriteLine("4.Update Employee Details");
                    Console.WriteLine("5.Delete Employee");
                    Console.WriteLine("6.Exit");
                    Console.Write("Choose the action from the menu: ");
                    i = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();
                    if (i == 1)
                    {
                        Console.WriteLine("Please enter employee details");
                        Console.WriteLine("Enter the employee id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the employee name: ");
                        String name = Console.ReadLine();
                        Console.WriteLine("Enter the employee department: ");
                        String department = Console.ReadLine();
                        Console.WriteLine("Enter the employee salary: ");
                        double salary = double.Parse(Console.ReadLine());
                        Employee newEmployee = new Employee(id, name, department, salary);
                        manager.AddEmployee(newEmployee);
                        Console.WriteLine();
                    }
                    if (i == 2)
                    {
                        manager.ViewAllEmployees();
                        Console.WriteLine();
                    }
                    if (i == 3)
                    {
                        Console.WriteLine("Enter the employee id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        manager.SearchEmployeeById(id);
                        Console.WriteLine();
                    }
                    if (i == 4)
                    {
                        Dictionary<string, string> d = new Dictionary<string, string>();
                        Console.WriteLine("Enter the employee id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter the employee name: ");
                        string name = Console.ReadLine();
                        d.Add("name", name);
                        Console.WriteLine("Enter the employee department: ");
                        string department = Console.ReadLine();
                        d.Add("department", department);
                        Console.WriteLine("Enter the employee salary: ");
                        string salary = Console.ReadLine();
                        d.Add("salary", salary);
                        manager.UpdateEmployeeById(id, d);
                        Console.WriteLine();
                    }
                    if (i == 5)
                    {
                        Console.WriteLine("Enter the employee id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        manager.DeleteEmployeeById(id);
                        Console.WriteLine();
                    }
                    if (i > 5)
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine($"######### {e.Message} ############");
                    Console.WriteLine();
                }

            } while (i < 6);
            Console.WriteLine("********** Exit **********");
            Console.Read();
        }
    }
}
