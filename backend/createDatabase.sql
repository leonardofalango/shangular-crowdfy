CREATE TABLE dbo.users (
	Id int primary key identity,
	Completename varchar(100),
	Username varchar(50),
	Photo varchar(MAX),
	BornDate DATETIME,
	Mail VARCHAR(255),
	isAuth int DEFAULT(0)
)

CREATE TABLE dbo.forum (
	Id int primary key identity,
	CreatedAt DATETIME DEFAULT(GETDATE()),
	Title VARCHAR(80),
	Description VARCHAR(255),
	Photo VARCHAR(MAX)
)

CREATE TABLE dbo.userXforum (
	IdUser int,
	IdForum int,
	PRIMARY KEY(IdUser, IdForum),
	FOREIGN KEY(IdUser) REFERENCES dbo.users(Id),
	FOREIGN KEY(IdForum) REFERENCES dbo.forum(Id)
)

CREATE TABLE dbo.posts (
	Id int primary key identity,
	Author int,
	Title VARCHAR(90),
	Content VARCHAR(320),
	CreatedAt DATETIME DEFAULT(GETDATE()),
	Crowds INT DEFAULT(0),
	Comments INT DEFAULT(0),
)

ALTER TABLE dbo.posts
	ADD IdPost INT DEFAULT(NULL), FOREIGN KEY (IdPost)
		REFERENCES dbo.posts(Id)
