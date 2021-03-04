use OFOS

create table Menu
(
	FoodID int primary key IDENTITY(1000,1),
	FoodName varchar(20) not null,
	FoodCategory varchar(30) not null,
	Price money not null,
	Stock varchar(20) not null 
	CHECK (Stock = 'Available' OR Stock = 'Not Available')
)

Create table FoodCart
(
	FoodID int FOREIGN KEY REFERENCES Menu(FoodID),
	Qunatity int not null,
	TotalAmount float
)

Create table PaymentDetails
(
	CustomerName varchar(30) not null,
	CustomerCardNo varchar(20) not null,
	CustomerPhoneNo varchar(12) not null,
	TotalAmount money not null,
	TransactionStatus varchar(20) not null,
	CHECK( TransactionStatus ='Successfull' OR TransactionStatus ='Unsuccessfull')	
)

create table OrderDetails
(
	OderID int primary key IDENTITY(2000,1),
	FoodID int Foreign key references Menu(FoodId),
	CustomerId int Foreign key references Customer(CustomerId),
	Orderstatus varchar(20) not null,   
	CHECK ( Orderstatus = 'Processing'  OR Orderstatus = 'Dispatched' OR Orderstatus='Delivered'),
	ShippingAddress varchar(100) not null,      
	ExpectedTimeOfDelivery datetime not null,
	Quantity int not null,
	TotalAmount money
)

create table AdminLogin
(
	Username varchar(20) Primary key,
	Pass varchar(20)
)
insert into AdminLogin values ('Kushanth', 'Admin@123');
insert into AdminLogin values ('Shantanu', 'Admin@123');
insert into AdminLogin values ('Arnav', 'Admin@123');
insert into AdminLogin values ('Manasa', 'Admin@123');
insert into AdminLogin values ('Varsha', 'Admin@123');

create table Customer
(
	CustomerId int primary key Identity(3000,1),
	Username varchar(20),
	Pass varchar(20)
)



Select * from Menu
Select * from Customer
select * from OrderDetails




