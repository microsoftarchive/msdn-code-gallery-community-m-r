USE [master]
GO
/****** Object:  Database [dbProyecto]    Script Date: 04/06/2014 7:49:18 p. m. ******/
CREATE DATABASE [dbProyecto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbProyecto', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbProyecto.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbProyecto_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\dbProyecto_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbProyecto] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbProyecto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbProyecto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbProyecto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbProyecto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbProyecto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbProyecto] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbProyecto] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [dbProyecto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbProyecto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbProyecto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbProyecto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbProyecto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbProyecto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbProyecto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbProyecto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbProyecto] SET  DISABLE_BROKER 
GO
ALTER DATABASE [dbProyecto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbProyecto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbProyecto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbProyecto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbProyecto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbProyecto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbProyecto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbProyecto] SET RECOVERY FULL 
GO
ALTER DATABASE [dbProyecto] SET  MULTI_USER 
GO
ALTER DATABASE [dbProyecto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbProyecto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbProyecto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbProyecto] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [dbProyecto]
GO
/****** Object:  User [proyecto]    Script Date: 04/06/2014 7:49:18 p. m. ******/
CREATE USER [proyecto] FOR LOGIN [proyecto] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [proyecto]
GO
/****** Object:  Table [dbo].[tblMenu]    Script Date: 04/06/2014 7:49:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMenu](
	[intIdMenu] [int] IDENTITY(1,1) NOT NULL,
	[strDescripcion] [varchar](80) NOT NULL,
	[intIdPadre] [int] NOT NULL,
	[intPosicion] [int] NOT NULL,
	[bitActivo] [bit] NOT NULL,
	[strUrl] [varchar](200) NULL,
	[dtmFechaCreacion] [datetime] NULL CONSTRAINT [DF_tblMenu_dtmFechaCreacion]  DEFAULT (getdate()),
	[strUsuarioCreador] [varchar](50) NULL,
	[dtmFechaModificacion] [datetime] NULL CONSTRAINT [DF_tblMenu_dtmFechaModificacion]  DEFAULT (getdate()),
	[strUsuarioModificador] [varchar](50) NULL,
	[bitAdministracion] [bit] NOT NULL CONSTRAINT [DF_tblMenu_bitAdministracion]  DEFAULT ((0))
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_ConsultarMenu]    Script Date: 04/06/2014 7:49:19 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[usp_ConsultarMenu]
 
as

SELECT intIdMenu, strDescripcion, intPosicion, intIdPadre, bitActivo, strUrl,bitAdministracion
FROM dbo.tblMenu
WHERE bitactivo = 1
ORDER BY intPosicion

GO
USE [master]
GO
ALTER DATABASE [dbProyecto] SET  READ_WRITE 
GO
