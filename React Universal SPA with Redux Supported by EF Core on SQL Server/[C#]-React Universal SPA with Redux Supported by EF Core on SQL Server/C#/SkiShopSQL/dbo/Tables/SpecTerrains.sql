CREATE TABLE [dbo].[SpecTerrains] (
    [TerrainId]   TINYINT       NOT NULL,
    [TerrainSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecTerrains] PRIMARY KEY CLUSTERED ([TerrainId] ASC),
    CONSTRAINT [AK_SpecTerrains_TerrainSpec] UNIQUE NONCLUSTERED ([TerrainSpec] ASC)
);

