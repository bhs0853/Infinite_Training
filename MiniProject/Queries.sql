use MiniProject;

-- Create user procedure

create or alter procedure proc_createUser @Name varchar (30), @email varchar(30), @password varchar(10), @phone bigint, @dob date
as
begin
	begin try
		insert into user_table (name, email, password, phone, dob) values(@Name, @email, @password, @phone, @dob);
		return 1;
	end try
	begin catch
		raiserror('Unable to create user', 15,1);
	end catch
end

-- create admin procedure

create or alter procedure proc_createAdmin @Name varchar (30), @email varchar(30), @password varchar(10), @phone bigint, @dob date, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission Denied', 15, 1);
	end
	begin try
		insert into user_table (name, email, password, phone, dob, role) values(@Name, @email, @password, @phone, @dob, 1);
		return 1;
	end try
	begin catch
		raiserror('Unable to create admin', 15,1);
	end catch
end

-- Delete user procedure

create or alter procedure proc_deleteUser @user_id int
as
begin
	if((select name from user_table where user_id = @user_id and isActive = 1) is null)
	begin
		raiserror('Could not find user', 15, 1);
	end
	begin try
		update user_table set isActive = 0 where user_id = @user_id;
		return 1;
	end try
	begin catch
		raiserror('Unable to delete user', 15, 1);
	end catch
end

-- user login function

create or alter function fn_userLogin(@email varchar(30), @password varchar(10))
returns @credentials table(user_id int, role varchar(10))
as
begin
	if((select user_id from user_table where email = @email and password = @password) is null)
	begin
		insert into @credentials values (-1, 'not_found');
		return;
	end
	insert into @credentials select user_id, role_name from user_role r join user_table u on u.role = r.role_id where email = @email and password = @password;
	return;
end

-- get user details function

create or alter function fn_getUser(@user_id int)
returns @UserTable table(user_id int, name varchar(30), email varchar(30), phone bigint, dob date, role varchar(10), isActive bit)
as
begin
	if((select name from user_table where user_id = @user_id) is null)
	begin
		insert into @UserTable values (@user_id, 'User not found', null, null, null, null, null);
		return;
	end
	insert into @UserTable select user_id, name, email, phone, dob, role, isActive from user_table where user_id = @user_id;
	return;
end

-- create train type procedure

create or alter procedure proc_createTrainType @type_name varchar(10), @base_fare float, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	begin try
		insert into train_type (type_name, base_fare) values (@type_name, @base_fare);
		return 1;
	end try
	begin catch
		raiserror('Unable to insert train type', 15, 1);
	end catch
end

-- delete train type procedure

create or alter procedure proc_deleteTrainType @type_id int, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	if((select type_name from train_type where type_id = @type_id and isActive = 1) is null)
	begin
		raiserror('Train type not found', 15, 1);
	end
	begin try
		update train_type set isActive = 0 where type_id = @type_id;
		return 1;
	end try
	begin catch
		raiserror('Could not delete train type', 15, 1);
	end catch
end

-- create train class procedure

create or alter procedure proc_createTrainClass @class_name varchar(10), @fare_multiplier float, @refund_multiplier float, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	begin try
		insert into class (class_name, fare_multiplier, refund_multiplier) values (@class_name, @fare_multiplier, @refund_multiplier);
		return 1;
	end try
	begin catch
		raiserror('Unable to insert into train class table', 15, 1);
	end catch
end

-- delete train class procedure

create or alter procedure proc_deleteTrainClass @class_id int, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	if((select class_name from class where class_id = @class_id and isActive = 1) is null)
	begin
		raiserror('class not found', 15, 1);
	end
	begin try
		update class set isActive = 0 where class_id = @class_id
		return 1;
	end try
	begin catch
		raiserror('could not delete class', 15, 1);
	end catch
end

-- Create train procedure

