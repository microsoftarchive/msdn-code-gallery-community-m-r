CREATE TABLE [dbo].[SpecConstructions] (
    [ConstructionId]   TINYINT       NOT NULL,
    [ConstructionSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecConstructions] PRIMARY KEY CLUSTERED ([ConstructionId] ASC),
    CONSTRAINT [AK_SpecConstructions_ConstructionSpec] UNIQUE NONCLUSTERED ([ConstructionSpec] ASC)
);

