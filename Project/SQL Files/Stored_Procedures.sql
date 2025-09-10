use BankingDB

create or alter procedure Sp_Register_Account @Title varchar(3),
	@First_Name varchar(20),
	@Middle_Name varchar(20),
	@Last_Name varchar(20),
	@Father_Name varchar(15),
	@Mobile_Number bigint,
	@Email_Id varchar(30),
	@Aadhar varchar(12),
	@Gender varchar(6),
	@Date_Of_Birth date,
	@Residential_Address varchar(50),
	@Permanent_Address varchar(50),
	@Occupation_Type varchar(50),
	@Source_Of_Income varchar(50),
	@Gross_Annual_Income float,
	@Opt_Debit_Card bit,
	@Opt_Net_Banking bit
as
begin
	if exists(select 1 from RegisterAccount where aadhar = @Aadhar)
	begin
		raiserror('Already Registered!!! Pending Approval', 15, 1);
		return;
	end
	begin try
		insert into RegisterAccount values(@Title, @First_Name, @Middle_Name, @Last_Name, @Father_Name, @Mobile_Number,
		@Email_Id, @Aadhar, @Gender, @Date_Of_Birth, @Residential_Address, @Permanent_Address, @Occupation_Type, @Source_Of_Income,
		@Gross_Annual_Income, @Opt_Debit_Card, @Opt_Net_Banking);
		select Service_Reference_Number from RegisterAccount where Aadhar = @Aadhar order by Service_Reference_Number desc;
	end try
	begin catch
		raiserror('Unable to register account', 15, 1);
		return;
	end catch
end

create or alter procedure Sp_RejectAccount @Service_Reference_Number int, @id int, @Remarks varchar(100)
as
begin
	declare @Email_Id varchar(30);
	select @Email_id = email_id from RegisterAccount where Service_Reference_Number = @Service_Reference_Number;
	if(@Email_Id is null)
	begin
		raiserror('Could not find your registration', 15, 1);
		return;
	end
	begin try
		insert into RejectedAccounts (service_reference_number, email_id, remarks) values (@Service_Reference_Number, @Email_Id, @Remarks);
		delete from RegisterAccount where Service_Reference_Number = @Service_Reference_Number;
	end try
	begin catch
		raiserror('Error occured while rejecting...', 15, 1);
		return;
	end catch
end

create or alter procedure Sp_CreateAccount @Service_Reference_Number int, @id int
as
begin
	declare @Title varchar(3),
	@First_Name varchar(20),
	@Middle_Name varchar(20),
	@Last_Name varchar(20),
	@Father_Name varchar(15),
	@Mobile_Number bigint,
	@Email_Id varchar(30),
	@Aadhar varchar(12),
	@Gender varchar(6),
	@Date_Of_Birth date,
	@Residential_Address varchar(50),
	@Permanent_Address varchar(50),
	@Occupation_Type varchar(50),
	@Source_Of_Income varchar(50),
	@Gross_Annual_Income float,
	@Opt_Debit_Card bit,
	@Opt_Net_Banking bit,
	@Customer_Id int;

	if((select email_id from admin_table where id = @id) is null)
	begin
		raiserror('Permission Denied', 15, 1);
		return;
	end

	select @Title = title, @First_Name = First_Name, @Middle_Name = Middle_Name, @Last_Name = Last_Name, @Father_Name = Father_Name,
	@Mobile_Number = Mobile_Number, @Email_Id = Email_Id, @Aadhar = Aadhar, @Gender = Gender, @Date_Of_Birth = Date_Of_Birth,
	@Residential_Address = Residential_Address, @Permanent_Address = Permanent_Address, @Occupation_Type = Occupation_Type,
	@Source_Of_Income = Source_Of_Income, @Gross_Annual_Income = Gross_Annual_Income, @Opt_Debit_Card = Opt_Debit_Card, @Opt_Net_Banking = Opt_Net_Banking  from RegisterAccount;

	begin try
		select @Customer_Id = customer_id from customer where Aadhar = @Aadhar;
		if(@Customer_id is null)
		begin
			insert into Customer(Title, First_Name, Middle_Name, Last_Name, Father_Name, Mobile_Number, Email_Id, Aadhar, Gender, Date_Of_Birth, Residential_Address, Permanent_Address, Occupation_Type, Source_Of_Income, Gross_Annual_Income)
			values (@Title, @First_Name, @Middle_Name, @Last_Name, @Father_Name, @Mobile_Number,
				@Email_Id, @Aadhar, @Gender, @Date_Of_Birth, @Residential_Address, @Permanent_Address, @Occupation_Type, @Source_Of_Income, @Gross_Annual_Income);
			select @Customer_Id = customer_id from customer where Aadhar = @Aadhar;
		end

		insert into accounts (Customer_Id, Balance) values (@customer_id, 1000);
		delete from RegisterAccount where Service_Reference_Number = @Service_Reference_Number;

		declare @Account_Number int;
		select @Account_Number = Account_Number from accounts where customer_id = @Customer_Id and balance = 1000;
	end try
	begin catch
		raiserror('Could not create account', 15, 1);
		return;
	end catch
