CREATE TABLE [dbo].[Countries] (
    [CountryId]   TINYINT      NOT NULL,
    [CountryName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryId] ASC),
    CONSTRAINT [AK_Countries_CountryName] UNIQUE NONCLUSTERED ([CountryName] ASC)
);


GO

CREATE TRIGGER [dbo].[Trigger_Countries_Del_Prevent]
    ON [dbo].[Countries]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;