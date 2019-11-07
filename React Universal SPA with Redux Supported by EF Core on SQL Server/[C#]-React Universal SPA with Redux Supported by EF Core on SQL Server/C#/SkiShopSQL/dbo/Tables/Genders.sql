CREATE TABLE [dbo].[Genders] (
    [GenderId]   TINYINT       NOT NULL,
    [GenderName] NVARCHAR (30) NOT NULL,
    CONSTRAINT [PK_Genders] PRIMARY KEY CLUSTERED ([GenderId] ASC),
    CONSTRAINT [AK_Genders_GenderName] UNIQUE NONCLUSTERED ([GenderName] ASC)
);


GO

CREATE TRIGGER [dbo].[Trigger_Gender_Delete_Prevent]
    ON [dbo].[Genders]
    FOR DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;