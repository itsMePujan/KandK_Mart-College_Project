USE [master]
GO
/****** Object:  Database [mart]    Script Date: 4/15/2019 5:07:18 AM ******/
CREATE DATABASE [mart]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'mart', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\mart.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'mart_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\mart_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [mart] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [mart].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [mart] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [mart] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [mart] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [mart] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [mart] SET ARITHABORT OFF 
GO
ALTER DATABASE [mart] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [mart] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [mart] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [mart] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [mart] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [mart] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [mart] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [mart] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [mart] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [mart] SET  DISABLE_BROKER 
GO
ALTER DATABASE [mart] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [mart] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [mart] SET TRUSTWORTHY ON 
GO
ALTER DATABASE [mart] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [mart] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [mart] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [mart] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [mart] SET RECOVERY FULL 
GO
ALTER DATABASE [mart] SET  MULTI_USER 
GO
ALTER DATABASE [mart] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [mart] SET DB_CHAINING OFF 
GO
ALTER DATABASE [mart] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [mart] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [mart] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'mart', N'ON'
GO
ALTER DATABASE [mart] SET QUERY_STORE = OFF
GO
USE [mart]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[categoryid] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[categoryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Countryid] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](60) NULL,
	[countryCode] [nvarchar](60) NULL,
	[code] [nvarchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[Countryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Countryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](60) NULL,
	[lastname] [nvarchar](60) NULL,
	[sex] [nvarchar](60) NULL,
	[email] [nvarchar](60) NULL,
	[dob] [varchar](60) NULL,
	[phone] [varchar](60) NULL,
	[CountryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderNumber] [nvarchar](10) NULL,
	[CustomerId] [int] NOT NULL,
	[TotalAmount] [decimal](12, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPrice] [decimal](12, 2) NOT NULL,
	[Quantity] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](60) NULL,
	[categoryid] [int] NULL,
	[supplierId] [int] NULL,
	[stock] [int] NULL,
	[costPrice] [int] NULL,
	[sellingPrice] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](60) NULL,
	[Categoryid] [int] NULL,
	[ContactName] [nvarchar](60) NULL,
	[Phone] [nvarchar](60) NULL,
	[CountryId] [int] NULL,
	[Address] [nvarchar](60) NULL,
	[Email] [nvarchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_detail]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_detail](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](60) NULL,
	[lastname] [nvarchar](60) NULL,
	[sex] [nvarchar](60) NULL,
	[email] [nvarchar](60) NULL,
	[dob] [varchar](60) NULL,
	[phone] [varchar](60) NULL,
	[CountryId] [int] NULL,
	[username] [nvarchar](60) NOT NULL,
	[password] [nvarchar](60) NOT NULL,
	[usertype] [nvarchar](60) NOT NULL,
	[salary] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Countryid])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[product] ([ProductId])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([categoryid])
