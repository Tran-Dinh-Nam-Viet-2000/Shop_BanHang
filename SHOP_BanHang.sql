CREATE DATABASE SHOP_BanHang
GO

USE SHOP_BanHang
GO

CREATE TABLE Brand(
	ID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(150),
	Images NCHAR(150),
	Slug VARCHAR(100),
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime,
	Deleted bit,
)
GO

INSERT Brand(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (1, N'Apple', '~/brand/apple.jpg', 'Apple', 'True', 1, NULL, NULL, 'False')
INSERT Brand(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (2, N'Samsung', '~/brand/samsung.jpg', 'Samsung', 'True', 2, NULL, NULL, 'False')
INSERT Brand(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (3, N'Xiaomi', '~/brand/xiaomi.jpg', 'Xiaomi', 'True', 3, NULL, NULL, 'False')
INSERT Brand(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (4, N'Dell', '~/brand/dell.jpg', 'Dell', 'True', 4, NULL, NULL, 'False')
INSERT Brand(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (5, N'LG', '~/brand/Lg.jpg', 'LG', 'True', 5, NULL, NULL, 'False')

GO

CREATE TABLE Category(
	ID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(150),
	Images NVARCHAR(150),
	Slug VARCHAR(100),
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime,
	Deleted bit,
)
GO
INSERT Category(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (1, N'Phone', '~/brand/apple.jpg', 'Apple', 'True', 1, NULL, NULL, 'False')
INSERT Category(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (2, N'Headphone', '~/brand/samsung.jpg', 'Samsung', 'True', 2, NULL, NULL, 'False')
INSERT Category(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (3, N'Phone Battery', '~/brand/pin.jpg', 'Xiaomi', 'True', 3, NULL, NULL, 'False')
INSERT Category(ID, Name, Images, Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) VALUES (4, N'Laptop', '~/brand/dell.jpg', 'Xiaomi', 'True', 4, NULL, NULL, 'False')
GO

CREATE TABLE Product(
	ID INT NOT NULL PRIMARY KEY,
	Name NVARCHAR(150),
	Images NVARCHAR(150),
	CategoryId INT,
	Price float,
	PriceDiscout float,
	TypeId INT,
	BrandId INT,
	Slug VARCHAR(100),
	ShowOnHomePage bit,
	DisplayOrder int,
	CreatedOnUtc datetime,
	UpdatedOnUtc datetime,
	Deleted bit,
)
GO

INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (11, N'Iphone 6', '~/brand/iphone6.jpg',1,6000000, 4000000, 1,1, 'iphone6', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (12, N'Iphone 7', '~/brand/iphone7.jpg',1,5000000, 3000000, 1,1, 'iphone7', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (13, N'Iphone 8', '~/brand/iphone8.jpg',1,8000000, 4000000, 1,1, 'iphone8', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (14, N'Iphone 10', '~/brand/iphone10.jpg',1,7000000, 5000000, 1,1, 'iphone10', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (15, N'Samsung Note 7', '~/brand/samsung7.jpg',1,2000000, 1000000, 2,1, 'samsungnote7', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (16, N'Samsung Note 8', '~/brand/samsung8.jpg',1,3000000, 2000000, 2,1, 'samsungnote8', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (17, N'Samsung Note 9', '~/brand/samsung9.jpg',1,4000000, 3000000, 2,1, 'samsungnote9', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (18, N'Samsung Note 10', '~/brand/samsung10.jpg',1,7000000, 5000000, 2,1, 'samsungnote10', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (19, N'Xiaomi Redmi Note 7', '~/brand/xiaomi7.jpg',1,7000000, 5000000, 3,1, 'xiaomi7', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (20, N'Xiaomi Redmi Note 8', '~/brand/xiaomi8.jpg',1,7000000, 5000000, 3,1, 'xiaomi8', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (21, N'Xiaomi Redmi Note 9', '~/brand/xiaomi9.jpg',1,7000000, 5000000, 3,1, 'xiaomi8', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (22, N'LG 24km600', '~/brand/lg24km600.png',1,7000000, 5000000, 4,1, 'lg24km600', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (23, N'LG 24GL6G', '~/brand/lg24gl6g.jpg',1,7000000, 5000000, 4,1, 'lg24gl6g', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (24, N'LG 23PG56', '~/brand/lg23pg56.jpg',1,7000000, 5000000, 4,1, 'lg23pg56', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (25, N'Dell Inspiron 3501', '~/brand/dellinspiron3501.jpg',1,7000000, 5000000, 5,1, 'dellinspiron3501', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (26, N'Dell Inspiron 3622', '~/brand/dellinspiron3622.jpg',1,8000000, 6000000, 5,1, 'dellinspiron3622', 'True', 1, NULL, NULL, 'False')
INSERT Product(ID, Name, Images, CategoryId, Price,PriceDiscout,TypeId, BrandId,Slug, ShowOnHomePage, DisplayOrder, CreatedOnUtc, UpdatedOnUtc, Deleted) 
VALUES (27, N'Dell Inspiron 3583', '~/brand/dellinspiron3583.jpg',1,8000000, 5000000, 5,1, 'dellinspiron3583', 'True', 1, NULL, NULL, 'False')
GO

CREATE TABLE Users(
	ID INT NOT NULL PRIMARY KEY,
	Username varchar(50),
	Password varchar(50),
	Email varchar(150)
)
GO

INSERT Users(ID, Username, Password, Email)
VALUES(1, 'viet123', '123', 'viet1085@gmail.com')
INSERT Users(ID, Username, Password, Email)
VALUES(2, 'thang123', 'abc', 'thang1085@gmail.com')
INSERT Users(ID, Username, Password, Email)
VALUES(3, 'nghia123', 'abc123', 'nghia1085@gmail.com')
GO


