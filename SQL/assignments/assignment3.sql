/* Assignment 3 */

-- 1. Retrieve a list of MANAGERS.
select * from emp where empno in (select distinct mgr_id from emp);

-- 2. Find out the names and salaries of all employees earning more than 1000 per month
select Ename, Salary from emp where salary > 1000;

-- 3. Display the names and salaries of all employees except JAMES.
select Ename, Salary from emp where ename != 'JAMES';

-- 4. Find out the details of employees whose names begin with ‘S’. 
select * from emp where ename like 'S%';

-- 5. Find out the names of all employees that have ‘A’ anywhere in their name. 
select * from emp where ename like '%A%';

-- 6. Find out the names of all employees that have ‘L’ as their third character in their name
select * from emp where ename like '__L%';

-- 7. Compute daily salary of JONES.
select Ename, (salary / 30) 'Daily Salary' from emp where ename  = 'JONES';

-- 8. Calculate the total monthly salary of all employees. 
select sum(salary) 'Total monthly salary' from emp;

-- 9. Print the average annual salary . 
select (avg(salary) * 12) 'Average annual salary' from emp;

-- 10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30. 
select Ename, Job, Salary, DeptNo from emp where job != 'SALESMAN' or deptno != 30;

-- 11. List unique departments of the EMP table. 
select distinct(Deptno) from emp;

-- 12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
select ename 'Employee', salary 'Monthly Salary' from emp where salary > 1500 and deptno in (10, 30);

-- 13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000. 
select Ename, Job, Salary from emp where job in ('Manager', 'ANALYST') and salary not in(1000, 3000, 5000);

-- 14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%. 
select Ename, Job, Comm from emp where comm > (salary * 1.1);

-- 15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782. 
select Ename from emp where ename like '%L%L%' and (deptno = 30 or mgr_id = 7782);

-- 16. Display the names of employees with experience of over 30 years and under 40 yrs.  Count the total number of employees. 
select Ename from emp where DATEDIFF(YEAR, hire_date, GETDATE()) > 30 and DATEDIFF(YEAR, hire_date, GETDATE()) < 40;
select count (*) 'No Of Employees exp > 30 && exp < 40' from emp where empno in (select empno from emp where DATEDIFF(YEAR, hire_date, GETDATE()) > 30 and DATEDIFF(YEAR, hire_date, GETDATE()) < 40);

-- 17. Retrieve the names of departments in ascending order and their employees in descending order. 
select * from emp e join dept d on e.deptno = d.deptno order by d.dname, e.empno desc

-- 18. Find out experience of MILLER. 
select (cast(DATEDIFF(YEAR, hire_date, GETDATE()) as varchar(5)) + ' years')  'Experience of Miller' from emp where ename = 'MILLER';