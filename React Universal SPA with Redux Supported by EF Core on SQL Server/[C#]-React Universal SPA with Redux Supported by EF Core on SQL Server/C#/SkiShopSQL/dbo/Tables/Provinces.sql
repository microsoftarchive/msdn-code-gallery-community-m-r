CREATE TABLE [dbo].[Provinces] (
    [ProvinceId]   SMALLINT      IDENTITY (1, 1) NOT NULL,
    [ProvinceCode] CHAR (2)      NOT NULL,
    [ProvinceName] NVARCHAR (50) NOT NULL,
    [CountryId]    TINYINT       NOT NULL,
    CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED ([ProvinceId] ASC),
    CONSTRAINT [FK_Provinces_Countries] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [AK_Provinces_Code_Country] UNIQUE NONCLUSTERED ([ProvinceCode] ASC, [CountryId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Provinces_CountryId]
    ON [dbo].[Provinces]([CountryId] ASC);

