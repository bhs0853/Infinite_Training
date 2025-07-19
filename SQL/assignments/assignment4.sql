/* Assignment 4 */

-- 1.	Write a T-SQL Program to find the factorial of a given number.

create or alter procedure sp_factorial @num int
as
begin
	declare @factorial int = @num
	while(@num > 1)
	begin
		set @num = @num - 1;
		set @factorial = @num * @factorial;
	end
	return @factorial;
end

declare @factorial int, @num int = 5;
exec @factorial = sp_factorial @num;
print 'Factorial of ' + cast(@num as varchar(5)) + ' is: ' + cast(@factorial as varchar(10));

-- 2.	Create a stored procedure to generate multiplication table that accepts a number and generates up to a given number.

create or alter procedure sp_multable @num1 int, @num2 int
as
begin
	declare @counter int = 1;
	while(@counter <= @num2)
		begin
			select (cast(@num1 as varchar(9)) +' * '+  cast(@counter as varchar(9)) + ' = ' + cast((@num1 * @counter) as varchar(15))) as Mul_Table;
			set @counter = @counter + 1;
		end
end

exec sp_multable 3, 5

-- 3. Create a function to calculate the status of the student. If student score >=50 then pass, else fail. Display the data neatly student table

create table student
(
	Sid int primary key,
	Sname varchar(30)
);

create table Marks
(
Mid int primary key,
Sid int,
Score int,
foreign key(Sid) references student(Sid)
);

insert into student values(1, 'Jack'), (2, 'Rithvik'), (3, 'Jaspreeth'), (4, 'Praveen'), (5, 'Bisa'), (6, 'Suraj');

insert into Marks values(1, 1, 23), (2, 6, 95), (3, 4, 98), (4, 2, 17), (5, 3, 53), (6, 5, 13);

create or alter function fn_studentStatus(@score int)
returns varchar(10)
as
begin
	if(@score >= 50)
	begin
		return 'Pass';
	end
	return 'Fail';
end

select Sname 'Student Name', dbo.fn_studentStatus(Score) 'Status' from student s join marks m on s.Sid = m.Sid;