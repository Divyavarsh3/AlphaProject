USE AlphaDB
GO

CREATE TABLE mst_Books
(
    BookId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

    Title VARCHAR(100) NOT NULL,

    Author VARCHAR(100) NOT NULL,

    Price DECIMAL(10,2) NOT NULL,

    RoleId VARCHAR(32) NOT NULL
    DEFAULT REPLACE(NEWID(), '-', ''),

    IsActive BIT NOT NULL DEFAULT 1,

    CreatedDate DATETIME NOT NULL DEFAULT GETDATE()
)


SELECT * FROM mst_Books


CREATE PROCEDURE usp_InsertBook
(
    @Title VARCHAR(100),
    @Author VARCHAR(100),
    @Price DECIMAL(10,2)
)
AS
BEGIN
    INSERT INTO mst_Books
    (
        Title,
        Author,
        Price
    )
    VALUES
    (
        @Title,
        @Author,
        @Price
    )
END
GO


CREATE PROCEDURE usp_GetBooks
AS
BEGIN
    SELECT *
    FROM mst_Books
    WHERE IsActive = 1
END
GO


CREATE PROCEDURE usp_GetBookById
(
    @BookId UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT *
    FROM mst_Books
    WHERE BookId = @BookId
END
GO


CREATE PROCEDURE usp_UpdateBook
(
    @BookId UNIQUEIDENTIFIER,
    @Title VARCHAR(100),
    @Author VARCHAR(100),
    @Price DECIMAL(10,2)
)
AS
BEGIN
    UPDATE mst_Books
    SET
        Title = @Title,
        Author = @Author,
        Price = @Price
    WHERE BookId = @BookId
END
GO


CREATE PROCEDURE usp_DeleteBook
(
    @BookId UNIQUEIDENTIFIER
)
AS
BEGIN
    UPDATE mst_Books
    SET IsActive = 0
    WHERE BookId = @BookId
END
GO


SELECT *
FROM sys.procedures

EXEC usp_InsertBook
    @Title = 'SQL Basics',
    @Author = 'Divya',
    @Price = 500


    EXEC usp_GetBooks


    SELECT * FROM mst_Books


    EXEC usp_GetBookById
    @BookId = '7F3A2C11-8B9D-4C22-A123-456789ABCD11'


    EXEC usp_UpdateBook
    @BookId = '7F3A2C11-8B9D-4C22-A123-456789ABCD11',
    @Title = 'Advanced SQL',
    @Author = 'Divya V',
    @Price = 900

    EXEC usp_DeleteBook
    @BookId = '7F3A2C11-8B9D-4C22-A123-456789ABCD11'

    EXEC usp_GetBooks

