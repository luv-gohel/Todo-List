create table Users(
	ID int identity(1,1) not null,
	GID uniqueidentifier not null,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Email nvarchar(50) not null,
	Password nvarchar(20) not null,
	PhoneNo nvarchar(10) not null,
	Gender nvarchar(10) not null,
)