create or alter procedure proc_createTrain @train_name varchar(20), @type_id int, @totalKms int, @source varchar(20), @destination varchar(20), @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	begin try
		insert into Train (train_name, train_type, source, destination, totalKms) values (@train_name, @type_id, @source, @destination, @totalKms);
		return (select train_no from Train where train_name = @train_name);
	end try
	begin catch
		raiserror('Unable to insert train', 15, 1);
	end catch
end

-- get all active trains

create or alter function fn_getAllTrains()
returns @train_table table(train_no int, train_name varchar(20), train_type varchar(10), source varchar(20), destination varchar(20), totalKms int)
as
begin
	insert into @train_table select train_no, train_name, type_name, source, destination, totalKms from Train join train_type on train.train_type = train_type.type_id where train.isActive = 1;
	return;
end

-- delete train procedure

create or alter procedure proc_deleteTrain @train_no int, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
	end
	if((select train_name from train where train_no = @train_no and isActive = 1) is null)
	begin 
		raiserror('Train not found', 15, 1);
	end
	begin try
		declare @reservation_id int, @cancelled_passenger_count int, @user int, @fare int;
		update Train set isActive = 0 where train_no = @train_no;
		select * into #temptable from (select * from dbo.fn_getReservations(@user_id) where train_no = @train_no) as reservations;
		while(exists(select reservation_id from #temptable))
		begin
			select top 1 @reservation_id = reservation_id, @cancelled_passenger_count = passenger_count, @user = user_id, @fare = total_fare from #temptable;
			insert into cancellation (reservation_id, cancelled_passenger_count, refund_amount, refund_status) values
				(@reservation_id, @cancelled_passenger_count, @fare, 'Refund successful');
			delete from #temptable where reservation_id = @reservation_id;
			update Reservation set isActive = 0 where reservation_id = @reservation_id;
		end
		return 1;
	end try
	begin catch
		raiserror('Could not delete train', 15, 1);
	end catch
end

-- Create train class capacity

create or alter procedure proc_createTrainClassCapacity @train_no int, @total_seats int, @user_id int
as
begin
	if((select role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id) != 'admin')
	begin
		raiserror('Permission denied', 15, 1);
		return -1;
	end
	declare	@class_id int, @fare_multiplier float, @totalKms int, @base_fare float;
	select @totalKms = totalKms from Train where train_no = @train_no;
	select @base_fare = base_fare from train_type where type_id = (select type_id from Train where train_no = @train_no);
	if(@totalKms is null)
	begin
		raiserror('Train not found', 15, 1);
	end
	select * into #temptable from class;
	begin try
		while(exists(select class_id from #temptable))
		begin
			select top 1 @class_id = class_id, @fare_multiplier = fare_multiplier from #temptable;
			insert into TrainClassCapacity(train_no, class_id, available_seats, total_seats , fare) values
				(@train_no, @class_id, @total_seats, @total_seats, @totalKms * @fare_multiplier * @base_fare);	
			delete from #temptable where class_id = @class_id;
		end
		return 1;
	end try
	begin catch
		delete train where train_no = @train_no;
		raiserror('Unable to add tickets', 15, 1);
	end catch
end

-- get train class capacity by train_no

create or alter function fn_getSeatsPerClassByTrain(@train_no int)
returns @seatsPerClass table(capacity_id int, class_name varchar(10), available_seats int, fare float)
as
begin
	insert into @seatsPerClass select capacity_id, class_name, available_seats, fare from TrainClassCapacity t join class c on t.class_id = c.class_id where t.train_no = @train_no and c.isActive = 1;
	return;
end


-- create reservation procedure

create or alter procedure proc_createReservation @user_id int, @train_no int, @class_id int, @passenger_count int, @journey_date date
as
begin
	declare @capacity_id int;
	select @capacity_id = capacity_id from TrainClassCapacity where train_no = @train_no and class_id = @class_id
	if(@capacity_id is null)
	begin 
		raiserror('Could not find suitable train or class', 15, 1);
	end
	if(DATEDIFF(DAY, GETDATE(), @journey_date) < 0)
	begin
		raiserror('Cannot book tickets for a past date', 15, 1);
	end
	begin try
		if((select available_seats from TrainClassCapacity where capacity_id = @capacity_id) >= @passenger_count)
		begin
			insert into Reservation (user_id, train_no, reservation_status, capacity_id, passenger_count, booking_date, journey_date) values
				(@user_id, @train_no, 'Booked', @capacity_id, @passenger_count, GETDATE(), @journey_date);
			update TrainClassCapacity set available_seats = available_seats - @passenger_count where capacity_id = @capacity_id;
			return 1;
		end
		else
		begin
			raiserror('Could not book tickets due to inavailability', 15, 1);
		end
	end try
	begin catch
		raiserror('Could not book ticket', 15, 1);
	end catch
end

-- get all reservations

create or alter function fn_getReservations(@user_id int)
returns @Reservation_Table table(user_id int, reservation_id int, train_no int, reservation_status varchar(30), class_name varchar(10), total_kms int, total_fare int, passenger_count int, booking_date date, journey_date date, isActive bit)
as
begin
	declare @train_no int, @passenger_count int, @reservation_status varchar(30), @role varchar(10), @class_name varchar(10), @total_kms int, @total_fare int, @capacity_id int, @reservation_id int, @class_id int, @fare float, @booking_date date, @journey_date date, @isActive bit;
	declare @temptable table (
		reservation_id int,
		user_id int,
		train_no int,
		reservation_status varchar(30),
		capacity_id int,
		passenger_count int,
		booking_date date,
		journey_date date,
		isActive bit
	);
	select @role = role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id;
	if(@role is null)
	begin
		insert into @Reservation_Table values (null, null, null, 'User not found', null, null, null, null, null, null, null);
	end
	else if(@role = 'admin')
	begin
		insert into @temptable select * from Reservation;
		while(exists(select reservation_id from @temptable))
		begin
			select top 1 @capacity_id = capacity_id, @reservation_id = reservation_id, @train_no = train_no, @user_id = user_id, @passenger_count = passenger_count, @reservation_status = reservation_status, @booking_date = booking_date, @journey_date = journey_date, @isActive = isActive from @temptable
			select @class_id = class_id, @fare = fare from TrainClassCapacity where capacity_id = @capacity_id;
			select @total_kms = totalKms from Train where train_no = @train_no;
			select @class_name = class_name from class where class_id  = @class_id;
			insert into @Reservation_Table values (@user_id, @reservation_id, @train_no, @reservation_status, @class_name, @total_kms, @fare * @passenger_count, @passenger_count, @booking_date, @journey_date, @isActive);
			delete from @temptable where reservation_id = @reservation_id;
		end
	end
	else
	begin
		insert into @temptable select * from Reservation where user_id = @user_id;
		while(exists(select reservation_id from @temptable))
		begin
			select top 1 @capacity_id = capacity_id, @reservation_id = reservation_id, @train_no = train_no, @passenger_count = passenger_count, @reservation_status = reservation_status, @booking_date = booking_date, @journey_date = journey_date, @isActive = isActive from @temptable
			select @class_id = class_id, @fare = fare from TrainClassCapacity where capacity_id = @capacity_id;
			select @total_kms = totalKms from Train where train_no = @train_no;
			select @class_name = class_name from class where class_id  = @class_id;
			insert into @Reservation_Table values (@user_id, @reservation_id, @train_no, @reservation_status, @class_name, @total_kms, @fare * @passenger_count, @passenger_count, @booking_date, @journey_date, @isActive);
			delete from @temptable where reservation_id = @reservation_id;
		end
	end
	return;
end

-- create cancellation procedure

create or alter procedure proc_createCancellation @reservation_id int, @cancelled_passenger_count int, @user_id int
as
begin
	declare @refund_amount float, @capacity_id int, @class_id int, @fare float, @refund_multiplier float, @passenger_count int, @id int;
	select @capacity_id = capacity_id, @passenger_count = passenger_count, @id = user_id from Reservation where reservation_id = @reservation_id;
	select @class_id = class_id, @fare = fare from TrainClassCapacity where capacity_id = @capacity_id;
	select @refund_multiplier = refund_multiplier from class where class_id = @class_id;
	if(@id != @user_id)
	begin
		raiserror('Permission denied', 15, 1);
	end
	if(@passenger_count < @cancelled_passenger_count)
	begin
		raiserror('Trying to cancel more passengers than reserved', 15, 1);
	end
	begin try
		set @refund_amount = @cancelled_passenger_count * @fare * @refund_multiplier;
		insert into cancellation (reservation_id, cancelled_passenger_count, refund_amount, refund_status) values
			(@reservation_id, @cancelled_passenger_count, @refund_amount, 'Refund successful');
		update Reservation set passenger_count = passenger_count - @cancelled_passenger_count where reservation_id = @reservation_id;
		if((select passenger_count from Reservation where reservation_id = @reservation_id) = 0)
		begin
			update Reservation set isActive = 0 where reservation_id = @reservation_id
		end
		update TrainClassCapacity set available_seats = available_seats + @cancelled_passenger_count where capacity_id = @capacity_id;
		return 1;
	end try
	begin catch
		raiserror('Could not cancel tickets', 15, 1);
	end catch
end

-- get all cancellations

create or alter function fn_getCancellations(@user_id int)
returns @Cancellation_Table table(user_id int, cancellation_id int, reservation_id int, train_no int, refund_amount float, refund_status varchar(30), cancelled_passenger_count int, cancellation_date date)
as
begin
	declare @role varchar(10), @cancellation_id int, @reservation_id int, @train_no int, @refund_amount float, @refund_status varchar(30), @cancelled_passenger_count int, @cancellation_date date;
	declare @temptable table (
		cancellation_id int,
		reservation_id int,
		cancelled_passenger_count int,
		refund_amount float,
		cancellation_date date,
		refund_status varchar(30)
	);
	select @role = role_name from user_role r join user_table u on r.role_id = u.role where u.user_id = @user_id;
	if(@role is null)
	begin
		insert into @Cancellation_Table values (null, null, null, null, null, 'User not found', null, null);
	end
	else if(@role = 'admin')
	begin
		insert into @temptable select * from cancellation;
		while(exists(select cancellation_id from @temptable))
		begin
			select top 1 @cancellation_id = cancellation_id,  @reservation_id = reservation_id, @refund_amount = refund_amount, @refund_status = refund_status, @cancelled_passenger_count = cancelled_passenger_count, @cancellation_date = cancellation_date from @temptable;
			select @train_no = train_no, @user_id = user_id from Reservation where reservation_id = @reservation_id;
			insert into @Cancellation_Table values (@user_id, @cancellation_id, @reservation_id, @train_no , @refund_amount , @refund_status , @cancelled_passenger_count , @cancellation_date);
			delete from @temptable where cancellation_id = @cancellation_id;
		end
	end
	else
	begin
		insert into @temptable select * from cancellation where reservation_id in (select reservation_id from Reservation where user_id = @user_id);
		while(exists(select cancellation_id from @temptable))
		begin
			select top 1 @cancellation_id = cancellation_id,  @reservation_id = reservation_id, @refund_amount = refund_amount, @refund_status = refund_status, @cancelled_passenger_count = cancelled_passenger_count, @cancellation_date = cancellation_date from @temptable;
			select @train_no = train_no from Reservation where reservation_id = @reservation_id;
			insert into @Cancellation_Table values (@user_id, @cancellation_id, @reservation_id, @train_no , @refund_amount , @refund_status , @cancelled_passenger_count , @cancellation_date);
			delete from @temptable where cancellation_id = @cancellation_id;
		end
	end
	return;
end
