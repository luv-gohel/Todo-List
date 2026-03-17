USE [Todo]
GO

/****** Object:  Table [dbo].[List]    Script Date: 3/17/2026 7:23:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[List](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TaskGID] [uniqueidentifier] NOT NULL,
	[UserGID] [uniqueidentifier] NOT NULL,
	[TaskTitle] [nvarchar](50) NOT NULL,
	[TaskDescription] [nvarchar](max) NOT NULL,
	[Priority] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
	[DeletedDate] [datetime] NULL,
	[isUpdated] [bit] NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[List] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[List] ADD  DEFAULT (NULL) FOR [DeletedDate]
GO

ALTER TABLE [dbo].[List] ADD  DEFAULT ((0)) FOR [isUpdated]
GO

ALTER TABLE [dbo].[List] ADD  DEFAULT (NULL) FOR [UpdatedDate]
GO


