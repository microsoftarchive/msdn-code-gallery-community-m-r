CREATE TABLE [dbo].[StyleTerrains] (
    [StyleId]   INT     NOT NULL,
    [TerrainId] TINYINT NOT NULL,
    CONSTRAINT [PK_StyleTerrains] PRIMARY KEY CLUSTERED ([StyleId] ASC, [TerrainId] ASC),
    CONSTRAINT [FK_StyleTerrains_SpecTerrains] FOREIGN KEY ([TerrainId]) REFERENCES [dbo].[SpecTerrains] ([TerrainId]),
    CONSTRAINT [FK_StyleTerrains_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

