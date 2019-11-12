CREATE TABLE [dbo].[SpecRockerCamberProfiles] (
    [RockerCamberProfileId]   TINYINT       NOT NULL,
    [RockerCamberProfileSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecRockerCamberProfiles] PRIMARY KEY CLUSTERED ([RockerCamberProfileId] ASC),
    CONSTRAINT [AK_SpecRockerCamberProfiles_RockerCamberProfileSpec] UNIQUE NONCLUSTERED ([RockerCamberProfileSpec] ASC)
);

