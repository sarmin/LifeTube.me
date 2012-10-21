SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[profileid] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[lastName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[createdOn] [datetime] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[profileid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[email] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[password] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[firstName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[lastName] [varchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[phone] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[profileid] [int] NULL,
	[rootUser] [bit] NULL,
	[createdBy] [int] NULL,
	[createdDate] [datetime] NULL,
	[lastModifiedBy] [int] NULL,
	[lastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)

GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Profile] FOREIGN KEY([profileid])
REFERENCES [dbo].[Profile] ([profileid])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Profile]
GO
