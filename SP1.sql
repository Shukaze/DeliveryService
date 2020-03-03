	CREATE PROCEDURE [dbo].[sp_LogPage]
    @Username nvarchar(50),
    @Password int
AS
    SELECT Count(1) From Users Where Username = @Username and Password = @Password
GO

CREATE PROCEDURE [dbo].[sp_User]
@Username nvarchar(50)
AS
    SELECT * FROM Users where Username = @Username
GO

CREATE PROCEDURE [dbo].[sp_RegUsers]
@Username nvarchar (50),
@Password int,
@Role nvarchar(50),
@First_Name nvarchar(50),
@Last_Name nvarchar(50),
@Phone_Number nvarchar(50),
@Address nvarchar(50),
@Email nvarchar(50)
AS
    Insert INTO [dbo].[Users](Username, Password, Role,
	First_Name, Last_Name, Phone_Number,
	Address, [E-mail])
	Values(@Username, @Password, @Role, 
	@First_Name, @Last_Name,
	@Phone_Number, @Address, @Email)
GO


CREATE PROCEDURE [dbo].[sp_Products]
@Id	   int,
@Books nvarchar(50),
@Cost  int, 
@count int
AS
	INSERT INTO [dbo].[Products](id_product, name_product, cost, Count)
		values (@Id, @Books, @Cost, @count)
GO



CREATE PROCEDURE [dbo].[sp_Books]
As
SELECT * FROM Products
GO




CREATE PROCEDURE [dbo].[sp_Books_upd]

@Books nvarchar(50),
@Cost int,
@count int,
@Id		int
AS
	UPDATE Products SET name_product = @Books, cost = @Cost, Count=@count
	Where id_product = @Id
GO




CREATE PROCEDURE [dbo].[sp_Books_Del]
@Id		int
AS
	Delete From Products 
	Where id_product = @Id
GO



CREATE PROCEDURE [dbo].[sp_Find_BName]
@Books		nvarchar (50)
AS
	SELECT * FROM Products
	 Where name_product like '%'+ @Books +'%'
GO


CREATE PROCEDURE [dbo].[sp_XmlDes]
@id_product int,
@name_product nvarchar(50),
@cost int,
@count int
AS
if not exists(select * from Products where id_product=@id_product)
BEGIN 
INSERT INTO Products(id_product, name_product, cost, Count) values (@id_product, @name_product,
 @cost, @count)
END
else
BEGIN
if exists(select * from Products where id_product=@id_product  
and name_product!=@name_product)
update Products set name_product=@name_product, cost=@cost, Count=@count where id_product=@id_product
END;
GO



CREATE PROCEDURE [dbo].[sp_Role]

@Username nvarchar(50),
@Password int
AS
	SELECT Role FROM Users where Username=@Username AND Password=@Password
GO

CREATE PROCEDURE [dbo].[sp_Basket]
@Username nvarchar(50)
AS
	Select id_product[id], name_product, count, cost From Basket where Username=@Username
GO


CREATE PROCEDURE [dbo].[sp_AddToBasket]
@username nvarchar (50),
@id_prod int,
@name_product nvarchar(50),
@cost int,
@count_add int
AS
	insert INTO BASKET (username, id_product, name_product, cost, count ) 
	values (@username, @id_prod, @name_product, @cost, @count_add)
Go


CREATE PROCEDURE [dbo].[sp_ComboAddress]
@Username nvarchar(50)
AS
SELECT * From [dbo].[Address] Where Username = @Username
GO




CREATE PROCEDURE [dbo].[sp_AddressAdd]
@Username nvarchar(50),
@City nvarchar(50),
@Street nvarchar(50),
@NumberH nvarchar(50)
AS
	Insert Into [dbo].[Address] (Username, City, Street, NumberHouse) 
	values (@Username, @City, @Street, @NumberH) 
GO



CREATE PROCEDURE [dbo].[sp_AddressDel]
@City nvarchar(50),
@Street nvarchar(50),
@NumberH nvarchar(50),
@Username nvarchar(50)
AS
	Delete From Address Where Username = @Username AND City=@City AND Street=@Street AND NumberHouse=@NumberH 
