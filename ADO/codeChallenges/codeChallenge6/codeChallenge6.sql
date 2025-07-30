/* Code Challenge 6 */

/*
1. Write a stored Procedure that inserts records in the Employee_Details table
 
The procedure should generate the EmpId automatically to insert and should return the generated value to the user
 
Also the Salary Column is a calculated column (Salary is givenSalary - 10%)
 
Table : Employee_Details (Empid, Name, Salary, Gender)
*/

create table Employee_Details
(
Empid int primary key,
Name varchar(30),
Salary float,
Net_Salary float,
Gender varchar(6)
);

create or alter procedure proc_insertEmployee @Name varchar(30), @Salary float, @Gender varchar(6)
as
begin
	declare @id int, @Net_Salary float;
	select @id = 
	case when max(empid) is null then 0
	else max(empid) end
	from Employee_Details
	set @id = @id + 1;
	set @Net_Salary = @Salary * 0.9;
	insert into Employee_Details values(@id, @Name, @Salary, @Net_Salary, @Gender);
	return @id;
end

declare @Name varchar(30), @Salary float, @Gender varchar(6), @Empid int;
set @Name = 'Hema Sai';
set @Salary = 10000;
set @Gender = 'Male';

exec @Empid = proc_insertEmployee @Name, @Salary, @Gender;
print 'Generated emp id is: ' + cast(@Empid as varchar(5));
select * from Employee_Details where Empid = @Empid

/*
2. Write a procedure that takes empid as input and outputs the updated salary as current salary + 100 for the give employee.
 
  Test the procedure using ADO classes and display the Employee details of that employee whose salary has been updated
*/

create or alter procedure updateEmployeeSalaryByEmpId @EmpId int, @Updated_Salary float output
as
begin
	declare @oldSalary int;
	select @oldSalary = salary from Employee_Details where Empid = @EmpId;
	set @Updated_Salary = @oldSalary + 100;
	select * from Employee_Details where Empid = @EmpId
	update Employee_Details set Salary = @Updated_Salary, Net_Salary = @Updated_Salary * 0.9 where Empid = @EmpId
	select * from Employee_Details where Empid = @EmpId
end


declare @Empid int, @Updated_Salary int;
set @EmpId = 1;
exec updateEmployeeSalaryByEmpId @Empid, @Updated_Salary;