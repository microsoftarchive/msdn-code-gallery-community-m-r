CREATE TABLE [dbo].[IdealFors] (
    [IdealForId]   TINYINT       NOT NULL,
    [IdealForSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_IdealFors] PRIMARY KEY CLUSTERED ([IdealForId] ASC),
    CONSTRAINT [AK_IdealFors_IdealForSpec] UNIQUE NONCLUSTERED ([IdealForSpec] ASC)
);


GO

CREATE TRIGGER [dbo].[Trigger_IdealFors_Delete_Prevent]
    ON [dbo].[IdealFors]
    FOR DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;