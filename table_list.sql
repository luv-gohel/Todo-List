create table List(
	ID Int identity(1,1) not null,
	TaskGID uniqueidentifier not null,
	UserGID uniqueidentifier not null,
	TaskTitle nvarchar(50) not null,
	TaskDescription nvarchar(MAX) not null,
	Priority int not null,
	IsDeleted BIT not null,
	DeletedDate datetime not null,
	isUpdated Bit not null,
	UpdatedDate datetime not null,
)

