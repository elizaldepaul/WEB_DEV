
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/14/2023 12:53:52
-- Generated from EDMX file: C:\Users\User\Documents\gege\WebApplication1\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_user_role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[user] DROP CONSTRAINT [FK_user_role];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[activity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[activity];
GO
IF OBJECT_ID(N'[dbo].[role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[role];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[user]', 'U') IS NOT NULL
    DROP TABLE [dbo].[user];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'roles'
CREATE TABLE [dbo].[roles] (
    [role_id] int IDENTITY(1,1) NOT NULL,
    [role1] nchar(10)  NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [user_id] int IDENTITY(1,1) NOT NULL,
    [first_name] varchar(255)  NULL,
    [last_name] varchar(255)  NULL,
    [age] int  NULL,
    [address] varchar(255)  NULL,
    [gender] varchar(20)  NULL,
    [email] varchar(50)  NULL,
    [password] varchar(25)  NULL,
    [role_id] int  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'activities'
CREATE TABLE [dbo].[activities] (
    [activity_id] int IDENTITY(1,1) NOT NULL,
    [activity_name] varchar(255)  NULL,
    [activity_date] varchar(255)  NULL,
    [activity_time] time  NULL,
    [activity_location] varchar(255)  NULL,
    [activity_ootd] varchar(255)  NULL,
    [user_id] int  NULL,
    [remarks] varchar(max)  NULL,
    [activity_status] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [role_id] in table 'roles'
ALTER TABLE [dbo].[roles]
ADD CONSTRAINT [PK_roles]
    PRIMARY KEY CLUSTERED ([role_id] ASC);
GO

-- Creating primary key on [user_id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([user_id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [activity_id] in table 'activities'
ALTER TABLE [dbo].[activities]
ADD CONSTRAINT [PK_activities]
    PRIMARY KEY CLUSTERED ([activity_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [role_id] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [FK_user_role]
    FOREIGN KEY ([role_id])
    REFERENCES [dbo].[roles]
        ([role_id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_user_role'
CREATE INDEX [IX_FK_user_role]
ON [dbo].[users]
    ([role_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------