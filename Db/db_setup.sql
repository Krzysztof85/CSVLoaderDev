CREATE DATABASE TaxCalculator;

GO

USE [TaxCalculator];
GO
 
CREATE TABLE [dbo].[Transaction]
(
ID BIGINT PRIMARY KEY IDENTITY(1,1),
Account NVARCHAR(MAX),
Description NVARCHAR(MAX),
CCYCode CHAR(3),
Value FLOAT
)