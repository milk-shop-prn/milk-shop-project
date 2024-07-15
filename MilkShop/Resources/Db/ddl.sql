create database MilkShop
use MilkShop

-- Tạo bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    PasswordHash NVARCHAR(256) NOT NULL,
    FullName NVARCHAR(100),
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber NVARCHAR(15),
    Role NVARCHAR(50) NOT NULL, -- Guest, Member, Staff, Admin
    Points INT DEFAULT 0
);

-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    Price DECIMAL(18, 2) NOT NULL,
    Stock INT NOT NULL,
    Category NVARCHAR(50),
    ImageURL NVARCHAR(256)
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(18, 2) NOT NULL,
    OrderStatus NVARCHAR(50), -- Pending, Confirmed, Shipped, Delivered, Cancelled
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Tạo bảng Reviews
CREATE TABLE Reviews (
    ReviewID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    UserID INT NOT NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(1000),
    ReviewDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Vouchers
CREATE TABLE Vouchers (
    VoucherID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Discount DECIMAL(5, 2) CHECK (Discount BETWEEN 0 AND 100),
    ExpiryDate DATETIME,
    IsUsed BIT DEFAULT 0
);

-- Tạo bảng Articles
CREATE TABLE Articles (
    ArticleID INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX),
    AuthorID INT NOT NULL,
    PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (AuthorID) REFERENCES Users(UserID)
);

-- Tạo bảng Points
CREATE TABLE Points (
    PointID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Points INT NOT NULL,
    DateEarned DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Consultations
CREATE TABLE Consultations (
    ConsultationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ConsultationDate DATETIME NOT NULL DEFAULT GETDATE(),
    Status NVARCHAR(50) -- Pending, Completed, Cancelled
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng Reports
CREATE TABLE Reports (
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    ReportType NVARCHAR(100),
    Description NVARCHAR(1000),
    ReportDate DATETIME NOT NULL DEFAULT GETDATE(),
    ReportedBy INT,
    Status NVARCHAR(50), -- Open, In Progress, Closed
    FOREIGN KEY (ReportedBy) REFERENCES Users(UserID)
);


-- Thêm dữ liệu giả vào bảng Users
INSERT INTO Users (PasswordHash, FullName, Email, PhoneNumber, Role, Points)
VALUES 
('rFlzCG8/7dnAfPl941dwLzzvUJzoPopCuhWqmx7Rnyk=', 'admin', 'admin@gmail.com', '1234567890', 'Admin', 0),
('ICXSc+oUAfzAuY7q+n3i5a6LMihWZhiA18uk1v2vMu0=', 'staff', 'staff@gmail.com', '0987654321', 'Staff', 100),
('hqHicGVjtcUdG3He5NyyC5Q29GCYhZ0jEPKA96vdDfI=', 'customer', 'customer@gmail.com', '1112223333', 'Member', 0);

-- Thêm dữ liệu giả vào bảng Products
INSERT INTO Products (ProductName, Description, Price, Stock, Category, ImageURL)
VALUES 
('Sữa tươi Vinamilk', 'Sữa tươi nguyên chất Vinamilk', 20000, 100, 'Sữa tươi', 'http://example.com/image1.jpg'),
('Sữa chua TH True Milk', 'Sữa chua tiệt trùng TH True Milk', 15000, 200, 'Sữa chua', 'http://example.com/image2.jpg'),
('Sữa bột Dielac', 'Sữa bột dành cho trẻ em Dielac', 250000, 50, 'Sữa bột', 'http://example.com/image3.jpg');

-- Thêm dữ liệu giả vào bảng Orders
INSERT INTO Orders (UserID, TotalPrice, OrderStatus)
VALUES 
(2, 20000, 'Pending'),
(2, 45000, 'Confirmed'),
(1, 15000, 'Pending');

-- Thêm dữ liệu giả vào bảng OrderDetails
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice)
VALUES 
(1, 1, 1, 20000),
(2, 1, 2, 20000),
(2, 2, 1, 15000),
(3, 2, 1, 15000);

-- Thêm dữ liệu giả vào bảng Reviews
INSERT INTO Reviews (ProductID, UserID, Rating, Comment)
VALUES 
(1, 2, 5, 'Sản phẩm rất tốt!'),
(2, 2, 4, 'Sản phẩm khá ổn'),
(3, 2, 3, 'Sản phẩm bình thường');

-- Thêm dữ liệu giả vào bảng Vouchers
INSERT INTO Vouchers (Code, Discount, ExpiryDate, IsUsed)
VALUES 
('DISCOUNT10', 10, '2024-12-31', 0),
('FREESHIP', 0, '2024-12-31', 0);

-- Thêm dữ liệu giả vào bảng Articles
INSERT INTO Articles (Title, Content, AuthorID)
VALUES 
('Chăm sóc sức khỏe cho mẹ bầu', 'Nội dung chăm sóc sức khỏe cho mẹ bầu...', 4),
('Lợi ích của sữa tươi', 'Nội dung về lợi ích của sữa tươi...', 4);

-- Thêm dữ liệu giả vào bảng Points
INSERT INTO Points (UserID, Points)
VALUES 
(2, 50),
(2, 50);

-- Thêm dữ liệu giả vào bảng Consultations
INSERT INTO Consultations (UserID, Status)
VALUES 
(2, 'Pending'),
(2, 'Completed');

-- Thêm dữ liệu giả vào bảng Reports
INSERT INTO Reports (ReportType, Description, ReportedBy, Status)
VALUES 
('Bug', 'Có lỗi khi thanh toán', 2, 'Open'),
('Feature Request', 'Thêm chức năng chat với nhân viên tư vấn', 2, 'In Progress');