GO	

CREATE PROCEDURE [dbo].[sp_AddressFill]
@Username nvarchar(50)
AS
	SELECT City, Street, NumberHouse FROM Address Where Username = @Username
GO

CREATE PROCEDURE [dbo].[sp_ComboDelivery]
AS
	Select * From Delivery_method
GO

CREATE PROCEDURE [dbo].[sp_ComboPayments]
AS
	Select * From Payments_method
GO

CREATE PROCEDURE [dbo].[sp_ComboCards]
@Username nvarchar(50)
AS
	Select card_number From Card Where Username=@Username
GO


CREATE PROCEDURE [dbo].[sp_AddAdditional]
@OrderNum int,
@Address nvarchar (50),
@Delivery nvarchar (50),
@Payments nvarchar (50),
@CardNum nvarchar(50),
@DeliveryDate date,
@Status nvarchar(10),
@Username nvarchar (50),
@DateOrder date
AS
	UPDATE Orders SET order_number=@OrderNum,
	 address = @Address, name_of_delivery = @Delivery, 
	 name_of_payments = @Payments, card_number = @CardNum, 
	 delivery_date = @DeliveryDate, Status=@Status
	Where Username = @Username and order_date = @DateOrder and address is null;	
GO


CREATE PROCEDURE [dbo].[sp_AddAdditionalWithCash]
@OrderNum int,
@Address nvarchar (50),
@Delivery nvarchar (50),
@Payments nvarchar (50),
@DeliveryDate date,
@Status nvarchar(10),
@Username nvarchar (50),
@DateOrder date
AS
	UPDATE Orders SET order_number=@OrderNum,
	 address = @Address, name_of_delivery = @Delivery, 
	 name_of_payments = @Payments, delivery_date = @DeliveryDate, 
	 Status=@Status
	Where Username = @Username and order_date = @DateOrder and address is null;	
GO


CREATE PROCEDURE [dbo].[sp_TCost]
@Username nvarchar(50)
AS
	Select sum(cost*count) From Basket Where Username = @Username
GO

CREATE PROCEDURE [dbo].[sp_Orders]
AS	
	Insert Into Orders (Username, name_product, cost, count, order_date)
	Select Basket.Username, Basket.name_product, Basket.cost, Basket.count, Basket.order_date
    From Basket
GO
	

CREATE PROCEDURE [dbo].[sp_AddCard]
@Username nvarchar (50),
@CardNum int,
@Owner nvarchar(50),
@MMYY int,
@CVC int
AS
	Insert Into Card(Username, card_number, owner, MMYY, CVC) 
	values (@Username, @CardNum, @Owner, @MMYY, @CVC)
GO

CREATE PROCEDURE [dbo].[sp_CheckCard]
@CardNum int,
@Username nvarchar(50)
AS
	SELECT * From Card Where card_number=@CardNum and Username=@Username
GO

CREATE PROCEDURE [dbo].[sp_UpdBasketDate]
@DateOrder date,
@Username nvarchar(50)
AS
	Update Basket Set order_Date = @DateOrder Where Username=@Username
GO

CREATE PROCEDURE [dbo].[sp_UpdOrderCost]
@TCost int, 
@Username nvarchar(50),
@DateOrder date
As
	Update Orders Set TCost=@TCost Where Username=@Username and order_date = @DateOrder
GO

Create Procedure [dbo].[sp_BasketClear]
@Username nvarchar(50)
AS
	Delete From Basket where Username=@Username
GO

CREATE Procedure [dbo].[sp_OrderCancel]
AS
	Insert Into Basket(Username, name_product, cost, count, order_date)
	Select Orders.Username, Orders.name_product, Orders.cost, Orders.count, Orders.order_date
    From Orders
GO

CREATE Procedure [dbo].[sp_OrderClear]
@Username nvarchar(50)
AS
	Delete From Orders where Username=@Username
GO


