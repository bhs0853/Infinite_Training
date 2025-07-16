CREATE DATABASE codechallenge;
use codechallenge;

CREATE TABLE books(
id int primary key,
title varchar(50),
author varchar(50),
isbn bigint unique,
published_date datetime
);

INSERT INTO books values (1, 'My First SQL Book','Mary Parker', 981483029127, '2012-02-22'),
				  (2, 'My Second SQL Book','John Mayer', 857300923713, '1972-07-03'),
				  (3, 'My Third SQL Book','Cary Flint', 523120967812, '1972-07-03');


-- q1: Write a query to fetch the details of the books written by author whose name ends with er.
select * from books where author like '%er';


CREATE TABLE reviews(
id int primary key,
bookid int,
reviewer_name varchar(50),
content varchar(50),
rating int,
published_date datetime,
foreign key(bookid) references books(id)
);

INSERT INTO reviews values (1, 1, 'John Smith','My First review', 4, '2017-12-10'),
				  (2, 2, 'John Smith','My Second review', 5, '2017-10-13'),
				  (3, 2, 'Alice Walker','Another review', 1, '2017-10-22');

-- q2: Display the Title ,Author and ReviewerName for all the books from the above table  

select Title, Author, Reviewer_name from books left join reviews on books.id = reviews.bookid;

-- q3: Display the  reviewer name who reviewed more than one book. 

select Reviewer_name from reviews
group by reviewer_name
having count(distinct(bookid)) > 1;

-----------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE customer(
id int primary key,
name varchar(50),
age int,
address varchar(50),
salary float
);

INSERT INTO customer values (1, 'Ramesh', 32, 'Ahmedabad', 2000.00),
				  (2, 'Khilan', 25, 'Delhi', 1500.00),
				  (3, 'Kaushik', 23,'Kota', 2000.00),
				  (4, 'Chaitali', 25,'Mumbai', 6500.00),
				  (5, 'Hardik', 27,'Bhopal', 8500.00),
				  (6, 'Komal', 22,'MP', 4500.00),
				  (7, 'Muffy', 24,'Indore', 10000.00);

-- q4: Display the Name for the customer from above customer table  who live in same address which has character o anywhere in address 
select name, address from customer where address like '%o%';


CREATE TABLE orders(
oid int primary key,
date datetime,
customer_id int,
amount int
foreign key(customer_id) references customer(id)
);

INSERT INTO orders values (102, '2009-10-08', 3, 3000),
						(100, '2009-10-08', 3, 1500),
						(101, '2009-11-20', 2, 1560),
						(103, '2008-05-20', 4, 2060);

-- q5: Write a query to display the   Date,Total no of customer  placed order on same Date  

select Date, count(distinct(customer_id)) 'Total no of customer' from orders
group by Date

-----------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Employee(
id int primary key,
name varchar(50),
age int,
address varchar(50),
salary float
);

INSERT INTO Employee values (1, 'Ramesh', 32, 'Ahmedabad', 2000.00),
				  (2, 'Khilan', 25, 'Delhi', 1500.00),
				  (3, 'Kaushik', 23,'Kota', 2000.00),
				  (4, 'Chaitali', 25,'Mumbai', 6500.00),
				  (5, 'Hardik', 27,'Bhopal', 8500.00),
				  (6, 'Komal', 22,'MP', null),
				  (7, 'Muffy', 24,'Indore', null);

-- q6: Display the Names of the Employee in lower case, whose salary is null  

select LOWER(name) 'Employee Name' from Employee where salary IS NULL;

-----------------------------------------------------------------------------------------------------------------------------------------

CREATE TABLE StudentDetails(
RegisterNo int primary key,
Name varchar(50),
Age int,
Qualification varchar(50),
Mobile bigint,
Mail_id varchar(20),
Location varchar(50),
Gender varchar(1)
);

INSERT INTO StudentDetails values (2, 'Sai', 22,'B.E', 9952836777, 'Sai@gmail.com', 'Chennai', 'M'),
							(3, 'Kumar', 20,'BSC', 7890125648, 'Kumar@gmail.com', 'Madurai', 'M'),
							(4, 'Selvi', 22,'B.Tech', 8904567342, 'selvi@gmail.com', 'Selam', 'F'),
							(5, 'Nisha', 25,'M.E', 7834672310, 'Nisha@gmail.com', 'Theni', 'F'),
							(6, 'SaiSaran', 21,'B.A', 7890345678, 'saran@gmail.com', 'Madurai', 'F'),
							(7, 'Tom', 23,'BCA', 8901234675, 'Tom@gmail.com', 'Pune', 'M');

-- q7: Write a sql server query to display the Gender,Total no of male and female from the above relation     

select Gender, count(*) 'COUNT' from StudentDetails
group by Gender;
