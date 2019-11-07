CREATE TABLE [dbo].[Categories] (
    [CategoryId]   TINYINT       NOT NULL,
    [CategoryName] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryId] ASC),
    CONSTRAINT [AK_Categories_CategoryName] UNIQUE NONCLUSTERED ([CategoryName] ASC)
);


GO

CREATE TRIGGER [dbo].[Trigger_Categories_Delete_Prevent]
    ON [dbo].[Categories]
    FOR DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;