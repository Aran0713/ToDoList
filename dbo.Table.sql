CREATE TABLE [dbo].[ToDoTable]
(
	[ToDo] NVARCHAR(MAX) NOT NULL PRIMARY KEY, 
    [Priority ] INT NOT NULL, 
    [Duration] FLOAT NOT NULL, 
    [DueDate] DATE NULL, 
    [Comments] NVARCHAR(MAX) NULL, 
    [Completed] BIT NULL
)
