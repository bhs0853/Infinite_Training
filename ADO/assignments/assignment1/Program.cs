using System;
using System.Collections.Generic;
using System.Linq;

namespace assignment1
{
    class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public string City { get; set; }

        public Employee(int EmployeeID, string FirstName, string LastName, string Title, DateTime DOB, DateTime DOJ, string City)
        {
            this.EmployeeID = EmployeeID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Title = Title;
            this.DOB = DOB;
            this.DOJ = DOJ;
            this.City = City;
        }
        public void Display()
        {
            Console.WriteLine($"EmployeeID: {EmployeeID} \nFirstName: {FirstName} \nLastName: {LastName} \nTitle: {Title} \nDOB: {DOB} \nDOJ: {DOJ} \nCity: {City}");
        }
    }
    class Program
    {
        static void DisplayList(List<Employee> employeeList)
        {
            for (int i = 0; i < employeeList.Count; i++)
            {
                employeeList[i].Display();
                Console.WriteLine();
            }
        }
        static List<Employee> GetEmployees()
        {
            List<Employee> empList = new List<Employee>() {
                new Employee(1001, "Malcolm", "Daruwalla", "Manager",Convert.ToDateTime("16-11-1984"), Convert.ToDateTime("08-06-2011"), "Mumbai"),
                new Employee(1002, "Asdin", "Dhalla", "AsstManager",Convert.ToDateTime("20-08-1994"), Convert.ToDateTime("07-07-2012"), "Mumbai"),
                new Employee(1003, "Madhavi", "Oza", "Consultant",Convert.ToDateTime("14-11-1987"), Convert.ToDateTime("12-04-2015"), "Pune"),
                new Employee(1004, "Saba", "Shaikh", "SE",Convert.ToDateTime("03-06-1990"), Convert.ToDateTime("02-02-2016"), "Pune"),
                new Employee(1005, "Nazia", "Shaikh", "SE",Convert.ToDateTime("08-03-1991"), Convert.ToDateTime("02-02-2016"), "Mumbai"),
                new Employee(1006, "Amit", "Pathak", "Consultant",Convert.ToDateTime("07-11-1989"), Convert.ToDateTime("08-08-2014"), "Chennai"),
                new Employee(1007, "Vijay", "Natrajan", "Consultant",Convert.ToDateTime("02-12-1989"), Convert.ToDateTime("01-06-2015"), "Mumbai"),
                new Employee(1008, "Rahul", "Dubey", "Associate",Convert.ToDateTime("11-11-1993"), Convert.ToDateTime("06-11-2014"), "Chennai"),
                new Employee(1009, "Suresh", "Mistry", "Associate",Convert.ToDateTime("12-08-1992"), Convert.ToDateTime("03-12-2014"), "Chennai"),
                new Employee(1010, "Sumit", "Shah", "Manager",Convert.ToDateTime("12-04-1991"), Convert.ToDateTime("02-01-2016"), "Pune")
            };
            return empList;
        }
        static void Main(string[] args)
        {
            List<Employee> empList = GetEmployees();

            // 1. Display a list of all the employee who have joined before 1/1/2015
            var joinedBeforeList = empList.FindAll(e => DateTime.Compare(e.DOJ, Convert.ToDateTime("01-01-2015")) < 0);
            Console.WriteLine("Display a list of all the employee who have joined before 1/1/2015");
            DisplayList(joinedBeforeList);
            Console.WriteLine("**************************************************");

            // 2. Display a list of all the employee whose date of birth is after 1/1/1990
            var dobAfterList = empList.FindAll(e => DateTime.Compare(e.DOB, Convert.ToDateTime("01-01-1990")) > 0);
            Console.WriteLine("Display a list of all the employee whose date of birth is after 1/1/1990");
            DisplayList(dobAfterList);
            Console.WriteLine("**************************************************");

            // 3. Display a list of all the employee whose designation is Consultant and Associate
            var degList = empList.FindAll(e => e.Title == "Consultant" || e.Title == "Associate");
            Console.WriteLine("Display a list of all the employee whose designation is Consultant and Associate");
            DisplayList(degList);
            Console.WriteLine("**************************************************");

            // 4. Display total number of employees
            //Console.WriteLine("Display total number of employees");
            Console.WriteLine("Total no of Employees: " + empList.Count);
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 5. Display total number of employees belonging to “Chennai”
            int chennaiEmpList = empList.Count(e => e.City == "Chennai");
            Console.WriteLine("Total no of Employees belonging to Chennai: " + chennaiEmpList);
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 6. Display highest employee id from the list
            int highestEmpId = empList.Max(e => e.EmployeeID);
            Console.WriteLine("Highest employee id: " + highestEmpId);
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 7. Display total number of employee who have joined after 1/1/2015
            int joinedAfterList = empList.Count(e => DateTime.Compare(e.DOJ, Convert.ToDateTime("01-01-2015")) > 0);
            Console.WriteLine("Total number of employee who have joined after 1/1/2015: " + joinedAfterList);
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 8. Display total number of employee whose designation is not “Associate”
            int degNotList = empList.Count(e => e.Title != "Associate");
            Console.WriteLine("Total number of employee whose designation is not Associate: " + degNotList);
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 9. Display total number of employee based on City
            var groupByCity = empList.GroupBy(e => e.City);
            foreach (var city in groupByCity)
                Console.WriteLine($"Number of employees in {city.Key}: {city.Count()}");
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 10. Display total number of employee based on city and title
            foreach (var city in groupByCity)
            {
                var groupByTitle = city.GroupBy(c => c.Title);
                foreach (var title in groupByTitle)
                    Console.WriteLine($"The no of employees working as {title.Key} in {city.Key}: {title.Count()}");
            }
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            // 11. Display total number of employee who is youngest in the list
            DateTime youngest = empList.Max(e => e.DOB);
            Console.WriteLine($"Youngest DOB: {youngest} Total number of employee who is youngest in the list: {empList.Count(e => e.DOB == youngest)}");
            Console.WriteLine();
            Console.WriteLine("**************************************************");

            Console.ReadLine();
        }
    }
}
