ALTER procedure Sp_Authorization(
	@FirstName nvarchar(20),
	@LastName nvarchar(20),
	@Email nvarchar(50),
	@Password nvarchar(50),
	@PhoneNo nvarchar(10),
	@Gender nvarchar(20),
	@status int output 
)
AS
BEGIN
	IF Exists (SELECT 1 from Users where Email = @Email)
		SET @status = 102;
	ELSE
	BEGIN
		insert into Users	( GID,	   FirstName, LastName, Email,
							  Password,PhoneNo,	  Gender	
							)
					Values  ( NEWID(),   @FirstName ,@LastName ,@Email,
							  @Password, @PhoneNo, @Gender
							)
	   SET @status = 101;
	END
END
declare @instatus int;
exec Sp_Authorization 'luv' , 'gohel', 'luvgohel@gmail.com','luvgohel','8128892734','Male', @status = @instatus output
print @instatus