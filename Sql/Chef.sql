/*******************************************************************************
  Chef.me Schema v1.0
  Author Ray Fan (@FanrayMedia)
  Tables prefixed AspNet are from AspNet Identity 2.0
********************************************************************************/

USE [ChefMe]
GO

/*******************************************************************************
Drop FKs and tables if exists!
********************************************************************************/
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.Profile_dbo.ProfileStyle_ProfileStyle_Id]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Profile] DROP CONSTRAINT [FK_dbo.Profile_dbo.ProfileStyle_ProfileStyle_Id]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[FK_dbo.GroupItem_dbo.Group]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[GroupItem] DROP CONSTRAINT [FK_dbo.GroupItem_dbo.Group]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[AspNetUsers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[AspNetUsers]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[AspNetUserRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[AspNetUserRoles]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[AspNetUserLogins]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[AspNetUserLogins]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[AspNetUserClaims]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[AspNetUserClaims]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[AspNetRoles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[AspNetRoles]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[ProfileStyle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[ProfileStyle]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Profile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[Profile]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[GroupItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[GroupItem]
GO
if exists (select * from dbo.sysobjects where id = object_id(N'[Group]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [dbo].[Group]
GO

/*******************************************************************************

Identity System Tables, modified PK to nonclustered and added clustered index

********************************************************************************/

CREATE TABLE [dbo].[AspNetRoles](
  [Id] [nvarchar](128) NOT NULL
, [Name] [nvarchar](256) NOT NULL
)
GO
ALTER TABLE [dbo].[AspNetRoles] ADD CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY NONCLUSTERED ([Id] ASC)
GO
CREATE UNIQUE CLUSTERED INDEX [CIX_dbo.AspNetRoles] ON [dbo].[AspNetRoles]([Name] ASC) 
GO 

/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[AspNetUserClaims](
  [Id] [int] IDENTITY(1,1) NOT NULL
, [UserId] [nvarchar](128) NOT NULL
, [ClaimType] [nvarchar](max) NULL
, [ClaimValue] [nvarchar](max) NULL
)
GO
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
GO

/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[AspNetUserLogins](
  [LoginProvider] [nvarchar](128) NOT NULL
, [ProviderKey] [nvarchar](128) NOT NULL
, [UserId] [nvarchar](128) NOT NULL
)
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC,[ProviderKey] ASC,[UserId] ASC)
GO

/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[AspNetUserRoles](
  [UserId] [nvarchar](128) NOT NULL
, [RoleId] [nvarchar](128) NOT NULL
)
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC,[RoleId] ASC)
GO

/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[AspNetUsers](
  [Id] [nvarchar](128) NOT NULL
, [Email] [nvarchar](256) NULL 
, [EmailConfirmed] [bit] NOT NULL
, [PasswordHash] [nvarchar](max) NULL
, [SecurityStamp] [nvarchar](max) NULL
, [PhoneNumber] [nvarchar](max) NULL 
, [PhoneNumberConfirmed] [bit] NOT NULL
, [TwoFactorEnabled] [bit] NOT NULL
, [LockoutEndDateUtc] [datetime] NULL
, [LockoutEnabled] [bit] NOT NULL
, [AccessFailedCount] [int] NOT NULL
, [UserName] [nvarchar](256) NOT NULL 
)
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY NONCLUSTERED ([Id])
GO
CREATE UNIQUE CLUSTERED INDEX [CIX_dbo.AspNetUsers] ON [dbo].[AspNetUsers]([UserName] ASC)
GO

-- ------------------------ fks

ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO

ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO

/*******************************************************************************
My tables
********************************************************************************/

CREATE TABLE [dbo].[Profile](
  [Id] [int] IDENTITY(1,1) NOT NULL
, [UserName] [nvarchar](64) NOT NULL -- so we can search Profile without involve AspNetUsers table
, [Email] [nvarchar](256) NOT NULL -- 
, [FirstName] [nvarchar](64) NOT NULL
, [LastName] [nvarchar](64) NOT NULL
, [BgImg]  [nvarchar](64) NOT NULL -- filename with ext A01.jpg, 
, [Headline] [nvarchar](256) NULL
, [Bio] [nvarchar](max) NULL
, [Locations] [nvarchar](450) NOT NULL 
, [EnableGravatar] bit NOT NULL -- false
, [AllowEmailMe] [bit] NOT NULL -- true
, [AccountStatus] [tinyint] NOT NULL
, [LastSeenOn] [datetime] NOT NULL
, [JoinedOn] [date] NOT NULL -- 
, [ViewCount] [int] NOT NULL
, [ProfileStyle_Id] [int] NOT NULL -- ProfileStyle
)
GO
ALTER TABLE [dbo].[Profile] ADD CONSTRAINT [PK_dbo.Profile] PRIMARY KEY NONCLUSTERED ([Id])
GO
CREATE UNIQUE CLUSTERED INDEX [CIX_dbo.Profile] ON [dbo].[Profile]([UserName] ASC)
GO


/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[ProfileStyle](
  [Id] [int] IDENTITY(1,1) NOT NULL
  -- profile box
, [Profbox_x] int NOT NULL -- 0
, [Profbox_y] int NOT NULL -- 0
, [Profbox_opacity] int NOT NULL -- 40
, [Profbox_bgcolor] [nvarchar](16) NOT NULL -- #000
  -- edit box
, [Name_color] [nvarchar](16) NOT NULL -- #eee
, [Name_font] [nvarchar](32) NOT NULL -- Arial
, [Name_size] int NOT NULL -- 66
, [Headline_color] [nvarchar](16) NOT NULL
, [Headline_font] [nvarchar](32) NOT NULL
, [Headline_size] int NOT NULL -- 24
, [Bio_color] [nvarchar](16) NOT NULL
, [Bio_font] [nvarchar](32) NOT NULL
, [Bio_size] int NOT NULL -- 18
, [Links_color] [nvarchar](16) NOT NULL
, [Links_font] [nvarchar](32) NOT NULL
, [Links_size] int NOT NULL -- 15
)
GO
ALTER TABLE [dbo].[ProfileStyle] ADD CONSTRAINT [PK_dbo.ProfileStyle] PRIMARY KEY CLUSTERED ([Id])
GO

-- FK
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Profile_dbo.ProfileStyle_ProfileStyle_Id] FOREIGN KEY([ProfileStyle_Id])
REFERENCES [dbo].[ProfileStyle] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_dbo.Profile_dbo.ProfileStyle_ProfileStyle_Id]
GO

/* ---------------------------------------------------------------------------- */

CREATE TABLE [dbo].[Group] (
  [Id] int NOT NULL  IDENTITY (1,1)
, [UserName] nvarchar(64) NOT NULL
, [Name] nvarchar(64) NOT NULL
, [Slug] nvarchar(64) NOT NULL
, [GroupType] tinyint NOT NULL -- different collections for users/recipes/posts etc. up to 255 max
, [IsPublic] [bit] NOT NULL
);
GO
ALTER TABLE [dbo].[Group] ADD CONSTRAINT [PK_dbo.Group] PRIMARY KEY NONCLUSTERED ([Id])
GO
CREATE UNIQUE CLUSTERED INDEX [CIX_dbo.Group] ON [dbo].[Group] ([UserName],[Name])
GO


CREATE TABLE [dbo].[GroupItem](
  [GroupId] int NOT NULL
, [ItemId] int NOT NULL -- was [ContactName]
, [AddedOn] [smalldatetime] NOT NULL
);
GO
ALTER TABLE [dbo].[GroupItem] ADD CONSTRAINT [PK_dbo.GroupItem] PRIMARY KEY CLUSTERED ([GroupId] ASC,[ItemId] ASC)
GO
ALTER TABLE [dbo].[GroupItem]  ADD  CONSTRAINT [FK_dbo.GroupItem_dbo.Group] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
ON DELETE CASCADE
GO

/*******************************************************************************
Data
********************************************************************************/

USE [ChefMe]
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e4581749-a765-4525-8101-7a62ef840b8a', N'AlexisSoyer1810@notset.com', 0, NULL, N'8aa64e0a-88f3-4768-a160-6ad71ac62870', NULL, 0, 0, NULL, 0, 0, N'AlexisSoyer1810')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4b3900a6-33f5-454d-97b5-a9161c74e0a0', N'GuillaumeTirel1310@notset.com', 0, NULL, N'03b268fd-ad19-4c2d-8285-87809ef251cb', NULL, 0, 0, NULL, 0, 0, N'GuillaumeTirel1310')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6104a57a-db40-446f-9848-55f13c926229', N'JoyceChen1917@notset.com', 0, NULL, N'46a593da-37b8-4074-a1d6-a41c254e4f2f', NULL, 0, 0, NULL, 0, 0, N'JoyceChen1917')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9b767f83-17e0-4f0e-aa9d-7244d4aa2804', N'JuliaChild1912@notset.com', 0, NULL, N'b266c4a6-1df4-4542-ac88-8f698bbdb0af', NULL, 0, 0, NULL, 0, 0, N'JuliaChild1912')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'29129f73-d4ea-4d87-8d88-c01991b621ee', N'MaestroMartino1430@notset.com', 0, NULL, N'48c57ae5-1460-4bc5-ba57-f9fbcafd08e1', NULL, 0, 0, NULL, 0, 0, N'MaestroMartino1430')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'bcdc6de7-bfca-437d-aea5-668263093bef', N'MarcelBoulestin1878@notset.com', 0, NULL, N'08682ed1-c0c2-4986-883d-27efddfe533b', NULL, 0, 0, NULL, 0, 0, N'MarcelBoulestin1878')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'33229172-72ba-404e-8fa9-6fdb0dc82918', N'NicolasAppert1749@notset.com', 0, NULL, N'bde9eabf-d7e0-4478-9ed3-6dddc158e5f6', NULL, 0, 0, NULL, 0, 0, N'NicolasAppert1749')
GO
SET IDENTITY_INSERT [dbo].[ProfileStyle] ON 

GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (1, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (2, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (3, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (4, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (5, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (6, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
INSERT [dbo].[ProfileStyle] ([Id], [Profbox_x], [Profbox_y], [Profbox_opacity], [Profbox_bgcolor], [Name_color], [Name_font], [Name_size], [Headline_color], [Headline_font], [Headline_size], [Bio_color], [Bio_font], [Bio_size], [Links_color], [Links_font], [Links_size]) VALUES (7, 0, 0, 40, N'#000', N'#eee', N'Arial', 66, N'#eee', N'Arial', 24, N'#eee', N'Arial', 18, N'#eee', N'Arial', 16)
GO
SET IDENTITY_INSERT [dbo].[ProfileStyle] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (7, N'AlexisSoyer1810', N'AlexisSoyer1810@notset.com', N'Alexis', N'Soyer', N'A09.jpg', N'The first celebrity chef', N'French chef who became the most celebrated cook in Victorian England and was arguably the first celebrity chef. He also tried to alleviate suffering of the Irish poor in the Great Irish Famine (1845–1849), and improve the food provided to British soldiers in the Crimean War.', N'Meaux, France', 0, 1, 1, CAST(N'2015-11-24 19:48:54.523' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 7)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (3, N'GuillaumeTirel1310', N'GuillaumeTirel1310@notset.com', N'Guillaume', N'Tirel', N'A04.jpg', N'A.K.A Taillevent', N'From 1326 he was queux, head chef, to Philip VI. In 1347, he became squire to the Dauphin de Viennois and his queux in 1349. In 1355 he became squire to the Duke of Normandy, in 1359 his queux and in 1361 his sergeant-at-arms.', N'Upper Normandy, France', 0, 1, 1, CAST(N'2015-11-24 19:48:54.483' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 3)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (2, N'JoyceChen1917', N'JoyceChen1917@notset.com', N'Joyce', N'Chen', N'A03.jpg', N'Chinese chef, restaurateur, author, television personality, and entrepreneur', N'Joyce Chen was credited with popularizing northern-style Chinese cuisine in the United States, coining the name "Peking Raviolis" for potstickers, inventing and holding the patent to the flat bottom wok with handle (also known as a stir fry pan), and developing the first line of bottled Chinese stir fry sauces for the US market. Starting in 1958, she operated several popular Chinese restaurants in Cambridge, Massachusetts. She died of Alzheimer''s disease in 1994. Four years after her death, Joyce Chen was included in the 1998 James Beard Foundation Hall of Fame. In 2012, the city of Cambridge held their first Central Square "Festival of Dumplings" in honor of Joyce Chen''s birthday.', N'Beijing, China', 0, 1, 1, CAST(N'2015-11-24 19:48:54.473' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 2)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (1, N'JuliaChild1912', N'JuliaChild1912@notset.com', N'Julia', N'Child', N'A02.jpg', N'American chef, author, and television personality', N'She is recognized for bringing French cuisine to the American public with her debut cookbook, Mastering the Art of French Cooking, and her subsequent television programs, the most notable of which was The French Chef, which premiered in 1963.', N'Pasadena, CA, USA', 0, 1, 1, CAST(N'2015-11-24 19:48:50.630' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 1)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (4, N'MaestroMartino1430', N'MaestroMartino1430@notset.com', N'Maestro', N'Martino', N'A05.jpg', N'The prince of cooks', N'Italian 15th-century culinary expert who was unequalled in his field at the time and could be considered the Western world''s first celebrity chef.', N'Torre, Blenio, Switzerland', 0, 1, 1, CAST(N'2015-11-24 19:48:54.497' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 4)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (6, N'MarcelBoulestin1878', N'MarcelBoulestin1878@notset.com', N'Marcel', N'Boulestin', N'A08.jpg', N'Perfect and récherché dinner to be found in all London', N'He was a French chef, restaurateur, and the author of cookery books that popularised French cuisine in the English-speaking world.', N'Poitiers, France', 0, 1, 1, CAST(N'2015-11-24 19:48:54.517' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 6)
GO
INSERT [dbo].[Profile] ([Id], [UserName], [Email], [FirstName], [LastName], [BgImg], [Headline], [Bio], [Locations], [EnableGravatar], [AllowEmailMe], [AccountStatus], [LastSeenOn], [JoinedOn], [ViewCount], [ProfileStyle_Id]) VALUES (5, N'NicolasAppert1749', N'NicolasAppert1749@notset.com', N'Nicolas', N'Appert', N'A07.jpg', N'The father of canning', N'He was the French inventor of airtight food preservation. Appert, known as the "father of canning", was a confectioner.', N'Châlons-en-Champagne, France', 0, 1, 1, CAST(N'2015-11-24 19:48:54.507' AS DateTime), CAST(N'2015-11-24' AS Date), 0, 5)
GO
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[Group] ON 

GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (14, N'AlexisSoyer1810', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (13, N'AlexisSoyer1810', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (6, N'GuillaumeTirel1310', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (5, N'GuillaumeTirel1310', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (4, N'JoyceChen1917', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (3, N'JoyceChen1917', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (2, N'JuliaChild1912', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (1, N'JuliaChild1912', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (8, N'MaestroMartino1430', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (7, N'MaestroMartino1430', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (12, N'MarcelBoulestin1878', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (11, N'MarcelBoulestin1878', N'Favorite', N'favorite', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (10, N'NicolasAppert1749', N'Family', N'family', 0, 1)
GO
INSERT [dbo].[Group] ([Id], [UserName], [Name], [Slug], [GroupType], [IsPublic]) VALUES (9, N'NicolasAppert1749', N'Favorite', N'favorite', 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Group] OFF
GO
