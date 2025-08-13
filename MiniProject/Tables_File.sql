CREATE DATABASE MiniProject;
drop database MiniProject
USE MiniProject;

CREATE TABLE user_role
(
role_id int primary key identity(1,1),
role_name varchar(10)
);

drop table user_role

CREATE TABLE user_table
(
user_id int primary key identity(100,1),
name varchar(30),
email varchar(30),
password varchar(10),
phone bigint,
dob date,
created_at date default getdate(),
role int default 2,
foreign key (role) references user_role(role_id),
isActive bit default 1
);

drop table user_table

CREATE TABLE train_type
(
type_id int primary key identity(1,1),
type_name varchar(10),
base_fare float,
isActive bit default 1
);

drop table train_type

CREATE TABLE class
(
class_id int primary key identity(1,1),
class_name varchar(10),
fare_multiplier float,
refund_multiplier float,
isActive bit default 1
);

drop table Class

CREATE TABLE Train
(
train_no int primary key identity(1000,1),
train_name varchar(20),
train_type int,
source varchar(20),
destination varchar(20),
totalKms int,
isActive bit default 1,
foreign key (train_type) references train_type(type_id)
);

drop table Train

CREATE TABLE TrainClassCapacity
(
capacity_id int primary key identity(1,1),
train_no int,
class_id int,
available_seats int,
total_seats int,
fare float,
isActive bit,
foreign key (train_no) references Train(train_no),
foreign key(class_id) references class(class_id)
);

drop table TrainClassCapacity

CREATE TABLE Reservation
(
reservation_id int primary key identity(1000,1),
user_id int,
train_no int,
reservation_status varchar(30),
capacity_id int,
passenger_count int check (passenger_count <= 6),
booking_date datetime default getdate(),
journey_date date,
isActive bit default 1,
foreign key (user_id) references user_table(user_id),
foreign key (train_no) references train(train_no),
foreign key(capacity_id) references TrainClassCapacity(capacity_id)
);

drop table Reservation

CREATE TABLE cancellation
(
cancellation_id int primary key identity(1000,1),
reservation_id int,
cancelled_passenger_count int,
refund_amount float,
cancellation_date date default getdate(),
refund_status varchar(30),
foreign key(reservation_id) references Reservation(reservation_id)
);

drop table cancellation