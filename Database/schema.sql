
DECLARE @DatabaseName NVARCHAR(128) = N'BookCatalogDB';



-- first check if the database exists, if not create it



IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @DatabaseName)

BEGIN

    EXEC ('CREATE DATABASE [' + @DatabaseName + ']');

END

GO 



-- use the database



USE BookCatalogDB;

GO



IF OBJECT_ID('Books', 'U') IS NOT NULL DROP TABLE Books;

IF OBJECT_ID('Authors', 'U') IS NOT NULL DROP TABLE Authors;





-- 1. CREATE Authors Table

CREATE TABLE Authors (

    ID          INT PRIMARY KEY IDENTITY(1,1),

    Name        NVARCHAR(255) NOT NULL

);



-- 1. CREATE Books Table

CREATE TABLE Books (

    ID              INT PRIMARY KEY IDENTITY(1,1),

    Title           NVARCHAR(500) NOT NULL,

    PublicationYear INT NOT NULL,

    AuthorID        INT NOT NULL,

    

    -- Define the FOREIGN KEY

    CONSTRAINT FK_Books_AuthorID FOREIGN KEY (AuthorID) REFERENCES Authors(ID)

);





-- 2. INSERTING Sample Data into Authors and Books Tables

INSERT INTO Authors (Name) VALUES

    ('Robert C. Martin'),

    ('Jeffrey Richter');



INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES

    ('Clean Code', 1, 2008),

    ('CLR via C#', 2, 2012),

    ('The Clean Coder', 1, 2011),

    ('Refactoring', 2, 2000);





-- 3. UPDATE Script example updating the PublicationYear of a book with ID 2

UPDATE Books SET PublicationYear = 2013 WHERE ID = 2;





-- 4. DELETE Script example deleting a book with ID 3

DELETE FROM Books WHERE ID = 3;





-- 5. SELECT Script (Retrieve all book titles and their author's name for books published AFTER 2010)

SELECT

    b.Title,

    a.Name AS AuthorName

FROM

    Books b
    a
JOIN

    Authors a ON b.AuthorID = a.ID

WHERE

    b.PublicationYear > 2010;