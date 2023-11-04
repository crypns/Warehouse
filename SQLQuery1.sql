CREATE DATABASE Warehouse;
GO

USE Warehouse;
GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    AccessLevel INT
);

INSERT INTO Users (Login, Password, Name, Phone, AccessLevel)
VALUES ('Admin', 'Admin', 'Администратор', '+79000000000', 1);

CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Quantity INT,
    Price DECIMAL(10, 2)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    SupplierID INT,
    EmployeeID INT,
    ProductID INT,
    Amount DECIMAL(10, 2),
    OrderDate DATE,
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID),
    FOREIGN KEY (EmployeeID) REFERENCES Users(UserID), -- Используем Users таблицу для EmployeeID
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE Purchases (
    PurchaseID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT,
    ProductID INT,
    EmployeeID INT,
    Quantity INT,
    Amount DECIMAL(10, 2),
    PurchaseDate DATE,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (EmployeeID) REFERENCES Users(UserID) -- Используем Users таблицу для EmployeeID
);
