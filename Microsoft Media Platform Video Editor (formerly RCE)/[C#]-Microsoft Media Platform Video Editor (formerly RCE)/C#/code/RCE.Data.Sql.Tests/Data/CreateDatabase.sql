USE [master]
GO

/****** Object:  Database [RoughCutEditor]    Script Date: 03/10/2009 16:46:42 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'RoughCutEditor')
DROP DATABASE [RoughCutEditor]
GO

USE [master]
GO

CREATE DATABASE [RoughCutEditor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RoughCutEditor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\RoughCutEditor.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RoughCutEditor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\RoughCutEditor_log.ldf' , SIZE = 1040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [RoughCutEditor] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RoughCutEditor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [RoughCutEditor] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [RoughCutEditor] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [RoughCutEditor] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [RoughCutEditor] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [RoughCutEditor] SET ARITHABORT OFF 
GO

ALTER DATABASE [RoughCutEditor] SET AUTO_CLOSE ON 
GO

ALTER DATABASE [RoughCutEditor] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [RoughCutEditor] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [RoughCutEditor] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [RoughCutEditor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [RoughCutEditor] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [RoughCutEditor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [RoughCutEditor] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [RoughCutEditor] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [RoughCutEditor] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [RoughCutEditor] SET  ENABLE_BROKER 
GO

ALTER DATABASE [RoughCutEditor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [RoughCutEditor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [RoughCutEditor] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [RoughCutEditor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [RoughCutEditor] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [RoughCutEditor] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [RoughCutEditor] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [RoughCutEditor] SET  READ_WRITE 
GO

ALTER DATABASE [RoughCutEditor] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [RoughCutEditor] SET  MULTI_USER 
GO

ALTER DATABASE [RoughCutEditor] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [RoughCutEditor] SET DB_CHAINING OFF 
GO

USE [RoughCutEditor]
GO
/****** Object:  ForeignKey [FK_Container_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container]'))
ALTER TABLE [dbo].[Container] DROP CONSTRAINT [FK_Container_Container]
GO
/****** Object:  ForeignKey [FK_Container_Items_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items] DROP CONSTRAINT [FK_Container_Items_Container]
GO
/****** Object:  ForeignKey [FK_Container_Items_Item]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items] DROP CONSTRAINT [FK_Container_Items_Item]
GO
/****** Object:  ForeignKey [FK_Resource_Item]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] DROP CONSTRAINT [FK_Resource_Item]
GO
/****** Object:  ForeignKey [FK_Project_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Container]
GO
/****** Object:  ForeignKey [FK_ImageFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageFormat]'))
ALTER TABLE [dbo].[ImageFormat] DROP CONSTRAINT [FK_ImageFormat_Resource]
GO
/****** Object:  ForeignKey [FK_AudioFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AudioFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[AudioFormat]'))
ALTER TABLE [dbo].[AudioFormat] DROP CONSTRAINT [FK_AudioFormat_Resource]
GO
/****** Object:  ForeignKey [FK_VideoFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoFormat]'))
ALTER TABLE [dbo].[VideoFormat] DROP CONSTRAINT [FK_VideoFormat_Resource]
GO
/****** Object:  ForeignKey [FK_Track_Project]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] DROP CONSTRAINT [FK_Track_Project]
GO
/****** Object:  ForeignKey [FK_Title_Project]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] DROP CONSTRAINT [FK_Title_Project]
GO
/****** Object:  ForeignKey [FK_Title_TitleTemplate]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_TitleTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] DROP CONSTRAINT [FK_Title_TitleTemplate]
GO
/****** Object:  ForeignKey [FK_Shot_Item]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] DROP CONSTRAINT [FK_Shot_Item]
GO
/****** Object:  ForeignKey [FK_Shot_Track]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Track]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] DROP CONSTRAINT [FK_Shot_Track]
GO
/****** Object:  ForeignKey [FK_Comment_Project]    Script Date: 03/25/2009 14:59:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Project]
GO
/****** Object:  ForeignKey [FK_Comment_Shot]    Script Date: 03/25/2009 14:59:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Shot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Shot]
GO
/****** Object:  StoredProcedure [dbo].[CleanUpDatabase]    Script Date: 03/25/2009 15:03:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CleanUpDatabase]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CleanUpDatabase]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 03/25/2009 14:59:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Project]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Shot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Shot]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
DROP TABLE [dbo].[Comment]
GO
/****** Object:  Table [dbo].[Shot]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] DROP CONSTRAINT [FK_Shot_Item]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Track]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] DROP CONSTRAINT [FK_Shot_Track]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shot]') AND type in (N'U'))
DROP TABLE [dbo].[Shot]
GO
/****** Object:  Table [dbo].[Title]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] DROP CONSTRAINT [FK_Title_Project]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_TitleTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] DROP CONSTRAINT [FK_Title_TitleTemplate]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Title]') AND type in (N'U'))
DROP TABLE [dbo].[Title]
GO
/****** Object:  Table [dbo].[Track]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] DROP CONSTRAINT [FK_Track_Project]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Track]') AND type in (N'U'))
DROP TABLE [dbo].[Track]
GO
/****** Object:  Table [dbo].[VideoFormat]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoFormat]'))
ALTER TABLE [dbo].[VideoFormat] DROP CONSTRAINT [FK_VideoFormat_Resource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VideoFormat]') AND type in (N'U'))
DROP TABLE [dbo].[VideoFormat]
GO
/****** Object:  Table [dbo].[AudioFormat]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AudioFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[AudioFormat]'))
ALTER TABLE [dbo].[AudioFormat] DROP CONSTRAINT [FK_AudioFormat_Resource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AudioFormat]') AND type in (N'U'))
DROP TABLE [dbo].[AudioFormat]
GO
/****** Object:  Table [dbo].[ImageFormat]    Script Date: 03/25/2009 14:59:49 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageFormat]'))
ALTER TABLE [dbo].[ImageFormat] DROP CONSTRAINT [FK_ImageFormat_Resource]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageFormat]') AND type in (N'U'))
DROP TABLE [dbo].[ImageFormat]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_Project_Container]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Project]') AND type in (N'U'))
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] DROP CONSTRAINT [FK_Resource_Item]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
DROP TABLE [dbo].[Resource]
GO
/****** Object:  Table [dbo].[TitleTemplate]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TitleTemplate]') AND type in (N'U'))
DROP TABLE [dbo].[TitleTemplate]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 03/25/2009 14:59:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
DROP TABLE [dbo].[Item]
GO
/****** Object:  Table [dbo].[Container]    Script Date: 03/25/2009 14:59:47 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container]'))
ALTER TABLE [dbo].[Container] DROP CONSTRAINT [FK_Container_Container]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Container]') AND type in (N'U'))
DROP TABLE [dbo].[Container]
GO
/****** Object:  Table [dbo].[Container]    Script Date: 03/25/2009 14:59:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Container]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Container](
	[Id] [integer] NOT NULL,
	[Title] [varchar](255) NULL,
	[ParentContainerId] [integer] NULL,
 CONSTRAINT [PK_MediaBin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Item]    Script Date: 03/25/2009 14:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Item]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Item](
	[Id] [integer] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[ItemType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Container_Items]    Script Date: 03/25/2009 15:08:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Container_Items]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Container_Items](
	[ContainerId] [integer] NOT NULL,
	[ItemId] [integer] NOT NULL,
 CONSTRAINT [PK_MediaBin_Items] PRIMARY KEY CLUSTERED 
(
	[ContainerId] ASC,
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TitleTemplate]    Script Date: 03/25/2009 14:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TitleTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TitleTemplate](
	[Id] [integer] NOT NULL,
	[TemplateName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TitleTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 03/25/2009 14:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Resource]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Resource](
	[Id] [integer] NOT NULL,
	[Ref] [varchar](max) NOT NULL,
	[ResourceType] [varchar](50) NOT NULL,
	[ItemId] [integer] NOT NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 03/25/2009 14:59:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Project]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Project](
	[Id] [integer] NOT NULL,
	[Duration] [float] NULL,
	[Resolution] [varchar](10) NULL,
	[MediaBinId] [integer] NULL,
	[AutoSaveInterval] [decimal](5, 2) NULL,
	[StartTimeCode] [float] NULL,
	[SmpteFrameRate] [varchar](255) NULL,
	[RippleMode] [bit] NULL,
	[Creator] [varchar](255) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Name] [varchar](100) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImageFormat]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ImageFormat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ImageFormat](
	[Id] [integer] NOT NULL,
	[ResolutionX] [int] NULL,
	[ResolutionY] [int] NULL,
 CONSTRAINT [PK_ImageFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AudioFormat]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AudioFormat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AudioFormat](
	[Id] [integer] NOT NULL,
	[Duration] [float] NULL,
 CONSTRAINT [PK_AudioFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[VideoFormat]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VideoFormat]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VideoFormat](
	[Id] [integer] NOT NULL,
	[Duration] [float] NULL,
	[FrameRate] [varchar](50) NULL,
	[ResolutionX] [int] NULL,
	[ResolutionY] [int] NULL,
 CONSTRAINT [PK_VideoFormat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Track]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Track]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Track](
	[Id] [integer] NOT NULL,
	[TrackType] [varchar](20) NOT NULL,
	[ProjectId] [integer] NOT NULL,
 CONSTRAINT [PK_Track] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Title]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Title]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Title](
	[Id] [integer] NOT NULL,
	[TitleTemplateId] [integer] NOT NULL,
	[TrackMarkIn] [float] NOT NULL,
	[TrackMarkOut] [float] NOT NULL,
	[MainText] [varchar](255) NOT NULL,
	[SubText] [varchar](255) NOT NULL,
	[ProjectId] [integer] NOT NULL,
 CONSTRAINT [PK_Title_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Shot]    Script Date: 03/25/2009 14:59:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shot]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Shot](
	[Id] [integer] NOT NULL,
	[ItemMarkIn] [float] NULL,
	[ItemMarkOut] [float] NULL,
	[TrackMarkIn] [float] NULL,
	[TrackMarkOut] [float] NULL,
	[ItemId] [integer] NOT NULL,
	[TrackId] [integer] NOT NULL,
	[Volume] [decimal](5, 2) NULL,
 CONSTRAINT [PK_Shot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 03/25/2009 14:59:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Comment](
	[Id] [integer] NOT NULL,
	[Text] [varchar](max) NOT NULL,
	[CommentType] [varchar](10) NOT NULL,
	[Creator] [varchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
	[MarkIn] [float] NULL,
	[MarkOut] [float] NULL,
	[ProjectId] [integer] NOT NULL,
	[ShotId] [integer] NULL,
	[Strokes] [varchar](max) NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[CleanUpDatabase]    Script Date: 03/25/2009 15:03:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CleanUpDatabase]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[CleanUpDatabase] 
AS
BEGIN
	SET NOCOUNT ON;

    delete from Comment
	delete from Shot
	delete from Track
	delete from Container_items where ContainerId <> ''http://rce.litwareinc.com/samples/2.0/Containers/1C15D40B-F54B-4b43-B32B-97E1070BAFF3''
	and ContainerId <> ''http://rce.litwareinc.com/samples/2.0/Containers/1C15D40B-F54B-4b43-B32B-97E1070BAFF3''
	delete from Container where Id <> ''http://rce.litwareinc.com/samples/2.0/Containers/1C15D40B-F54B-4b43-B32B-97E1070BAFF3''
	and Id <> ''http://rce.litwareinc.com/samples/2.0/Containers/1C15D40B-F54B-4b43-B32B-97E1070BAFF3''
	delete from Project
END
' 
END
GO
/****** Object:  ForeignKey [FK_Container_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container]'))
ALTER TABLE [dbo].[Container]  WITH CHECK ADD  CONSTRAINT [FK_Container_Container] FOREIGN KEY([ParentContainerId])
REFERENCES [dbo].[Container] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container]'))
ALTER TABLE [dbo].[Container] CHECK CONSTRAINT [FK_Container_Container]
GO
/****** Object:  ForeignKey [FK_Container_Items_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items]  WITH CHECK ADD  CONSTRAINT [FK_Container_Items_Container] FOREIGN KEY([ContainerId])
REFERENCES [dbo].[Container] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items] CHECK CONSTRAINT [FK_Container_Items_Container]
GO
/****** Object:  ForeignKey [FK_Container_Items_Item]    Script Date: 03/25/2009 14:59:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items]  WITH CHECK ADD  CONSTRAINT [FK_Container_Items_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Container_Items_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Container_Items]'))
ALTER TABLE [dbo].[Container_Items] CHECK CONSTRAINT [FK_Container_Items_Item]
GO
/****** Object:  ForeignKey [FK_Resource_Item]    Script Date: 03/25/2009 14:59:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource]  WITH CHECK ADD  CONSTRAINT [FK_Resource_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Resource_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Resource]'))
ALTER TABLE [dbo].[Resource] CHECK CONSTRAINT [FK_Resource_Item]
GO
/****** Object:  ForeignKey [FK_Project_Container]    Script Date: 03/25/2009 14:59:48 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Container] FOREIGN KEY([MediaBinId])
REFERENCES [dbo].[Container] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Project_Container]') AND parent_object_id = OBJECT_ID(N'[dbo].[Project]'))
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Container]
GO
/****** Object:  ForeignKey [FK_ImageFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageFormat]'))
ALTER TABLE [dbo].[ImageFormat]  WITH CHECK ADD  CONSTRAINT [FK_ImageFormat_Resource] FOREIGN KEY([Id])
REFERENCES [dbo].[Resource] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ImageFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[ImageFormat]'))
ALTER TABLE [dbo].[ImageFormat] CHECK CONSTRAINT [FK_ImageFormat_Resource]
GO
/****** Object:  ForeignKey [FK_AudioFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AudioFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[AudioFormat]'))
ALTER TABLE [dbo].[AudioFormat]  WITH CHECK ADD  CONSTRAINT [FK_AudioFormat_Resource] FOREIGN KEY([Id])
REFERENCES [dbo].[Resource] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AudioFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[AudioFormat]'))
ALTER TABLE [dbo].[AudioFormat] CHECK CONSTRAINT [FK_AudioFormat_Resource]
GO
/****** Object:  ForeignKey [FK_VideoFormat_Resource]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoFormat]'))
ALTER TABLE [dbo].[VideoFormat]  WITH CHECK ADD  CONSTRAINT [FK_VideoFormat_Resource] FOREIGN KEY([Id])
REFERENCES [dbo].[Resource] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VideoFormat_Resource]') AND parent_object_id = OBJECT_ID(N'[dbo].[VideoFormat]'))
ALTER TABLE [dbo].[VideoFormat] CHECK CONSTRAINT [FK_VideoFormat_Resource]
GO
/****** Object:  ForeignKey [FK_Track_Project]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track]  WITH CHECK ADD  CONSTRAINT [FK_Track_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Track_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Track]'))
ALTER TABLE [dbo].[Track] CHECK CONSTRAINT [FK_Track_Project]
GO
/****** Object:  ForeignKey [FK_Title_Project]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title]  WITH CHECK ADD  CONSTRAINT [FK_Title_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] CHECK CONSTRAINT [FK_Title_Project]
GO
/****** Object:  ForeignKey [FK_Title_TitleTemplate]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_TitleTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title]  WITH CHECK ADD  CONSTRAINT [FK_Title_TitleTemplate] FOREIGN KEY([TitleTemplateId])
REFERENCES [dbo].[TitleTemplate] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Title_TitleTemplate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Title]'))
ALTER TABLE [dbo].[Title] CHECK CONSTRAINT [FK_Title_TitleTemplate]
GO
/****** Object:  ForeignKey [FK_Shot_Item]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot]  WITH CHECK ADD  CONSTRAINT [FK_Shot_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Item]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] CHECK CONSTRAINT [FK_Shot_Item]
GO
/****** Object:  ForeignKey [FK_Shot_Track]    Script Date: 03/25/2009 14:59:49 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Track]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot]  WITH CHECK ADD  CONSTRAINT [FK_Shot_Track] FOREIGN KEY([TrackId])
REFERENCES [dbo].[Track] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Shot_Track]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shot]'))
ALTER TABLE [dbo].[Shot] CHECK CONSTRAINT [FK_Shot_Track]
GO
/****** Object:  ForeignKey [FK_Comment_Project]    Script Date: 03/25/2009 14:59:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Project]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Project]
GO
/****** Object:  ForeignKey [FK_Comment_Shot]    Script Date: 03/25/2009 14:59:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Shot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Shot] FOREIGN KEY([ShotId])
REFERENCES [dbo].[Shot] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Shot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Shot]
GO

CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE]
GO
CREATE USER [BUILTIN\IIS_IUSRS] FOR LOGIN [BUILTIN\IIS_IUSRS]
GO
EXEC sp_addrolemember 'db_owner', 'NT AUTHORITY\NETWORK SERVICE'
GO
EXEC sp_addrolemember 'db_owner', 'BUILTIN\IIS_IUSRS'
GO
