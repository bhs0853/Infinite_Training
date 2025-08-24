CREATE DATABASE ElectricityBillDB;
use ElectricityBillDB

create table ElectricityBill
(
consumer_number varchar(20),
consumer_name varchar(50),
units_consumed int,
bill_amount float,
created_at datetime default getdate()
);