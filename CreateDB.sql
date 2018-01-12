USE master;
IF DB_ID('ReportCreator') IS NOT NULL
   DROP DATABASE ReportCreator;
CREATE DATABASE ReportCreator  collate Ukrainian_CI_AS;
GO
USE ReportCreator;

IF OBJECT_ID('Category', 'U') IS NOT NULL
   DROP TABLE Category;

CREATE TABLE dbo.Category
(
   CategoryId int not null IDENTITY(1,1) primary key, 
   Name NVARCHAR(30) NOT NULL       
);
GO

SET IDENTITY_INSERT dbo.Category ON;
INSERT INTO dbo.Category (CategoryId, Name)
VALUES
   (1, 'Honorarium'),
   (2, 'Scholarship'),
   (3, 'Travel cost'),
   (4, 'Project money'),
   (5, 'Admin expenses')

SET IDENTITY_INSERT dbo.Category OFF;
GO

IF OBJECT_ID('Expenditure', 'U') IS NOT NULL
   DROP TABLE Expenditure;
CREATE TABLE dbo.Expenditure
(
   ExpenditureId int not null IDENTITY(1,1) primary key,   
   [Number] VARCHAR(5) NOT NULL,     
   Name NVARCHAR(40) NOT NULL,
   CategoryId INT NOT NULL FOREIGN KEY REFERENCES dbo.Category(CategoryId) ON UPDATE CASCADE ON DELETE CASCADE
);
GO

SET IDENTITY_INSERT dbo.Expenditure ON;
INSERT INTO dbo.Expenditure (ExpenditureId, [Number], Name, CategoryId)
VALUES
   (1, '17', 'Manager honorarium', 1),
   (2, '73', 'Translation',1),
   (3, '74', 'Tech personal',1),
   (4, '19', 'Student Scholarship',2),
   (5, '63', 'Bank expenses', 5),
   (6, '22', 'Fellows scholarship',2),
   (7, '26', 'Fellows transport', 3)
   

SET IDENTITY_INSERT dbo.Expenditure OFF;
GO

IF OBJECT_ID('Payment', 'U') IS NOT NULL
   DROP TABLE Payment;

CREATE TABLE dbo.Payment
(
   PaymentId int not null IDENTITY(1,1) primary key,   
   PaymentDate DATE NOT NULL,
   Receiver NVARCHAR(50) NOT NULL,
   PurposeOfPayment NVARCHAR(100) NOT NULL,
   [Sum] MONEY NOT NULL DEFAULT 0,
   ExpenditureId INT NOT NULL FOREIGN KEY REFERENCES  dbo.Expenditure(ExpenditureId) ON UPDATE CASCADE ON DELETE CASCADE
);
GO
SET DATEFORMAT dmy;
SET IDENTITY_INSERT dbo.Payment ON;
INSERT INTO dbo.Payment (PaymentId, PaymentDate, Receiver, PurposeOfPayment, [Sum], ExpenditureId)
VALUES
   (1, '23.02.17', 'Olena Holodenko', 'Honorar','528.36',1),
   (2, '13.03.17', 'Olena Holodenko', 'Honorar','493.13',1),
   (3, '18.04.17', 'Olena Holodenko', 'Honorar','492.61',1),
   (4, '19.04.17', 'Olexandr Dobryy', 'Honorar','827.59',1),
   (5, '03.05.17', 'Olexandr Dobryy', 'Honorar','1294.08',1),
   (6, '28.02.17', 'Ievgeniia Zhuk', 'Scholarship','250.52',4),
   (7, '06.11.17', 'KredoBank','Banking expenses','0.10',5),
   (8, '24.02.17', 'Yevhenii Sydorenko','transport reimbursement','26.07',7),
   (9, '24.02.17','Dmytro Sviridov','transport reimbursement', '7.00',7)



SET IDENTITY_INSERT dbo.Payment OFF;
GO
