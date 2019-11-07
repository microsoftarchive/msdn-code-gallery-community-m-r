CREATE TABLE [dbo].[SpecCores] (
    [CoreId]   TINYINT       NOT NULL,
    [CoreSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecCores] PRIMARY KEY CLUSTERED ([CoreId] ASC),
    CONSTRAINT [AK_SpecCores_CoreSpec] UNIQUE NONCLUSTERED ([CoreSpec] ASC)
);

