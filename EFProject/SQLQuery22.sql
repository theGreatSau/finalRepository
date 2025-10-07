USE [master]
GO
/****** Object:  Database [EFDatabase]    Script Date: 8/8/2025 4:43:49 PM ******/
CREATE DATABASE [EFDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EFDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EFDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EFDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EFDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EFDatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EFDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EFDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EFDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EFDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EFDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EFDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [EFDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EFDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EFDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EFDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EFDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EFDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EFDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EFDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EFDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EFDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EFDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EFDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EFDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EFDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EFDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EFDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EFDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EFDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EFDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [EFDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EFDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EFDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EFDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EFDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EFDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [EFDatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [EFDatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EFDatabase]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permission](
	[PermID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Controller] [varchar](50) NULL,
	[Action] [varchar](50) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[PermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RoleId] [int] NULL,
	[PermId] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[IsActive] [int] NULL,
	[IsDeleted] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[RoleId] [int] NULL,
	[IsActive] [int] NULL,
	[IsDeleted] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (1, N'View', N'Users', N'Index')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (2, N'Create', N'Users', N'Create')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (3, N'Edit', N'Users', N'Edit')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (4, N'Delete', N'Users', N'Delete')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (5, N'View', N'Role', N'Index')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (6, N'Create', N'Role', N'Create')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (7, N'Edit', N'Role', N'Edit')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (8, N'Delete', N'Role', N'Delete')
GO
INSERT [dbo].[Permission] ([PermID], [Name], [Controller], [Action]) VALUES (9, N'Assing Permission', N'Role', N'AssignPermissions')
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 1)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 2)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 3)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 4)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 5)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 6)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 7)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 8)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (2, 9)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 1)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 2)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 3)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 4)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 5)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 6)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 7)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 8)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (1, 9)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 1)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 2)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 3)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 5)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 6)
GO
INSERT [dbo].[RolePermissions] ([RoleId], [PermId]) VALUES (5, 7)
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleId], [Name], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'Admin', 1, 0, 1, CAST(N'2025-08-07T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Roles] ([RoleId], [Name], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, N'Client', 1, 0, 1, CAST(N'2025-08-07T00:00:00.000' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Roles] ([RoleId], [Name], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, N'testee', 0, 1, 1, CAST(N'2025-08-08T13:15:11.927' AS DateTime), 1, CAST(N'2025-08-08T13:41:17.617' AS DateTime))
GO
INSERT [dbo].[Roles] ([RoleId], [Name], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, N'uiiuiuiui', 1, 1, 1, CAST(N'2025-08-08T15:07:13.413' AS DateTime), 1, CAST(N'2025-08-08T15:07:25.620' AS DateTime))
GO
INSERT [dbo].[Roles] ([RoleId], [Name], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, N'Tester', 1, 0, 1, CAST(N'2025-08-08T16:26:01.357' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [RoleId], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'Admin', N'admin', 1, 1, 0, 1, CAST(N'2025-08-07T00:00:00.000' AS DateTime), 1, CAST(N'2025-08-08T15:35:41.630' AS DateTime))
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [RoleId], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, N'dsdsd', N'sd', 1, 0, 1, 1, CAST(N'2025-08-07T15:20:17.820' AS DateTime), 1, CAST(N'2025-08-07T15:20:25.610' AS DateTime))
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [RoleId], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, N'zreview', N'fgfgfg', 2, 0, 0, 1, CAST(N'2025-08-08T12:18:54.253' AS DateTime), 1, CAST(N'2025-08-08T12:19:07.473' AS DateTime))
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [RoleId], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, N'ghjfdfdf', N'iui', 1, 0, 1, 1, CAST(N'2025-08-08T15:05:47.677' AS DateTime), 1, CAST(N'2025-08-08T15:07:01.780' AS DateTime))
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [RoleId], [IsActive], [IsDeleted], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, N'Tester', N'Tester', 5, 1, 0, 1, CAST(N'2025-08-08T16:26:21.607' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAllRoles]
as
begin
select RoleId,Name as RoleName from Roles where IsDeleted = 0
end
GO
/****** Object:  StoredProcedure [dbo].[GetRoleName]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRoleName]
@RoleId INT
AS
BEGIN
	SELECT * FROM Users WHERE RoleId = @RoleId and IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteRole]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteRole]
    @RoleId INT,
    @DeletedBy INT
AS
BEGIN
    UPDATE Roles
    SET IsDeleted = 1,
        UpdatedBy = @DeletedBy,
        UpdatedOn = GETDATE()
    WHERE RoleId = @RoleId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUser]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Delete User (soft delete)
CREATE PROCEDURE [dbo].[sp_DeleteUser]
    @UserId INT,
    @DeletedBy INT
AS
BEGIN
    UPDATE Users
    SET IsDeleted = 1,
        UpdatedBy = @DeletedBy,
        UpdatedOn = GETDATE()
    WHERE UserId = @UserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllRoles]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllRoles]
    @RoleName VARCHAR(20) = NULL,
    @IsActive INT,
    @PageNo INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNo - 1) * @PageSize;
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @Params NVARCHAR(MAX) = N'@RoleName VARCHAR(20), @IsActive INT, @Offset INT, @PageSize INT';

    -- Start constructing the SQL query
    SET @SQL = '
        SELECT COUNT(*) OVER() AS TotalRecords, 
               r.RoleId, 
               r.Name AS RoleName,
               r.RoleId,
               r.IsActive,
               cb.UserName AS CreatedByName, 
               r.CreatedOn, 
               ub.UserName AS UpdatedByName, 
               r.UpdatedOn
        FROM Roles r
        LEFT JOIN Users cb ON r.CreatedBy = cb.UserId
        LEFT JOIN Users ub ON r.UpdatedBy = ub.UserId
        WHERE r.IsDeleted = 0';

    IF(@RoleName IS NOT NULL AND @RoleName <> '')
    BEGIN
        SET @SQL = @SQL + ' AND r.Name LIKE ''%'' + @RoleName + ''%''';
    END

    -- Add filtering for @IsActive if provided
    IF(@IsActive IS NOT NULL)
    BEGIN
        SET @SQL = @SQL + ' AND r.IsActive = @IsActive';
    END

    -- Add pagination logic
    SET @SQL = @SQL + ' ORDER BY r.RoleId OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY';

    -- Print the final SQL for debugging (optional)
    PRINT(@SQL)

    -- Execute the dynamic SQL with parameters
    EXEC sp_executesql @SQL, @Params, @RoleName, @IsActive, @Offset, @PageSize;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllUsers]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllUsers]
    @UserName VARCHAR(20) = NULL,
    @IsActive INT,
    @PageNo INT,
    @PageSize INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Offset INT = (@PageNo - 1) * @PageSize;
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @Params NVARCHAR(MAX) = N'@UserName VARCHAR(20), @IsActive INT, @Offset INT, @PageSize INT';

    -- Start constructing the SQL query
    SET @SQL = '
        SELECT COUNT(*) OVER() AS TotalRecords, 
               u.UserId, u.UserName, 
               r.Name AS RoleName,
               r.RoleId,
               u.IsActive,
               cb.UserName AS CreatedByName, 
               u.CreatedOn, 
               ub.UserName AS UpdatedByName, 
               u.UpdatedOn
        FROM Users u
        LEFT JOIN Roles r ON u.RoleId = r.RoleId
        LEFT JOIN Users cb ON u.CreatedBy = cb.UserId
        LEFT JOIN Users ub ON u.UpdatedBy = ub.UserId
        WHERE u.IsDeleted = 0';

    -- Add filtering for @UserName if provided
    IF(@UserName IS NOT NULL AND @UserName <> '')
    BEGIN
        SET @SQL = @SQL + ' AND u.UserName LIKE ''%'' + @UserName + ''%''';
    END

    -- Add filtering for @IsActive if provided
    IF(@IsActive IS NOT NULL)
    BEGIN
        SET @SQL = @SQL + ' AND u.IsActive = @IsActive';
    END

    -- Add pagination logic
    SET @SQL = @SQL + ' ORDER BY u.UserId OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY';

    -- Print the final SQL for debugging (optional)
    PRINT(@SQL)

    -- Execute the dynamic SQL with parameters
    EXEC sp_executesql @SQL, @Params, @UserName, @IsActive, @Offset, @PageSize;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRoleById]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRoleById]
    @RoleId INT
AS
BEGIN
    SELECT RoleId, Name as RoleName, IsDeleted, CreatedBy, CreatedOn
    FROM Roles
    WHERE RoleId = @RoleId AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserById]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Get User by Id
CREATE PROCEDURE [dbo].[sp_GetUserById]
    @UserId INT
AS
BEGIN
    SELECT UserId, Username, Password, RoleId, IsDeleted, CreatedBy, CreatedOn
    FROM Users
    WHERE UserId = @UserId AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertRole]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertRole]
    @Rolename NVARCHAR(50),
	@IsActive INT,
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Roles(Name, IsActive, IsDeleted, CreatedBy, CreatedOn)
    VALUES (@Rolename, @IsActive,0, @CreatedBy, GETDATE());

    SELECT SCOPE_IDENTITY() AS NewRoleId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUser]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Insert User
CREATE PROCEDURE [dbo].[sp_InsertUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @RoleId INT,
	@IsActive INT,
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Users (Username, Password, RoleId, IsActive, IsDeleted, CreatedBy, CreatedOn)
    VALUES (@Username, @Password, @RoleId, @IsActive,0, @CreatedBy, GETDATE());

    SELECT SCOPE_IDENTITY() AS NewUserId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRole]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateRole]
    @RoleId INT,
    @RoleName NVARCHAR(50),
	@IsActive INT,
    @UpdatedBy INT
AS
BEGIN
    UPDATE Roles
    SET Name = @RoleName,
		IsActive = @IsActive,
        UpdatedBy = @UpdatedBy,
        UpdatedOn = GETDATE()
    WHERE RoleId = @RoleId AND IsDeleted = 0;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUser]    Script Date: 8/8/2025 4:43:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Update User
CREATE PROCEDURE [dbo].[sp_UpdateUser]
    @UserId INT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @RoleId INT,
	@IsActive INT,
    @UpdatedBy INT
AS
BEGIN
    UPDATE Users
    SET Username = @Username,
        Password = @Password,
        RoleId = @RoleId,
		IsActive = @IsActive,
        UpdatedBy = @UpdatedBy,
        UpdatedOn = GETDATE()
    WHERE UserId = @UserId AND IsDeleted = 0;
END
GO
USE [master]
GO
ALTER DATABASE [EFDatabase] SET  READ_WRITE 
GO