REFERENCES [dbo].[category] ([categoryid])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([supplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD FOREIGN KEY([Categoryid])
REFERENCES [dbo].[category] ([categoryid])
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Countryid])
GO
ALTER TABLE [dbo].[User_detail]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Countryid])
GO
/****** Object:  StoredProcedure [dbo].[customerName]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[customerName]
as 
begin
select (c.firstname+'  '+c.lastname) as [Customer Name] , c.phone as Phone ,t.CountryName
from  Customer c
  inner join Country t
  on c.CountryId = t.CountryId
  end
GO
/****** Object:  StoredProcedure [dbo].[customersearch]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[customersearch](
@firstname nvarchar(60)
)
as
begin 
select C.CustomerId , C.firstname ,C.lastname , C.sex,C.email,C.dob,C.phone,Co.CountryName
From Customer C
inner join Country Co
on C.CountryId = Co.Countryid

where firstname like @firstname
end
GO
/****** Object:  StoredProcedure [dbo].[customerwisesalesreport]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  Create procedure [dbo].[customerwisesalesreport](
     @customername nvarchar(60)
  )
  as
  begin
  select (c.firstname+' '+c.lastname) as [Customere Name],p.ProductName,o.OrderNumber,o.OrderDate,t.UnitPrice,t.Quantity,o.TotalAmount
  from [Order] o
  inner join OrderItem t
  on o.OrderId = t.Id
   inner join Customer c
   on o.CustomerId = c.CustomerId
   inner join product p
   on t.ProductId = p.ProductId
   where  (c.firstname+'  '+c.lastname) like @customername
   order by OrderNumber desc
   end

GO
/****** Object:  StoredProcedure [dbo].[employee]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[employee](
@firstname nvarchar(60)
)
as
Begin 

SELECT U.userid,U.firstname,U.lastname,U.sex,U.email,U.dob,U.phone,C.CountryName,U.username,U.[password],U.usertype,U.salary
 from User_detail U
 inner join Country C
 on   U.CountryId = C.Countryid
 
 where firstname like @firstname and (usertype ='Admin' or usertype ='Employee') 
end
GO
/****** Object:  StoredProcedure [dbo].[employeeinfo]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[employeeinfo](
@id nvarchar(60)
)
as
Begin 

SELECT U.userid,U.firstname,U.lastname,U.sex,U.email,U.dob,U.phone,C.CountryName,U.username,U.[password],U.usertype,U.salary
 from User_detail U
 inner join Country C
 on   U.CountryId = C.Countryid
 
 where U.userid like @id and (usertype ='Admin' or usertype ='Employee') 
end
GO
/****** Object:  StoredProcedure [dbo].[employesearch]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[employesearch](
@search varchar(60)

)
As 
Begin
select (firstname +' '+ lastname)as[Name],sex,email,dob,phone,username,usertype,employeekey from User_detail
 where (firstname +' '+ lastname) like @search


End
GO
/****** Object:  StoredProcedure [dbo].[placeorder]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[placeorder]
(
@OrderNumber int,
@CustomerId int,
@TotalAmount int,
@ProductId int,
@UnitPrice int, 
@Quantity int
)
as
begin
declare @id int
BEGIN TRY
    BEGIN TRANSACTION
insert into [Order](OrderDate, OrderNumber, CustomerId, TotalAmount) 
values(GETDATE(), @OrderNumber, @CustomerId, @TotalAmount);          --insert values into the [Order] table
select @id = SCOPE_IDENTITY();         --get the identity value by using SCOPE_IDENTITY() or @@IDENTITY
               
--insert values into the second table 
insert into OrderItem(OrderId, ProductId, UnitPrice, Quantity) 
values(@id, @ProductId, @UnitPrice, @Quantity);
COMMIT TRANSACTION -- Transaction Success!
END TRY
BEGIN CATCH
IF @@TRANCOUNT > 0
ROLLBACK TRANSACTION --RollBack in case of Error

DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
DECLARE @ErrorState INT = ERROR_STATE()

-- Use RAISERROR inside the CATCH block to return error  
-- information about the original error that caused  
-- execution to jump to the CATCH block.  
RAISERROR (@ErrorMessage, -- Message text.  
   @ErrorSeverity, -- Severity.  
   @ErrorState -- State.  
   );
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Products]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Products](
@productname nvarchar(60)
)
As
Begin
select P.ProductId,P.ProductName , C.categoryName,S.CompanyName ,P.stock,P.costPrice,P.sellingPrice 
from product P
inner join 	category C
on P.categoryid = C.categoryid  
inner join Supplier S
on P.SupplierId = S.SupplierId


where P.ProductName like @productname


End
GO
/****** Object:  StoredProcedure [dbo].[productwisesalesreport]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  Create procedure [dbo].[productwisesalesreport](
     @productname nvarchar(60)
  )
  as
  begin
  select p.ProductName,o.OrderNumber,o.OrderDate,(c.firstname+' '+c.lastname) as [Name],t.UnitPrice,t.Quantity,o.TotalAmount
  from [Order] o
  inner join OrderItem t
  on o.OrderId = t.Id
   inner join Customer c
   on o.CustomerId = c.CustomerId
   inner join product p
   on t.ProductId = p.ProductId
   where  p.ProductName like @productname
   order by OrderNumber desc
   end
GO
/****** Object:  StoredProcedure [dbo].[suppliersearch]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[suppliersearch](
@like varchar(60)
)
as
begin
select S.SupplierId ,S.CompanyName ,C.categoryName ,S.ContactName,S.Phone,Co.CountryName,S.[Address],S.Email
from Supplier S
inner join category C
      on C.categoryid = S.Categoryid
inner join Country Co
		on Co.Countryid = S.CountryId

		where CompanyName  like  @like

end
GO
/****** Object:  StoredProcedure [dbo].[yearwisesalesreport]    Script Date: 4/15/2019 5:07:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create procedure [dbo].[yearwisesalesreport](
  @DateStart date,
@DateEnd date
  )
  as
  begin
  select o.OrderNumber,o.OrderDate,(c.firstname+' '+c.lastname) as Name,p.ProductName,t.UnitPrice,t.Quantity,o.TotalAmount
  from [Order] o
  inner join OrderItem t
  on o.OrderId = t.Id
   inner join Customer c
   on o.CustomerId = c.CustomerId
   inner join product p
   on t.ProductId = p.ProductId
   where OrderDate >=  @DateStart and OrderDate <=@DateEnd 
   order by OrderNumber desc
   end
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Category Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'category', @level2type=N'COLUMN',@level2name=N'categoryid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Category Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'category', @level2type=N'COLUMN',@level2name=N'categoryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Country Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'Countryid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Country Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'CountryName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Country Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'countryCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Country Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Country', @level2type=N'COLUMN',@level2name=N'code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CustomerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer FirstName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'firstname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer LastName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'lastname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer SEX' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer Date of Birth' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'dob'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer Phone number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customer countryid from country table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Customer', @level2type=N'COLUMN',@level2name=N'CountryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Order Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Order Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store OrderDate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'OrderNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Customerid From customer table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'CustomerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Order Total amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'TotalAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderItem', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store order id from order table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderItem', @level2type=N'COLUMN',@level2name=N'OrderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store product id from product table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderItem', @level2type=N'COLUMN',@level2name=N'ProductId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store unit price of product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderItem', @level2type=N'COLUMN',@level2name=N'UnitPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store quantity of product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'OrderItem', @level2type=N'COLUMN',@level2name=N'Quantity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Product Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'ProductId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Product Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'ProductName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store CategoryId from Category Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'categoryid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store SupplierId from Supplier Table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'supplierId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store stock of product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'stock'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store cost price of product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'costPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'store selling price of product' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'product', @level2type=N'COLUMN',@level2name=N'sellingPrice'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'SupplierId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Company Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'CompanyName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Category id from category table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'Categoryid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Contact Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'ContactName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Phone Number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Country id from Country table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'CountryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Address' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store Supplier Email' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Supplier', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'userid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User First Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'firstname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User lastname' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'lastname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User Sex' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User Sex' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User Date of birth' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'dob'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User Phone number' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User country from country table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'CountryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User username' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User usertype' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'usertype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Store User Salary ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'User_detail', @level2type=N'COLUMN',@level2name=N'salary'
GO
USE [master]
GO
ALTER DATABASE [mart] SET  READ_WRITE 
GO
