/*
Do not use database modifying (ALTER DATABASE), creating (CREATE DATABASE) or switching (USE) statements 
in this file.
*/

-- User table
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [Username] [nvarchar](50) NOT NULL,
    [PwdHash] [nvarchar](256) NOT NULL, 
    [PwdSalt] [nvarchar](256) NOT NULL, 
    [FirstName] [nvarchar](256) NOT NULL,
    [LastName] [nvarchar](256) NOT NULL,
    [Email] [nvarchar](256) NOT NULL,
    [Phone] [nvarchar](256) NULL,
	[IsConfirmed] [bit] NOT NULL DEFAULT 0,
	[SecurityToken] [nvarchar](256) NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

-- Genre table (M-N)
CREATE TABLE [dbo].[Genre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[DebutYear] [int] NULL
	CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

--Review (User M-N)
CREATE TABLE [dbo].[Review](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [int] NOT NULL,
	[IsFavorite] [bit] NOT NULL DEFAULT 0,
	[Comment] [nvarchar](256) NULL,
	CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO


-- Performer table (1-N)
CREATE TABLE [dbo].[Performer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
    [LastName] [nvarchar](256) NOT NULL,
	[YearOfBirth][int] NULL

	CONSTRAINT [PK_Performer] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

-- Song table (Primary)
CREATE TABLE [dbo].[Song](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Tempo] [nvarchar](256) NULL,
	[Melody] [nvarchar](256) NULL,
	[Language] [nvarchar](256) NULL,
	[YearOfRelease] [int] NULL,
	[PerformerId] [int] NOT NULL,
	CONSTRAINT [PK_Song] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO

-- SongGenre table (M-to-N bridge)
CREATE TABLE [dbo].[SongGenre](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SongId] [int] NOT NULL,
	[GenreId] [int] NOT NULL,
	CONSTRAINT [PK_SongGenre] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
)
GO


-- UserReview table (User M-to-N bridge)
CREATE TABLE [dbo].[UserReview](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [int] NOT NULL,
    [ReviewId] [int] NOT NULL,
    CONSTRAINT [PK_UserReview] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
)
GO

-- Log (used for logs)
CREATE TABLE [dbo].[Logs](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Timestamp] [datetime2](7) NOT NULL,
    [Level] [nvarchar](50) NULL,
	[Message] [nvarchar](max)  NULL
)
GO

-- Foreign key constraints
ALTER TABLE [dbo].[Song]  WITH CHECK ADD  CONSTRAINT [FK_Song_Performer] FOREIGN KEY([PerformerId])
REFERENCES [dbo].[Performer] ([Id])
GO

ALTER TABLE [dbo].[Song] CHECK CONSTRAINT [FK_Song_Performer]
GO

ALTER TABLE [dbo].[SongGenre]  WITH CHECK ADD  CONSTRAINT [FK_SongGenre_Song] FOREIGN KEY([SongId])
REFERENCES [dbo].[Song] ([Id])
GO

ALTER TABLE [dbo].[SongGenre] CHECK CONSTRAINT [FK_SongGenre_Song]
GO

ALTER TABLE [dbo].[SongGenre]  WITH CHECK ADD  CONSTRAINT [FK_SongGenre_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
GO

ALTER TABLE [dbo].[SongGenre] CHECK CONSTRAINT [FK_SongGenre_Genre]
GO

ALTER TABLE [dbo].[UserReview]  WITH CHECK ADD  CONSTRAINT [FK_UserReview_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserReview] CHECK CONSTRAINT [FK_UserReview_User]
GO

ALTER TABLE [dbo].[UserReview]  WITH CHECK ADD  CONSTRAINT [FK_UserReview_Review] FOREIGN KEY([ReviewId])
REFERENCES [dbo].[Review] ([Id])
GO

ALTER TABLE [dbo].[UserReview] CHECK CONSTRAINT [FK_UserReview_Review]
GO

/* Use table data inserting, modifying, deleting and retrieving statements here */