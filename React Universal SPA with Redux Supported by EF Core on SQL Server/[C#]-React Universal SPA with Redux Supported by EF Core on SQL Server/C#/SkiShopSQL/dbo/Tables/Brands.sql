CREATE TABLE [dbo].[Brands] (
    [BrandId]     SMALLINT      NOT NULL,
    [BrandName]   NVARCHAR (50) NOT NULL,
    [CountryId]   TINYINT       NOT NULL,
    [SoftDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED ([BrandId] ASC),
    CONSTRAINT [FK_Brands_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [AK_Brands_BrandName] UNIQUE NONCLUSTERED ([BrandName] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Brands_SoftDeleted]
    ON [dbo].[Brands]([SoftDeleted] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Brands_CountryId]
    ON [dbo].[Brands]([CountryId] ASC);


GO

CREATE TRIGGER [dbo].[Trigger_Brands_Del_Prevent]
    ON [dbo].[Brands]
    INSTEAD OF DELETE
    AS
    BEGIN
		;THROW 50001, 'Records cannot be deleted', 1;
    END;