end

create or alter procedure Sp_CreateInternetBanking 	@Account_Number int, @login_password varchar(100), @transaction_password varchar(100)
as
begin
	if((select Account_Number from Accounts where Account_Number = @Account_Number) is null)
	begin
		raiserror('Could not find account', 15, 1);
		return;
	end
	begin try
		exec Sp_CreateDebitCard @Account_Number;
		declare @email varchar(50);
		select @email = Email_Id from customer where customer_id = (select customer_id from accounts where Account_Number = @Account_Number)
		insert into Internet_Banking_Details (Account_Number,email_id, login_password, transaction_password, last_login) values (@Account_Number, @email, @login_password, @transaction_password, getdate());
	end try
	begin catch
		raiserror('Could not activate internet banking', 15, 1);
		return;
	end catch
end

create or alter procedure Sp_CreateDebitCard @Account_Number int
as
begin
	if((select Account_Number from Accounts where Account_Number = @Account_Number) is null)
	begin
		raiserror('Could not find account', 15, 1);
		return;
	end
	begin try
		declare @expire_date date;
		set @expire_date = DATEADD(year, 5, getdate());
		insert into Debit_Card_Details values (@Account_Number, @expire_date);
	end try
	begin catch
		raiserror('Could not create debit card', 15, 1);
		return;
	end catch
end

CREATE OR ALTER PROCEDURE Sp_MakeTransaction
    @From_Account INT,
    @To_Account INT,
    @Transaction_Mode VARCHAR(10),
    @Amount INT,
    @Transaction_Date DATETIME,
    @Remarks VARCHAR(50),
    @transaction_password VARCHAR(100)
AS
BEGIN
    BEGIN TRY
        -- Validate To_Account
        IF NOT EXISTS (SELECT 1 FROM Accounts WHERE Account_Number = @To_Account)
        BEGIN
            RAISERROR('Could not find the account to be credited', 15, 1);
            RETURN;
        END

        -- Validate transaction password
        IF NOT EXISTS (
            SELECT 1 FROM Internet_Banking_Details 
            WHERE Account_Number = @From_Account AND transaction_password = @transaction_password
        )
        BEGIN
            RAISERROR('Permission Denied', 15, 1);
            RETURN;
        END

        DECLARE @available_balance INT;
        SELECT @available_balance = Balance FROM Accounts WHERE Account_Number = @From_Account;

        IF(@available_balance < @Amount)
        BEGIN
            RAISERROR('Insufficient Funds', 15, 1);
            RETURN;
        END

        IF (@Remarks IS NULL OR @Remarks = '')
        BEGIN
            SET @Remarks = 'Amount ' + CAST(@Amount AS VARCHAR) + ' sent to account: ' + CAST(@To_Account AS VARCHAR);
        END

        BEGIN TRANSACTION;

        DECLARE @From_Balance FLOAT, @To_Balance FLOAT;

        -- Debit from sender
        UPDATE Accounts SET Balance = Balance - @Amount WHERE Account_Number = @From_Account;
        SELECT @From_Balance = Balance FROM Accounts WHERE Account_Number = @From_Account;

        INSERT INTO Transaction_Details 
        (from_account, to_account, transaction_mode, transaction_type, amount, remarks, Balance) 
        VALUES (@From_Account, @To_Account, @Transaction_Mode, 'DEBIT', @Amount, @Remarks, @From_Balance);

        -- Credit to receiver
        UPDATE Accounts SET Balance = Balance + @Amount WHERE Account_Number = @To_Account;
        SELECT @To_Balance = Balance FROM Accounts WHERE Account_Number = @To_Account;

        SET @Remarks = 'Amount ' + CAST(@Amount AS VARCHAR) + ' sent from account: ' + CAST(@From_Account AS VARCHAR);

        INSERT INTO Transaction_Details 
        (from_account, to_account, transaction_mode, transaction_type, amount, remarks, Balance) 
        VALUES (@From_Account, @To_Account, @Transaction_Mode, 'CREDIT', @Amount, @Remarks, @To_Balance);

        COMMIT;

        SELECT CAST((SCOPE_IDENTITY() - 1) AS INT) AS Id;
    END TRY
    BEGIN CATCH
        IF(@@TRANCOUNT > 0)
            ROLLBACK;

        throw;
    END CATCH
END


create or alter procedure Sp_ChangeLoginPassword 
	@account_number int,
	@old_password varchar(100),
	@new_password varchar(100)