CREATE PROCEDURE [dbo].[sp_OrderNumber]
@Username nvarchar(50),
@DateOrder date
AS
	Select sum(id_order) From Orders Where Username = @Username and 
	order_date = @DateOrder and address is null
GO


CREATE PROCEDURE [dbo].[sp_ViewOrdersAdmin]
AS
	Select username AS [User] , order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date], Status AS [Status] 
	From Orders
GO	


CREATE PROCEDURE [dbo].[sp_ViewOrders]
@Username nvarchar(50)
AS
	Select order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date], Status AS [Status]
	From Orders 
	where Username=@Username and Status = 'Active'
GO

CREATE PROCEDURE [dbo].[sp_Find_Cost]
@MinCost		int,
@MaxCost		int
AS
	SELECT * FROM Products
	 Where cost between @MinCost and @MaxCost 
GO

CREATE PROCEDURE [dbo].[sp_INActiveStatus]
AS
	Select username AS [User] , order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date] ,
	Status AS [Status]
	From Orders Where Status='Inactive'
GO

CREATE PROCEDURE [dbo].[sp_ActiveStatus]
AS
	Select username AS [User], order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date],
	Status AS [Status] 
	From Orders Where Status='Active'
GO


CREATE PROCEDURE [dbo].[sp_CheckStatus]
AS
	Update Orders SET Status='Inactive' WHERE Status='Active' and GETDATE()>=delivery_date
GO

CREATE PROCEDURE [dbo].[sp_ConfirmStatus]
@Username nvarchar(50)
AS
	Update Orders SET Status='Inactive' WHERE Status='Active' and GETDATE()>=delivery_date  
	and Username=@Username
GO


CREATE PROCEDURE [dbo].[sp_Find_Order_Product]
@Product		nvarchar (50),
@Username nvarchar(50)
AS
	Select order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date], Status AS [Status] 
	From Orders 
	 Where name_product like '%'+ @Product +'%' and Username=@Username
GO

CREATE PROCEDURE [dbo].[sp_Find_Order]
@Product		nvarchar (50)
AS
	Select username AS [User], order_number AS [Order Number], name_product AS [Product], count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date], Status AS [Status] 
	From Orders 
	 Where name_product like '%'+ @Product +'%'
GO


CREATE PROCEDURE [dbo].[sp_ViewUsers]
AS
	Select Username AS [User], Password,
		   Role, First_Name AS [First Name],
		   Last_Name AS [Last Name], 
		   Phone_Number AS [Phone Number],
		   Address, [E-mail] 
		   From Users
GO

CREATE PROCEDURE [dbo].[sp_ChangeRole]
@Role nvarchar(50),
@Username nvarchar(50)
AS
	Update Users SET Role=@Role where username=@Username
GO


CREATE PROCEDURE [dbo].[sp_Report]
@StartDate date,
@EndDate date
AS
	Select username AS [User], order_number AS [Order Number], name_product AS [Product], 
	count AS [Count],
	TCost AS [Total Cost], address AS [Address], name_of_delivery AS [Delivery],
	name_of_payments AS [Payments], card_number AS [Card], 
	convert(varchar(10),order_date,101) as [Order date],
	convert(varchar(10), delivery_date,101) as [Delivery Date], Status AS [Status] 
	From Orders where order_date between @StartDate and @EndDate
 GO




 CREATE PROCEDURE [dbo].[sp_CheckBookName]
 @id_prod nvarchar(50),
 @Username nvarchar(50)
 AS
	SELECT Count(1) From Basket Where id_product=@id_prod and Username=@Username
GO

CREATE PROCEDURE [dbo].[sp_DeleteBasketRow]
 @id_prod nvarchar(50),
 @Username nvarchar(50)
 AS
	DELETE From Basket where id_product=@id_prod and Username=@Username
	GO


CREATE PROCEDURE [dbo].[sp_100000]
AS
	DECLARE @i int
	Set @i = 0
	WHILE @i < 100000
	BEGIN
	INSERT INTO Products(id_product, name_product, cost, Count)
	Values (@i, 'Product', 123, 111, 1000)
	SET @i = @i + 1
	END;
	