CREATE DATABASE BankingDB;
USE BankingDb;

CREATE TABLE RegisterAccount
(
	Service_Reference_Number int primary key identity(10000,1),
	Title varchar(3),
	First_Name varchar(20) not null,
	Middle_Name varchar(20),
	Last_Name varchar(20) not null,
	Father_Name varchar(15) not null,
	Mobile_Number bigint not null,
	Email_Id varchar(30) not null,
	Aadhar varchar(12) not null,
	Gender varchar(6) not null,
	Date_Of_Birth date not null,
	Residential_Address varchar(100) not null,
	Permanent_Address varchar(100) not null,
	Occupation_Type varchar(50) not null,
	Source_Of_Income varchar(50) not null,
	Gross_Annual_Income float default 0,
	Opt_Debit_Card bit default 0,
	Opt_Net_Banking bit default 0
);

CREATE TABLE RejectedAccounts
(
	Service_Reference_Number int primary key,
	Email_Id varchar(30) not null,
	remarks varchar(100),
	rejected_at date default getdate(),
);

CREATE TABLE Customer
(
	Customer_Id int primary key identity(10000,1),
	Title varchar(3),
	First_Name varchar(20) not null,
	Middle_Name varchar(20),
	Last_Name varchar(20) not null,
	Father_Name varchar(15) not null,
	Mobile_Number bigint not null,
	Email_Id varchar(30) not null,
	Gender varchar(6) not null,
	Aadhar varchar(12) not null,
	created_at date default getdate(),
	Date_Of_Birth date not null,
	Residential_Address varchar(100) not null,
	Permanent_Address varchar(100) not null,
	Occupation_Type varchar(50) not null,
	Source_Of_Income varchar(50) not null,
	Gross_Annual_Income float,
);

CREATE TABLE Accounts
(
	Customer_Id int references Customer(Customer_Id),
	Account_Number int primary key identity(10000,1),
	created_at date default getdate(),
	Balance float default 1000,
);


CREATE TABLE Internet_Banking_Details
(
	Account_Number int references Accounts(Account_Number),
	Email_Id varchar(30) primary key,
	login_password varchar(100) not null,
	transaction_password varchar(100) not null,
	is_locked bit default 0,
	failed_attempts int default 0,
	locked_time datetime,
	last_login datetime
);

CREATE TABLE Debit_Card_Details
(
	Account_Number int references Accounts(Account_Number),
	Debit_Card_Number bigint primary key identity(4000000000000000, 1),
	Expiry_Date date not null
);


CREATE TABLE Transaction_Details
(
	Transaction_Id int primary key identity(10000,1),
	From_Account int,
	To_Account int,
	Transaction_Mode varchar(10),
	Transaction_Type varchar(6),
	Amount int check(Amount > 0),
	Balance int,
	Transaction_Date datetime default getdate(),
	Remarks varchar(50),
);

CREATE TABLE Admin_Table
(
	id int primary key identity(1,1),
	Name varchar(30) not null,
	Email_Id varchar(30) unique,
	password varchar(50) not null
);

create table Payees
(
Payee_Id int identity(1,1) primary key,
From_Account int references Accounts(Account_Number),
To_Account int references Accounts(Account_Number),
Beneficiary_Name varchar(50) not null,
created_at date default getdate(),
Nickname varchar(20)
);

CREATE TABLE SupportMessages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserEmail VARCHAR(100) NOT NULL,
    Subject VARCHAR(200) NOT NULL,
    Message TEXT NOT NULL,
    SentAt DATETIME NOT NULL DEFAULT GETDATE(),
    AdminReply TEXT NULL,
    RepliedAt DATETIME NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending'

);

create table OtpRequests
(
Id int identity(1,1) primary key, 
Email_Id varchar(30),
Otp int, 
created_time datetime default getdate(),
is_used bit default 0
);