CREATE TABLE Clients
(
Client_ID int primary key,
CName varchar(40) not null,
Address varchar(30),
Email varchar(30) unique,
Phone bigint,
Business varchar(20) not null
);

CREATE TABLE Departments
(
Deptno int primary key,
Dname varchar(15),
Loc varchar(20)
);

CREATE TABLE Employees
(
Empno int primary key,
EName varchar(20) not null,
Job varchar(15),
Salary int check(salary > 0),
DeptNo int,
foreign key(DeptNo) references Departments(Deptno)
);

CREATE TABLE Projects
(
Project_ID int primary key,
Descr varchar(30) not null,
Start_Date datetime,
Planned_End_Date datetime,
Actual_End_date datetime,
Budget int check(Budget > 0),
Client_ID int,
check(Actual_End_date > Planned_End_Date),
foreign key(Client_ID) references Clients(Client_ID)
);

CREATE TABLE EmpProjectTasks
(
Project_ID int,
Empno int,
Start_Date datetime,
End_Date datetime,
Task varchar(25) not null,
Status varchar(15) not null,
foreign key(Project_ID) references Projects(Project_ID),
foreign key(Empno) references Employees(Empno),
primary key(Project_ID, Empno)
);

insert into Clients values(1001, 'ACME Utilities', 'Noida', 'contact@acmeutil.com', 9567880032, 'Manufacturing'),
							(1002, 'Trackon Consultants', 'Mumbai', 'consult@trackon.com', 8734210090, 'Consultant'),
							(1003, 'MoneySaver Distributors', 'Kolkata', 'save@moneysaver.com', 7799886655, 'Reseller'),
							(1004, 'Lawful Corp', 'Chennai', 'justice@lawful.com', 9210342219, 'Professional');

insert into Departments values (10, 'Design', 'Pune'),
								(20, 'Development', 'Pune'),
								(30, 'Testing', 'Mumbai'),
								(40, 'Document', 'Mumbai');

insert into Employees values(7001, 'Sandeep', 'Analyst', 25000, 10),
							(7002, 'Rajesh', 'Designer', 30000, 10),
							(7003, 'Madhav', 'Developer', 40000, 20),
							(7004, 'Manoj', 'Developer', 40000, 20),
							(7005, 'Abhay', 'Designer', 35000, 10),
							(7006, 'Uma', 'Tester', 30000, 30),
							(7007, 'Gita', 'Tech. Writer', 30000, 40),
							(7008, 'Priya', 'Tester', 35000, 30),
							(7009, 'Nutan', 'Developer', 45000, 20),
							(7010, 'Smita', 'Analyst', 20000, 10),
							(7011, 'Anand', 'Project Mgr', 65000, 10);

insert into Projects values (401, 'Inventory', '01-Apr-11', '01-Oct-11', '31-Oct-11', 150000, 1001),
							(402, 'Accounting', '01-Aug-11', '01-Jan-12', null, 500000, 1002),
							(403, 'Payroll', '01-Oct-11', '31-Dec-11', null, 75000, 1003),
							(404, 'Contact Mgmt', '01-Nov-11', '31-Dec-11', null, 50000, 1004);

insert into EmpProjectTasks values (401, 7001, '01-Apr-11', '20-Apr-11', 'System Analysis', 'Completed'),
									(401, 7002, '21-Apr-11', '30-May-11', 'System Design', 'Completed'),
									(401, 7003, '01-Jun-11', '15-Jul-11', 'Coding', 'Completed'),
									(401, 7004, '18-Jul-11', '01-Sep-11', 'Coding', 'Completed'),
									(401, 7006, '03-Sep-11', '15-Sep-11', 'Testing', 'Completed'),
									(401, 7009, '18-Sep-11', '05-Oct-11', 'Code Change', 'Completed'),
									(401, 7008, '06-Oct-11', '16-Oct-11', 'Testing', 'Completed'),
									(401, 7007, '06-Oct-11', '22-Oct-11', 'Documentation', 'Completed'),
									(401, 7011, '22-Oct-11', '31-Oct-11', 'Sign Off', 'Completed'),
									(402, 7010, '01-Aug-11', '20-Aug-11', 'System Analysis', 'Completed'),
									(402, 7002, '22-Aug-11', '30-Sep-11', 'System Design', 'Completed'),
									(402, 7004, '01-Oct-11', null, 'Coding', 'In Progess');