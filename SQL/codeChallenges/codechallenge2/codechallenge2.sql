/* Code challenge 5 (sql) */

-- 1.	Write a query to display your birthday( day of week)
select DATENAME(WEEKDAY, '2003-05-08') as 'Day of week';

-- 2.	Write a query to display your age in days
select DATEDIFF(DAY, '2003-05-08', GETDATE()) as 'Age in days';

-- 3.	Write a query to display all employees information those who joined before 5 years in the current month
--(Hint : If required update some HireDates in your EMP table of the assignment)

update emp set hire_date = '20-JUL-19' where empno in (7788, 7876)
select * from emp where DATEDIFF(YEAR, emp.hire_date, GETDATE()) > 5 and month(emp.hire_date) = month(GETDATE());

-- 4.	Create table Employee with empno, ename, sal, doj columns or use your emp table and perform the following operations in a single transaction
--	a. First insert 3 rows 
--	b. Update the second row sal with 15% increment  
--  c. Delete first row.
-- After completing above all actions, recall the deleted row without losing increment of second row.

begin transaction

	-- a. First insert 3 rows 
	insert into emp values(7991, 'AName', 'Operations manager', 7566, '18-APR-86', 7000, NULL, 40),
	(7992, 'BName', 'executive', 7566, '20-APR-87', 6500, NULL, 40),
	(7993, 'CName', 'executive', 7566, '22-APR-87', 6200, NULL, 40)
	
	-- data after insertion
	select * from emp where empno > 7990
	--	b. Update the second row sal with 15% increment  
	update emp set salary = salary * 1.15 where empno = 7992

	-- data after updating the salary
	select * from emp where empno > 7990
	save transaction t1

	--  c. Delete first row.
	delete from emp where empno = 7991
	-- data after deleting the first row.
	select * from emp where empno > 7990

	rollback transaction t1
	commit
	-- final data after rolling back
	select * from emp where empno > 7990
set implicit_transactions on

-- 5.      Create a user defined function calculate Bonus for all employees of a  given dept using 	following conditions
--  a.     For Deptno 10 employees 15% of sal as bonus.
--	b.     For Deptno 20 employees  20% of sal as bonus
--  c.     For Others employees 5%of sal as bonus

create or alter function fn_calculatebonus(@deptno int)
returns @bonustable table(empno int, ename varchar(30), salary int, bonus_amount float, deptno int)
as
begin
	if(@deptno = 10)
	begin
		insert into @bonustable
		select empno, ename, salary, (salary * 0.15) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	else if(@deptno = 20)
		begin
		insert into @bonustable
		select empno, ename, salary, (salary * 0.2) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	else
		begin
		insert into @bonustable
		select empno, ename, salary, (salary * 0.05) 'Bonus amount', deptno from emp where deptno = @deptno;
	end
	return;
end

select * from dbo.fn_calculatebonus(10);
select * from dbo.fn_calculatebonus(20);
select * from dbo.fn_calculatebonus(30);

-- 6. Create a procedure to update the salary of employee by 500 whose dept name is Sales and current salary is below 1500 (use emp table)
 
 create or alter procedure proc_updateSalary
 as
 begin
	select * from emp where deptno = (select deptno from dept where dname = 'SALES') and salary < 1500
	update emp set salary = salary + 500 where deptno = (select deptno from dept where dname = 'SALES') and salary < 1500
	select * from emp where deptno = (select deptno from dept where dname = 'SALES')
end

exec proc_updateSalary;
