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


--? SENHA DO SUPER USER
-- leonardofalas

INSERT INTO dbo.users (Completename, Username, Photo, BornDate, Mail, isAuth, Salt, HashCode) VALUES (
	'leonardo falango',
	'falas',
	'https://media.licdn.com/dms/image/C4D03AQE1Rm0AGcCSUw/profile-displayphoto-shrink_800_800/0/1628694636268?e=2147483647&v=beta&t=lJqAUeWNtbFak8pufZTkyXT4bbAnDYXCsI2BsZAFA_8',
	GETDATE(),
	'leo_falango@hotmail.com',
	1,
	'lInDaODeMaIs',
	'22d092102e95f74e322942f4e0ed70b50fb2f333faaf2089fcd1115e55c63aa7'
)
