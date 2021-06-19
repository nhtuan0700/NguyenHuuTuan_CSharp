create database NguyenHuuTuanDB
use NguyenHuuTuanDB

create table UserAccount
(
	ID int identity,
	Name nvarchar(50),
	UserName varchar(20),
	Password varchar(100),
	Status bit default 1,
	primary key (ID)
)

create table Category
(
	ID int identity,
	Name nvarchar(100),
	Description nvarchar(max),
	primary key (ID)
)

create table Product
(
	ID int identity,
	Name nvarchar(100),
	UnitCost money,
	Quantity int,
	Image nvarchar(max),
	Description nvarchar(max),
	Status bit default 1,
	ID_Category int,
	foreign key (ID_Category) references Category(ID),
	primary key (ID)
)

insert into UserAccount(Name, UserName, Password)
values(N'Nguyễn Hữu Tuấn','admin', '4297f44b13955235245b2497399d7a93'),
(N'Trần Hòa','hoa123', '4297f44b13955235245b2497399d7a93'),
(N'Nguyễn Văn Trí','tri123', '4297f44b13955235245b2497399d7a93'),
(N'Nguyễn Kim An','an123', '4297f44b13955235245b2497399d7a93'),
(N'Trịnh Quang Phúc','phuc123', '4297f44b13955235245b2497399d7a93'),
(N'Hà Đại Nghĩa','nghia123', '4297f44b13955235245b2497399d7a93')

insert into Category(name)
values('Iphone'),
('Samsung'),
('Oppo')

insert into Product(Name, Quantity, UnitCost, ID_Category, Image)
values(N'iPhone 12 64GB', 25, 19190000, 1, 'sp1.jpg'),
(N'iPhone 12 Pro Max 128GB', 20, 31990000, 1, 'sp2.jpg'),
(N'iPhone 11 256GB', 10, 22490000, 1, 'sp3.jpg'),
(N'iPhone XR 64GB ', 7, 14490000, 1, 'sp4.jpg'),
(N'Samsung Galaxy S21', 30, 18990000, 2, 'sp5.jpg'),
(N'Samsung Galaxy A02', 32, 2090000, 2, 'sp6.jpg'),
(N'Samsung Galaxy M51', 30, 7990000, 2, 'sp7.jpg'),
(N'Samsung Galaxy A32', 15, 6290000, 2, 'sp8.jpg'),
(N'OPPO A93', 20, 5990000, 3, 'sp9.jpg'),
(N'OPPO Reno5 5G', 30, 10990000, 3, 'sp10.jpg'),
(N'OPPO Find X3 Pro 5G', 15, 26990000, 3, 'sp11.jpg'),
(N'OPPO Reno4', 13, 6990000, 3, 'sp12.jpg')