
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 10/06/2014 12:50:11
-- Generated from EDMX file: C:\Users\Usager\Desktop\BACKUP TEMPORAIRE BD DEFECTUEUSE!!!!!!!!!!!!!!!!\a14-cash-flow\CashFlow\CashFlow\Models\aaCashFlow.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbCashFlow];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tableProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tableProjects];
GO
IF OBJECT_ID(N'[dbo].[tableUtilisateurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tableUtilisateurs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tableProjects'
CREATE TABLE [dbo].[tableProjects] (
    [Hash] nvarchar(max)  NOT NULL,
    [Titre] varchar(60)  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [Location] varchar(50)  NOT NULL,
    [MontantRecu] decimal(19,4)  NOT NULL,
    [MontantRequis] decimal(19,4)  NOT NULL,
    [DateDebut] datetime  NOT NULL,
    [DateLimite] datetime  NOT NULL,
    [Categorie] varchar(50)  NOT NULL,
    [Username] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'tableUtilisateurs'
CREATE TABLE [dbo].[tableUtilisateurs] (
    [Username] varchar(420)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(16)  NOT NULL,
    [Nom] varchar(50)  NOT NULL,
    [Description] varchar(300)  NULL,
    [PhotoProfile] varbinary(max)  NULL,
    [Location] varchar(50)  NULL,
    [Twitter] varchar(25)  NULL,
    [Facebook] varchar(60)  NULL,
    [GooglePlus] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Hash] in table 'tableProjects'
ALTER TABLE [dbo].[tableProjects]
ADD CONSTRAINT [PK_tableProjects]
    PRIMARY KEY CLUSTERED ([Hash] ASC);
GO

-- Creating primary key on [Username] in table 'tableUtilisateurs'
ALTER TABLE [dbo].[tableUtilisateurs]
ADD CONSTRAINT [PK_tableUtilisateurs]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------