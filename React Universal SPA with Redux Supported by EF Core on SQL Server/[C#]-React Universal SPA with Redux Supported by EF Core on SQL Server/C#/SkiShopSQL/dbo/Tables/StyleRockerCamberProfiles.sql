CREATE TABLE [dbo].[StyleRockerCamberProfiles] (
    [StyleId]               INT     NOT NULL,
    [RockerCamberProfileId] TINYINT NOT NULL,
    CONSTRAINT [PK_StyleRockerCamberProfiles] PRIMARY KEY CLUSTERED ([StyleId] ASC, [RockerCamberProfileId] ASC),
    CONSTRAINT [FK_StyleRockerCamberProfiles_SpecRockerCamberProfiles] FOREIGN KEY ([RockerCamberProfileId]) REFERENCES [dbo].[SpecRockerCamberProfiles] ([RockerCamberProfileId]),
    CONSTRAINT [FK_StyleRockerCamberProfiles_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

