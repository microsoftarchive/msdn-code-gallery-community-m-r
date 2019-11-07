CREATE TABLE [dbo].[SpecSingleValues] (
    [StyleId]                   INT     NOT NULL,
    [DisplayIndex_Core]         TINYINT NOT NULL,
    [CoreId]                    TINYINT NOT NULL,
    [DisplayIndex_Construction] TINYINT NOT NULL,
    [ConstructionId]            TINYINT NOT NULL,
    [DisplayIndex_MadeIn]       TINYINT NOT NULL,
    [MadeInId]                  TINYINT NOT NULL,
    CONSTRAINT [PK_SpecSingleValues] PRIMARY KEY CLUSTERED ([StyleId] ASC),
    CONSTRAINT [FK_SpecSingleValues_Countries] FOREIGN KEY ([MadeInId]) REFERENCES [dbo].[Countries] ([CountryId]),
    CONSTRAINT [FK_SpecSingleValues_SpecConstructions] FOREIGN KEY ([ConstructionId]) REFERENCES [dbo].[SpecConstructions] ([ConstructionId]),
    CONSTRAINT [FK_SpecSingleValues_SpecCores] FOREIGN KEY ([CoreId]) REFERENCES [dbo].[SpecCores] ([CoreId]),
    CONSTRAINT [FK_SpecSingleValues_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);


GO
CREATE NONCLUSTERED INDEX [IX_SpecSingleValues_CoreId]
    ON [dbo].[SpecSingleValues]([CoreId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SpecSingleValues_ConstructionId]
    ON [dbo].[SpecSingleValues]([ConstructionId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_SpecSingleValues_MadeInId]
    ON [dbo].[SpecSingleValues]([MadeInId] ASC);

