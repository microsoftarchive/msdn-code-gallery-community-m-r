CREATE TABLE [dbo].[SpecKeys] (
    [SpecKeyId]   TINYINT         NOT NULL,
    [SpecKeyName] NVARCHAR (50)   NOT NULL,
    [SpecKeyDesc] NVARCHAR (1000) NULL,
    CONSTRAINT [PK_SpecKeys] PRIMARY KEY CLUSTERED ([SpecKeyId] ASC),
    CONSTRAINT [AK_SpecKeys_SpecKey] UNIQUE NONCLUSTERED ([SpecKeyName] ASC)
);

