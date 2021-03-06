USE [master]
GO
/****** Object:  Database [tpce]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE DATABASE [tpce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tpce', FILENAME = N'D:\SqlServer\Database\Data\tpce.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'tpce_log', FILENAME = N'D:\SqlServer\Database\Data\tpce_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [tpce] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tpce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tpce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tpce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tpce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tpce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tpce] SET ARITHABORT OFF 
GO
ALTER DATABASE [tpce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tpce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tpce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tpce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tpce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tpce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tpce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tpce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tpce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tpce] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tpce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tpce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tpce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tpce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tpce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tpce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tpce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tpce] SET RECOVERY FULL 
GO
ALTER DATABASE [tpce] SET  MULTI_USER 
GO
ALTER DATABASE [tpce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tpce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tpce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tpce] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [tpce] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'tpce', N'ON'
GO
USE [tpce]
GO
/****** Object:  Rule [unsignedrule]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE RULE [dbo].[unsignedrule] 
AS
@range > 0

GO
/****** Object:  UserDefinedDataType [dbo].[BALANCE_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[BALANCE_T] FROM [numeric](12, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[FIN_AGG_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[FIN_AGG_T] FROM [numeric](15, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[IDENT_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[IDENT_T] FROM [bigint] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[S_COUNT_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[S_COUNT_T] FROM [numeric](15, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[S_PRICE_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[S_PRICE_T] FROM [numeric](8, 2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[S_QTY_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[S_QTY_T] FROM [numeric](6, 0) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[t1]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[t1] FROM [numeric](18, 0) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[TRADE_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[TRADE_T] FROM [numeric](15, 0) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[VALUE_T]    Script Date: 8/10/2014 11:09:41 PM ******/
CREATE TYPE [dbo].[VALUE_T] FROM [numeric](10, 2) NULL
GO
/****** Object:  Table [dbo].[Account_Permission]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account_Permission](
	[AP_CA_ID] [dbo].[IDENT_T] NOT NULL,
	[AP_ACL] [char](4) NOT NULL,
	[AP_TAX_ID] [char](20) NOT NULL,
	[AP_L_NAME] [char](25) NOT NULL,
	[AP_F_NAME] [char](20) NOT NULL,
 CONSTRAINT [PK_Account_Permission] PRIMARY KEY CLUSTERED 
(
	[AP_CA_ID] ASC,
	[AP_TAX_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[AD_ID] [dbo].[IDENT_T] NOT NULL,
	[AD_LINE1] [char](80) NULL,
	[AD_LINE2] [char](80) NULL,
	[AD_ZC_CODE] [char](12) NOT NULL,
	[AD_CTRY] [char](80) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AD_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Broker]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Broker](
	[B_ID] [dbo].[IDENT_T] NOT NULL,
	[B_ST_ID] [char](4) NOT NULL,
	[B_NAME] [char](49) NOT NULL,
	[B_NUM_TRADES] [numeric](9, 0) NOT NULL,
	[B_COMM_TOTAL] [dbo].[BALANCE_T] NOT NULL,
 CONSTRAINT [PK_Broker] PRIMARY KEY CLUSTERED 
(
	[B_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cash_Transaction]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cash_Transaction](
	[CT_T_ID] [dbo].[TRADE_T] NOT NULL,
	[CT_DTS] [datetime] NOT NULL,
	[CT_AMT] [dbo].[VALUE_T] NOT NULL,
	[CT_NAME] [char](100) NULL,
 CONSTRAINT [PK_Cash_Transaction] PRIMARY KEY CLUSTERED 
(
	[CT_T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Charge]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Charge](
	[CH_TT_ID] [char](3) NOT NULL,
	[CH_C_TIER] [numeric](1, 0) NOT NULL,
	[CH_CHRG] [dbo].[VALUE_T] NOT NULL,
 CONSTRAINT [PK_Charge] PRIMARY KEY CLUSTERED 
(
	[CH_TT_ID] ASC,
	[CH_C_TIER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Commission_Rate]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Commission_Rate](
	[CR_C_TIER] [numeric](1, 0) NOT NULL,
	[CR_TT_ID] [char](3) NOT NULL,
	[CR_EX_ID] [char](6) NOT NULL,
	[CR_FROM_QTY] [dbo].[S_QTY_T] NOT NULL,
	[CR_TO_QTY] [dbo].[S_QTY_T] NOT NULL,
	[CR_RATE] [numeric](5, 2) NOT NULL,
 CONSTRAINT [PK_Customer_Rate] PRIMARY KEY CLUSTERED 
(
	[CR_C_TIER] ASC,
	[CR_TT_ID] ASC,
	[CR_EX_ID] ASC,
	[CR_FROM_QTY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company](
	[CO_ID] [dbo].[IDENT_T] NOT NULL,
	[CO_ST_ID] [char](4) NOT NULL,
	[CO_NAME] [char](60) NOT NULL,
	[CO_IN_ID] [char](2) NOT NULL,
	[CO_SP_RATE] [char](4) NOT NULL,
	[CO_CEO] [char](46) NOT NULL,
	[CO_AD_ID] [dbo].[IDENT_T] NOT NULL,
	[CO_DESC] [char](150) NOT NULL,
	[CO_OPEN_DATE] [datetime] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Company_Competitor]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Company_Competitor](
	[CP_CO_ID] [dbo].[IDENT_T] NOT NULL,
	[CP_COMP_CO_ID] [dbo].[IDENT_T] NOT NULL,
	[CP_IN_ID] [char](2) NOT NULL,
 CONSTRAINT [PK_Company_Competitor] PRIMARY KEY CLUSTERED 
(
	[CP_CO_ID] ASC,
	[CP_COMP_CO_ID] ASC,
	[CP_IN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Customer](
	[C_ID] [dbo].[IDENT_T] NOT NULL,
	[C_TAX_ID] [char](20) NOT NULL,
	[C_ST_ID] [char](4) NOT NULL,
	[C_L_NAME] [char](25) NOT NULL,
	[C_F_NAME] [char](20) NOT NULL,
	[C_M_NAME] [char](1) NULL,
	[C_GNDR] [char](1) NULL,
	[C_TIER] [numeric](1, 0) NOT NULL,
	[C_DOB] [datetime] NOT NULL,
	[C_AD_ID] [dbo].[IDENT_T] NOT NULL,
	[C_CTRY_1] [char](3) NULL,
	[C_AREA_1] [char](3) NULL,
	[C_LOCAL_1] [nchar](10) NULL,
	[C_EXT_1] [char](5) NULL,
	[C_CTRY_2] [char](3) NULL,
	[C_AREA_2] [char](3) NULL,
	[C_LOCAL_2] [nchar](10) NULL,
	[C_EXT_2] [char](5) NULL,
	[C_CTRY_3] [char](3) NULL,
	[C_AREA_3] [char](3) NULL,
	[C_LOCAL_3] [nchar](10) NULL,
	[C_EXT_3] [char](5) NULL,
	[C_EMAIL_1] [char](50) NULL,
	[C_EMAIL_2] [char](50) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[C_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Account]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[Customer_Account](
	[CA_ID] [dbo].[IDENT_T] NOT NULL,
	[CA_B_ID] [dbo].[IDENT_T] NOT NULL,
	[CA_C_ID] [dbo].[IDENT_T] NOT NULL,
	[CA_NAME] [char](50) NULL,
	[CA_TAX_ST] [numeric](1, 0) NOT NULL,
	[CA_BAL] [nchar](10) NULL,
 CONSTRAINT [PK_Customer_Account] PRIMARY KEY CLUSTERED 
(
	[CA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Customer_Taxrate]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Taxrate](
	[CX_TX_ID] [char](4) NOT NULL,
	[CX_C_ID] [dbo].[IDENT_T] NOT NULL,
 CONSTRAINT [PK_Customer_Taxrate] PRIMARY KEY CLUSTERED 
(
	[CX_TX_ID] ASC,
	[CX_C_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Daily_Market]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Daily_Market](
	[DM_DATE] [datetime] NOT NULL,
	[DM_S_SYMB] [char](15) NOT NULL,
	[DM_CLOSE] [dbo].[S_PRICE_T] NOT NULL,
	[DM_HIGH] [dbo].[S_PRICE_T] NOT NULL,
	[DM_LOW] [dbo].[S_PRICE_T] NOT NULL,
	[DM_VOL] [dbo].[S_COUNT_T] NOT NULL,
 CONSTRAINT [PK_Daily_Market] PRIMARY KEY CLUSTERED 
(
	[DM_DATE] ASC,
	[DM_S_SYMB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Exchange]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exchange](
	[EX_ID] [char](6) NOT NULL,
	[EX_NAME] [char](100) NOT NULL,
	[EX_NUM_SYMB] [numeric](6, 0) NOT NULL,
	[EX_OPEN] [numeric](4, 0) NOT NULL,
	[EX_CLOSE] [numeric](4, 0) NOT NULL,
	[EX_DESC] [char](150) NULL,
	[EX_AD_ID] [dbo].[IDENT_T] NOT NULL,
 CONSTRAINT [PK_Exchange] PRIMARY KEY CLUSTERED 
(
	[EX_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Financial]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Financial](
	[FI_CO_ID] [dbo].[IDENT_T] NOT NULL,
	[FI_YEAR] [numeric](4, 0) NOT NULL,
	[FI_QTR] [numeric](4, 0) NOT NULL,
	[FI_QTR_START_DATE] [datetime] NOT NULL,
	[FI_REVENUE] [dbo].[FIN_AGG_T] NOT NULL,
	[FI_NET_EARN] [dbo].[FIN_AGG_T] NOT NULL,
	[FI_BASIC_EPS] [dbo].[VALUE_T] NOT NULL,
	[FI_DILUT_EPF] [dbo].[VALUE_T] NOT NULL,
	[FI_MARGIN] [dbo].[VALUE_T] NOT NULL,
	[FI_INVENTORY] [dbo].[FIN_AGG_T] NOT NULL,
	[FI_ASSETS] [dbo].[FIN_AGG_T] NOT NULL,
	[FI_LIABILITY] [dbo].[FIN_AGG_T] NOT NULL,
	[FI_OUT_BASIC] [dbo].[S_COUNT_T] NOT NULL,
	[FI_OUT_DILUT] [dbo].[S_COUNT_T] NOT NULL,
 CONSTRAINT [PK_Financial] PRIMARY KEY CLUSTERED 
(
	[FI_CO_ID] ASC,
	[FI_YEAR] ASC,
	[FI_QTR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Holding]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Holding](
	[H_T_ID] [dbo].[TRADE_T] NOT NULL,
	[H_CA_ID] [dbo].[IDENT_T] NOT NULL,
	[H_S_SYMB] [char](15) NOT NULL,
	[H_DTS] [datetime] NOT NULL,
	[H_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[H_QTY] [dbo].[S_QTY_T] NOT NULL,
 CONSTRAINT [PK_Holding] PRIMARY KEY CLUSTERED 
(
	[H_T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Holding_History]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holding_History](
	[HH_H_T_ID] [dbo].[TRADE_T] NOT NULL,
	[HH_T_ID] [dbo].[TRADE_T] NOT NULL,
	[HH_BEFORE_QTY] [dbo].[S_QTY_T] NOT NULL,
	[HH_AFTER_QTY] [dbo].[S_QTY_T] NOT NULL,
 CONSTRAINT [PK_Holding_History] PRIMARY KEY CLUSTERED 
(
	[HH_H_T_ID] ASC,
	[HH_T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Holding_Summary]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Holding_Summary](
	[HS_CA_ID] [dbo].[IDENT_T] NOT NULL,
	[HS_S_SYMB] [char](15) NOT NULL,
	[HS_QTY] [dbo].[S_QTY_T] NOT NULL,
 CONSTRAINT [PK_Holding_Summary] PRIMARY KEY CLUSTERED 
(
	[HS_CA_ID] ASC,
	[HS_S_SYMB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Industry]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Industry](
	[IN_ID] [char](2) NOT NULL,
	[IN_NAME] [char](50) NOT NULL,
	[IN_SC_ID] [char](2) NOT NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[IN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Last_Trade]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Last_Trade](
	[LT_S_SYMB] [char](15) NOT NULL,
	[LT_DTS] [datetime] NOT NULL,
	[LT_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[LT_OPEN_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[LT_VOL] [dbo].[S_COUNT_T] NOT NULL,
 CONSTRAINT [PK_List_Trade] PRIMARY KEY CLUSTERED 
(
	[LT_S_SYMB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News_Item]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News_Item](
	[NI_ID] [dbo].[IDENT_T] NOT NULL,
	[NI_HEADLINE] [char](80) NOT NULL,
	[NI_SUMMARY] [char](255) NOT NULL,
	[NI_ITEM] [varbinary](max) NOT NULL,
	[NI_DTS] [datetime] NOT NULL,
	[NI_SOURCE] [char](30) NOT NULL,
	[NI_AUTHOR] [nchar](30) NULL,
 CONSTRAINT [PK_News_Item] PRIMARY KEY CLUSTERED 
(
	[NI_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[News_Xref]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News_Xref](
	[NX_NI_ID] [dbo].[IDENT_T] NOT NULL,
	[NX_CO_ID] [dbo].[IDENT_T] NOT NULL,
 CONSTRAINT [PK_News_Xref] PRIMARY KEY CLUSTERED 
(
	[NX_NI_ID] ASC,
	[NX_CO_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sector]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sector](
	[SC_ID] [char](2) NOT NULL,
	[SC_NAME] [char](30) NOT NULL,
 CONSTRAINT [PK_SECTOR] PRIMARY KEY CLUSTERED 
(
	[SC_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Security]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Security](
	[S_SYMB] [char](15) NOT NULL,
	[S_ISSUE] [char](6) NOT NULL,
	[S_ST_ID] [char](4) NOT NULL,
	[S_NAME] [char](70) NOT NULL,
	[S_EX_ID] [char](6) NOT NULL,
	[S_CO_ID] [dbo].[IDENT_T] NOT NULL,
	[S_NUM_OUT] [dbo].[S_COUNT_T] NOT NULL,
	[S_START_DATE] [datetime] NOT NULL,
	[S_EXCH_DATE] [datetime] NOT NULL,
	[S_PE] [dbo].[VALUE_T] NOT NULL,
	[S_52WK_HIGH] [dbo].[S_PRICE_T] NOT NULL,
	[S_52WK_HIGH_DATE] [datetime] NOT NULL,
	[S_52WK_LOW] [dbo].[S_PRICE_T] NOT NULL,
	[S_52WK_LOW_DATE] [datetime] NOT NULL,
	[S_DIVIDEND] [dbo].[VALUE_T] NOT NULL,
	[S_YIELD] [numeric](5, 2) NOT NULL,
 CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED 
(
	[S_SYMB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Settlement]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Settlement](
	[SE_T_ID] [dbo].[TRADE_T] NOT NULL,
	[SE_CASH_TYPE] [char](40) NOT NULL,
	[SE_CASH_DUE_DATE] [datetime] NOT NULL,
	[SE_AMT] [dbo].[VALUE_T] NOT NULL,
 CONSTRAINT [PK_Settingment] PRIMARY KEY CLUSTERED 
(
	[SE_T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Status_Type]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Status_Type](
	[ST_ID] [char](4) NOT NULL,
	[ST_NAME] [char](10) NOT NULL,
 CONSTRAINT [PK_Status_Type] PRIMARY KEY CLUSTERED 
(
	[ST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TaxRate]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TaxRate](
	[TX_ID] [char](4) NOT NULL,
	[TX_NAME] [char](5) NOT NULL,
	[TX_RATE] [numeric](6, 5) NOT NULL,
 CONSTRAINT [PK_TaxRate] PRIMARY KEY CLUSTERED 
(
	[TX_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trade]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trade](
	[T_ID] [dbo].[TRADE_T] NOT NULL,
	[T_DTS] [datetime] NOT NULL,
	[T_ST_ID] [char](4) NOT NULL,
	[T_TT_ID] [char](3) NOT NULL,
	[T_IS_CASH] [bit] NOT NULL,
	[T_S_SYMB] [char](15) NOT NULL,
	[T_QTY] [dbo].[S_QTY_T] NOT NULL,
	[T_BID_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[T_CA_ID] [dbo].[IDENT_T] NOT NULL,
	[T_EXEC_NAME] [char](49) NOT NULL,
	[T_TRADE_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[T_CHRG] [dbo].[VALUE_T] NOT NULL,
	[T_COMM] [dbo].[VALUE_T] NOT NULL,
	[T_TAX] [dbo].[VALUE_T] NOT NULL,
	[T_LIFO] [bit] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trade_History]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trade_History](
	[TH_T_ID] [dbo].[TRADE_T] NOT NULL,
	[TH_DTS] [datetime] NOT NULL,
	[TH_ST_ID] [char](4) NOT NULL,
 CONSTRAINT [PK_Trade_History] PRIMARY KEY CLUSTERED 
(
	[TH_T_ID] ASC,
	[TH_ST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trade_Request]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trade_Request](
	[TR_T_ID] [dbo].[TRADE_T] NOT NULL,
	[TR_TT_ID] [char](3) NOT NULL,
	[TR_S_SYMB] [char](15) NOT NULL,
	[TR_QTY] [dbo].[S_QTY_T] NOT NULL,
	[TR_BID_PRICE] [dbo].[S_PRICE_T] NOT NULL,
	[TR_B_ID] [dbo].[IDENT_T] NOT NULL,
 CONSTRAINT [PK_Trade_Request] PRIMARY KEY CLUSTERED 
(
	[TR_T_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trade_Type]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trade_Type](
	[TT_ID] [char](3) NOT NULL,
	[TT_NAME] [char](12) NOT NULL,
	[TT_IS_SELL] [bit] NOT NULL,
	[TT_IS_MRKT] [bit] NOT NULL,
 CONSTRAINT [PK_Trade_Type] PRIMARY KEY CLUSTERED 
(
	[TT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Watch_Item]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Watch_Item](
	[WI_WL_ID] [dbo].[IDENT_T] NOT NULL,
	[WI_S_SYMB] [char](15) NOT NULL,
 CONSTRAINT [PK_Watch_Item] PRIMARY KEY CLUSTERED 
(
	[WI_WL_ID] ASC,
	[WI_S_SYMB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Watch_List]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Watch_List](
	[WL_ID] [dbo].[IDENT_T] NOT NULL,
	[WL_C_ID] [dbo].[IDENT_T] NOT NULL,
 CONSTRAINT [PK_Watch_List] PRIMARY KEY CLUSTERED 
(
	[WL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Zip_Code]    Script Date: 8/10/2014 11:09:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Zip_Code](
	[ZC_CODE] [char](12) NOT NULL,
	[ZC_TOWN] [char](80) NOT NULL,
	[ZC_DIV] [char](80) NOT NULL,
 CONSTRAINT [PK_Zip_Code] PRIMARY KEY CLUSTERED 
(
	[ZC_CODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Account_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Account_Permission_Customer_Account] FOREIGN KEY([AP_CA_ID])
REFERENCES [dbo].[Customer_Account] ([CA_ID])
GO
ALTER TABLE [dbo].[Account_Permission] CHECK CONSTRAINT [FK_Account_Permission_Customer_Account]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Zip_Code] FOREIGN KEY([AD_ZC_CODE])
REFERENCES [dbo].[Zip_Code] ([ZC_CODE])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Zip_Code]
GO
ALTER TABLE [dbo].[Broker]  WITH CHECK ADD  CONSTRAINT [FK_Broker_Status_Type] FOREIGN KEY([B_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Broker] CHECK CONSTRAINT [FK_Broker_Status_Type]
GO
ALTER TABLE [dbo].[Cash_Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Cash_Transaction_Trade] FOREIGN KEY([CT_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Cash_Transaction] CHECK CONSTRAINT [FK_Cash_Transaction_Trade]
GO
ALTER TABLE [dbo].[Charge]  WITH NOCHECK ADD  CONSTRAINT [FK_Charge_Trade_Type] FOREIGN KEY([CH_TT_ID])
REFERENCES [dbo].[Trade_Type] ([TT_ID])
GO
ALTER TABLE [dbo].[Charge] CHECK CONSTRAINT [FK_Charge_Trade_Type]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [FK_Commission_Rate_Exchange] FOREIGN KEY([CR_EX_ID])
REFERENCES [dbo].[Exchange] ([EX_ID])
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [FK_Commission_Rate_Exchange]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [FK_Commission_Rate_Trade_Type] FOREIGN KEY([CR_TT_ID])
REFERENCES [dbo].[Trade_Type] ([TT_ID])
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [FK_Commission_Rate_Trade_Type]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Address] FOREIGN KEY([CO_AD_ID])
REFERENCES [dbo].[Address] ([AD_ID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Address]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Industry] FOREIGN KEY([CO_IN_ID])
REFERENCES [dbo].[Industry] ([IN_ID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Industry]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_Status_Type] FOREIGN KEY([CO_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_Status_Type]
GO
ALTER TABLE [dbo].[Company_Competitor]  WITH CHECK ADD  CONSTRAINT [FK_Company_Competitor_Company] FOREIGN KEY([CP_CO_ID])
REFERENCES [dbo].[Company] ([CO_ID])
GO
ALTER TABLE [dbo].[Company_Competitor] CHECK CONSTRAINT [FK_Company_Competitor_Company]
GO
ALTER TABLE [dbo].[Company_Competitor]  WITH CHECK ADD  CONSTRAINT [FK_Company_Competitor_Company1] FOREIGN KEY([CP_COMP_CO_ID])
REFERENCES [dbo].[Company] ([CO_ID])
GO
ALTER TABLE [dbo].[Company_Competitor] CHECK CONSTRAINT [FK_Company_Competitor_Company1]
GO
ALTER TABLE [dbo].[Company_Competitor]  WITH CHECK ADD  CONSTRAINT [FK_Company_Competitor_Industry] FOREIGN KEY([CP_IN_ID])
REFERENCES [dbo].[Industry] ([IN_ID])
GO
ALTER TABLE [dbo].[Company_Competitor] CHECK CONSTRAINT [FK_Company_Competitor_Industry]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Address] FOREIGN KEY([C_AD_ID])
REFERENCES [dbo].[Address] ([AD_ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Address]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Status_Type] FOREIGN KEY([C_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_Status_Type]
GO
ALTER TABLE [dbo].[Customer_Account]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Account_Broker] FOREIGN KEY([CA_B_ID])
REFERENCES [dbo].[Broker] ([B_ID])
GO
ALTER TABLE [dbo].[Customer_Account] CHECK CONSTRAINT [FK_Customer_Account_Broker]
GO
ALTER TABLE [dbo].[Customer_Account]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Account_Customer] FOREIGN KEY([CA_C_ID])
REFERENCES [dbo].[Customer] ([C_ID])
GO
ALTER TABLE [dbo].[Customer_Account] CHECK CONSTRAINT [FK_Customer_Account_Customer]
GO
ALTER TABLE [dbo].[Customer_Taxrate]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Taxrate_Customer] FOREIGN KEY([CX_C_ID])
REFERENCES [dbo].[Customer] ([C_ID])
GO
ALTER TABLE [dbo].[Customer_Taxrate] CHECK CONSTRAINT [FK_Customer_Taxrate_Customer]
GO
ALTER TABLE [dbo].[Customer_Taxrate]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Taxrate_TaxRate] FOREIGN KEY([CX_TX_ID])
REFERENCES [dbo].[TaxRate] ([TX_ID])
GO
ALTER TABLE [dbo].[Customer_Taxrate] CHECK CONSTRAINT [FK_Customer_Taxrate_TaxRate]
GO
ALTER TABLE [dbo].[Daily_Market]  WITH CHECK ADD  CONSTRAINT [FK_Daily_Market_Security] FOREIGN KEY([DM_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Daily_Market] CHECK CONSTRAINT [FK_Daily_Market_Security]
GO
ALTER TABLE [dbo].[Exchange]  WITH CHECK ADD  CONSTRAINT [FK_Exchange_Address] FOREIGN KEY([EX_AD_ID])
REFERENCES [dbo].[Address] ([AD_ID])
GO
ALTER TABLE [dbo].[Exchange] CHECK CONSTRAINT [FK_Exchange_Address]
GO
ALTER TABLE [dbo].[Financial]  WITH CHECK ADD  CONSTRAINT [FK_Financial_Company] FOREIGN KEY([FI_CO_ID])
REFERENCES [dbo].[Company] ([CO_ID])
GO
ALTER TABLE [dbo].[Financial] CHECK CONSTRAINT [FK_Financial_Company]
GO
ALTER TABLE [dbo].[Holding]  WITH CHECK ADD  CONSTRAINT [FK_Holding_Holding_Summary] FOREIGN KEY([H_CA_ID], [H_S_SYMB])
REFERENCES [dbo].[Holding_Summary] ([HS_CA_ID], [HS_S_SYMB])
GO
ALTER TABLE [dbo].[Holding] CHECK CONSTRAINT [FK_Holding_Holding_Summary]
GO
ALTER TABLE [dbo].[Holding]  WITH CHECK ADD  CONSTRAINT [FK_Holding_Trade] FOREIGN KEY([H_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Holding] CHECK CONSTRAINT [FK_Holding_Trade]
GO
ALTER TABLE [dbo].[Holding_History]  WITH CHECK ADD  CONSTRAINT [FK_Holding_History_Trade] FOREIGN KEY([HH_H_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Holding_History] CHECK CONSTRAINT [FK_Holding_History_Trade]
GO
ALTER TABLE [dbo].[Holding_History]  WITH CHECK ADD  CONSTRAINT [FK_Holding_History_Trade1] FOREIGN KEY([HH_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Holding_History] CHECK CONSTRAINT [FK_Holding_History_Trade1]
GO
ALTER TABLE [dbo].[Holding_Summary]  WITH CHECK ADD  CONSTRAINT [FK_Holding_Summary_Customer_Account] FOREIGN KEY([HS_CA_ID])
REFERENCES [dbo].[Customer_Account] ([CA_ID])
GO
ALTER TABLE [dbo].[Holding_Summary] CHECK CONSTRAINT [FK_Holding_Summary_Customer_Account]
GO
ALTER TABLE [dbo].[Holding_Summary]  WITH CHECK ADD  CONSTRAINT [FK_Holding_Summary_Security] FOREIGN KEY([HS_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Holding_Summary] CHECK CONSTRAINT [FK_Holding_Summary_Security]
GO
ALTER TABLE [dbo].[Industry]  WITH CHECK ADD  CONSTRAINT [FK_Industry_SECTOR] FOREIGN KEY([IN_SC_ID])
REFERENCES [dbo].[Sector] ([SC_ID])
GO
ALTER TABLE [dbo].[Industry] CHECK CONSTRAINT [FK_Industry_SECTOR]
GO
ALTER TABLE [dbo].[Last_Trade]  WITH CHECK ADD  CONSTRAINT [FK_Last_Trade_Security] FOREIGN KEY([LT_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Last_Trade] CHECK CONSTRAINT [FK_Last_Trade_Security]
GO
ALTER TABLE [dbo].[News_Xref]  WITH CHECK ADD  CONSTRAINT [FK_News_Xref_Company] FOREIGN KEY([NX_CO_ID])
REFERENCES [dbo].[Company] ([CO_ID])
GO
ALTER TABLE [dbo].[News_Xref] CHECK CONSTRAINT [FK_News_Xref_Company]
GO
ALTER TABLE [dbo].[News_Xref]  WITH CHECK ADD  CONSTRAINT [FK_News_Xref_News_Item] FOREIGN KEY([NX_NI_ID])
REFERENCES [dbo].[News_Item] ([NI_ID])
GO
ALTER TABLE [dbo].[News_Xref] CHECK CONSTRAINT [FK_News_Xref_News_Item]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_Company] FOREIGN KEY([S_CO_ID])
REFERENCES [dbo].[Company] ([CO_ID])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_Company]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_Exchange] FOREIGN KEY([S_EX_ID])
REFERENCES [dbo].[Exchange] ([EX_ID])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_Exchange]
GO
ALTER TABLE [dbo].[Security]  WITH CHECK ADD  CONSTRAINT [FK_Security_Status_Type] FOREIGN KEY([S_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Security] CHECK CONSTRAINT [FK_Security_Status_Type]
GO
ALTER TABLE [dbo].[Settlement]  WITH CHECK ADD  CONSTRAINT [FK_Settlement_Trade] FOREIGN KEY([SE_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Settlement] CHECK CONSTRAINT [FK_Settlement_Trade]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Customer_Account] FOREIGN KEY([T_CA_ID])
REFERENCES [dbo].[Customer_Account] ([CA_ID])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Customer_Account]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Security] FOREIGN KEY([T_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Security]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Status_Type] FOREIGN KEY([T_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Status_Type]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Trade_Type] FOREIGN KEY([T_TT_ID])
REFERENCES [dbo].[Trade_Type] ([TT_ID])
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [FK_Trade_Trade_Type]
GO
ALTER TABLE [dbo].[Trade_History]  WITH CHECK ADD  CONSTRAINT [FK_Trade_History_Status_Type] FOREIGN KEY([TH_ST_ID])
REFERENCES [dbo].[Status_Type] ([ST_ID])
GO
ALTER TABLE [dbo].[Trade_History] CHECK CONSTRAINT [FK_Trade_History_Status_Type]
GO
ALTER TABLE [dbo].[Trade_History]  WITH CHECK ADD  CONSTRAINT [FK_Trade_History_Trade] FOREIGN KEY([TH_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Trade_History] CHECK CONSTRAINT [FK_Trade_History_Trade]
GO
ALTER TABLE [dbo].[Trade_Request]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Request_Broker] FOREIGN KEY([TR_B_ID])
REFERENCES [dbo].[Broker] ([B_ID])
GO
ALTER TABLE [dbo].[Trade_Request] CHECK CONSTRAINT [FK_Trade_Request_Broker]
GO
ALTER TABLE [dbo].[Trade_Request]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Request_Security] FOREIGN KEY([TR_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Trade_Request] CHECK CONSTRAINT [FK_Trade_Request_Security]
GO
ALTER TABLE [dbo].[Trade_Request]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Request_Trade] FOREIGN KEY([TR_T_ID])
REFERENCES [dbo].[Trade] ([T_ID])
GO
ALTER TABLE [dbo].[Trade_Request] CHECK CONSTRAINT [FK_Trade_Request_Trade]
GO
ALTER TABLE [dbo].[Trade_Request]  WITH CHECK ADD  CONSTRAINT [FK_Trade_Request_Trade_Type] FOREIGN KEY([TR_TT_ID])
REFERENCES [dbo].[Trade_Type] ([TT_ID])
GO
ALTER TABLE [dbo].[Trade_Request] CHECK CONSTRAINT [FK_Trade_Request_Trade_Type]
GO
ALTER TABLE [dbo].[Watch_Item]  WITH CHECK ADD  CONSTRAINT [FK_Watch_Item_Security] FOREIGN KEY([WI_S_SYMB])
REFERENCES [dbo].[Security] ([S_SYMB])
GO
ALTER TABLE [dbo].[Watch_Item] CHECK CONSTRAINT [FK_Watch_Item_Security]
GO
ALTER TABLE [dbo].[Watch_Item]  WITH CHECK ADD  CONSTRAINT [FK_Watch_Item_Watch_List] FOREIGN KEY([WI_WL_ID])
REFERENCES [dbo].[Watch_List] ([WL_ID])
GO
ALTER TABLE [dbo].[Watch_Item] CHECK CONSTRAINT [FK_Watch_Item_Watch_List]
GO
ALTER TABLE [dbo].[Watch_List]  WITH CHECK ADD  CONSTRAINT [FK_Watch_List_Customer] FOREIGN KEY([WL_C_ID])
REFERENCES [dbo].[Customer] ([C_ID])
GO
ALTER TABLE [dbo].[Watch_List] CHECK CONSTRAINT [FK_Watch_List_Customer]
GO
ALTER TABLE [dbo].[Charge]  WITH NOCHECK ADD  CONSTRAINT [CK_Charge_CH_CHRG] CHECK  (([CH_CHRG]>(0)))
GO
ALTER TABLE [dbo].[Charge] CHECK CONSTRAINT [CK_Charge_CH_CHRG]
GO
ALTER TABLE [dbo].[Charge]  WITH NOCHECK ADD  CONSTRAINT [CK_Charge_CH_TIER] CHECK  (([CH_C_TIER]=(3) OR [CH_C_TIER]=(2) OR [CH_C_TIER]=(1)))
GO
ALTER TABLE [dbo].[Charge] CHECK CONSTRAINT [CK_Charge_CH_TIER]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [CK_Customer_Rate_CR_C_TIER] CHECK  (([CR_C_TIER]=(3) OR [CR_C_TIER]=(2) OR [CR_C_TIER]=(1)))
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [CK_Customer_Rate_CR_C_TIER]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [CK_Customer_Rate_CR_FROM_QTY] CHECK  (([CR_FROM_QTY]>(0)))
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [CK_Customer_Rate_CR_FROM_QTY]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [CK_Customer_Rate_CR_RATE] CHECK  (([CR_RATE]>(0)))
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [CK_Customer_Rate_CR_RATE]
GO
ALTER TABLE [dbo].[Commission_Rate]  WITH CHECK ADD  CONSTRAINT [CK_Customer_Rate_CR_TO_QTY] CHECK  (([CR_TO_QTY]>[CR_FROM_QTY]))
GO
ALTER TABLE [dbo].[Commission_Rate] CHECK CONSTRAINT [CK_Customer_Rate_CR_TO_QTY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [CK_Customer_C_TIER] CHECK  (([C_TIER]=(3) OR [C_TIER]=(2) OR [C_TIER]=(1)))
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [CK_Customer_C_TIER]
GO
ALTER TABLE [dbo].[Financial]  WITH CHECK ADD  CONSTRAINT [CK_Financial_FI_QTR] CHECK  (([FI_QTR]=(4) OR [FI_QTR]=(3) OR [FI_QTR]=(2) OR [FI_QTR]=(1)))
GO
ALTER TABLE [dbo].[Financial] CHECK CONSTRAINT [CK_Financial_FI_QTR]
GO
ALTER TABLE [dbo].[Holding]  WITH CHECK ADD  CONSTRAINT [CK_Holding_H_PRICE] CHECK  (([H_PRICE]>(0)))
GO
ALTER TABLE [dbo].[Holding] CHECK CONSTRAINT [CK_Holding_H_PRICE]
GO
ALTER TABLE [dbo].[TaxRate]  WITH CHECK ADD  CONSTRAINT [CK_TaxRate_TX_RATE] CHECK  (([TX_RATE]>=(0)))
GO
ALTER TABLE [dbo].[TaxRate] CHECK CONSTRAINT [CK_TaxRate_TX_RATE]
GO
ALTER TABLE [dbo].[Trade]  WITH CHECK ADD  CONSTRAINT [CK_Trade] CHECK  (([T_QTY]>(0) AND [T_BID_PRICE]>(0) AND [T_CHRG]>=(0) AND [T_COMM]>=(0) AND [T_TAX]>=(0)))
GO
ALTER TABLE [dbo].[Trade] CHECK CONSTRAINT [CK_Trade]
GO
USE [master]
GO
ALTER DATABASE [tpce] SET  READ_WRITE 
GO
