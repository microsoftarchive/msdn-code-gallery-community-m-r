CREATE TABLE [dbo].[SpecMultiValues] (
    [StyleId]                          INT     NOT NULL,
    [DisplayIndex_IdealFor]            TINYINT NOT NULL,
    [DisplayIndex_Ability]             TINYINT NULL,
    [DisplayIndex_SnowCondition]       TINYINT NULL,
    [DisplayIndex_Terrain]             TINYINT NULL,
    [DisplayIndex_RockerCamberProfile] TINYINT NULL,
    CONSTRAINT [PK_SpecMultiValues] PRIMARY KEY CLUSTERED ([StyleId] ASC),
    CONSTRAINT [FK_SpecMultiValues_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