as
begin
	if((select email_id from Internet_Banking_Details where Account_Number = @account_number and login_password = @old_password) is not null)
	begin
		if(@old_password = @new_password)
		begin
			raiserror('New password cant be same as old password', 15, 1);
			return;
		end
		update Internet_Banking_Details set login_password = @new_password where Account_Number = @account_number
	end
	else
	begin 
		raiserror('Invalid Credentials',16,1);
		return;
	end
end

create or alter procedure Sp_ChangeTransactionPassword 
	@account_number int,
	@old_password varchar(100),
	@new_password varchar(100)
as
begin
	if((select email_id from Internet_Banking_Details where Account_Number = @account_number and transaction_password = @old_password) is not null)
	begin
		if(@old_password = @new_password)
		begin
			raiserror('New password cant be same as old password', 15, 1);
			return;
		end
		update Internet_Banking_Details set transaction_password = @new_password where Account_Number = @account_number
	end
	else
	begin 
		raiserror('Invalid Credentials',16,1)
		return;
	end
end

create or alter function fn_GetStatement(@Account_number int,
	@from_date datetime,
	@to_date datetime)
returns @statement table(Transaction_Id int, From_Account int, To_Account int, Transaction_Mode varchar(50), Transaction_Type varchar(6), Amount int, Transaction_Date datetime, Remarks varchar(50), Balance int)
as
begin
	if((select Account_number from Accounts where Account_Number = @Account_number) is null)
	begin
		insert into @statement values( -1, -1, -1, 'Unable to Find account', '', 0, null, null, null);
		return;
	end
		insert into @statement select Transaction_Id, From_Account, To_Account, Transaction_Mode, Transaction_Type, Amount, Transaction_Date, Remarks, Balance from Transaction_Details 
			where ((From_Account = @Account_number and Transaction_Type = 'DEBIT')  or (To_Account = @Account_number and Transaction_Type = 'CREDIT')) and Transaction_Date between @from_date and @to_date
				order by Transaction_Date desc
	return;
end

create or alter procedure Sp_AddPayee
	@beneficiary_name varchar(50),
	@from_account int,
	@to_account int,
	@nickname varchar(20)
as
begin
	if((select Account_Number from Accounts where Account_Number = @to_account) is null)
	begin
		raiserror('Could not find payee account', 15, 1);
		return;
	end
	
	IF EXISTS (SELECT 1 FROM Payees WHERE To_Account = @to_account AND From_Account = @from_account)
	BEGIN
		RAISERROR('Beneficiary already present', 15, 1);
		RETURN;
	END

	begin try
		insert into Payees (Beneficiary_Name, From_Account, To_Account, Nickname) values (@beneficiary_name, @from_account, @to_account, @nickname);
	end try
	begin catch
		raiserror('Could not add payee', 15, 1);
		return;
	end catch
end
 
create or alter function fn_GetPayee(@Payee_Id int)
returns @PayeeDetails table(Payee_Id int, Beneficiary_Name varchar(50), Account_Number int, Nickname varchar(20))
as
begin
	if((select From_Account from Payees where Payee_Id = @Payee_Id) is null)
	begin
		insert into @PayeeDetails values (-1, 'Could not find payee', null, null);
		return;
	end
	insert into @PayeeDetails select Payee_Id, Beneficiary_Name, To_Account, Nickname from Payees where Payee_Id = @Payee_Id;
	return;
end

create or alter function fn_CustomerLogin (@email varchar(30), @login_password varchar(100))
returns @Details table(Account_Number int)
as
begin
	declare @Account_Number int;
	select @Account_Number = account_number from Internet_Banking_Details where Email_Id = @email and login_password = @login_password;
	if(@Account_Number is null)
	begin
		set @Account_Number = -1;
	end
	insert into @Details values (@Account_Number);
	return;
end

create or alter function fn_AdminLogin (@email varchar(30), @password varchar(100))
returns @Details table(id int)
as
begin
	declare @id int;
	select @id = id from Admin_Table where Email_Id = @email and password = @password;
	if(@id is null)
	begin
		set @id = -1;
	end
		insert into @Details values (@id);
	return;
end

CREATE OR ALTER PROCEDURE Sp_RaiseSupportMessage
    @UserEmail VARCHAR(100),
    @Subject VARCHAR(200),
    @Message TEXT
AS
BEGIN
    BEGIN TRY
        INSERT INTO SupportMessages (UserEmail, Subject, Message, SentAt, Status)
        VALUES (@UserEmail, @Subject, @Message, GETDATE(), 'Pending');
 
        -- Return the newly inserted Id
        return SELECT CAST(SCOPE_IDENTITY() AS INT) AS Id;
    END TRY
    BEGIN CATCH
        RAISERROR('Could not raise support message', 15, 1);
		return;
    END CATCH
END