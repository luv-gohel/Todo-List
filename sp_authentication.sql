Alter procedure Authentication
(
	@Email nvarchar(50),
	@Password nvarchar(50),
	@userGuid uniqueidentifier output,
	@status int output
)
AS
BEGIN
	Declare @GID uniqueidentifier;
	select @GID=GID from Users where Email = @Email AND Password = @Password;
		
	IF @GID IS NOT NULL
		BEGIN
			SET @status = 101;
			SET @userGuid = @GID;
		END
	ELSE
		SET @status = 100;
END

declare @output uniqueidentifier;
declare @output2 int;
exec Authentication 'luvgohel@gmail.com' ,'luvgohel' ,  @userGuid = @output  output,@status =  @output2 output
if(@output2 = 100)
	print 'no user found'
else
	print @output

select * from Users