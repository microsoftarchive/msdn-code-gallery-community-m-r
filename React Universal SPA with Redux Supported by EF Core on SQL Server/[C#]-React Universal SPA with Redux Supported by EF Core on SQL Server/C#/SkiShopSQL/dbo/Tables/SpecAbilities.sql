CREATE TABLE [dbo].[SpecAbilities] (
    [AbilityId]   TINYINT       NOT NULL,
    [AbilitySpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecAbilities] PRIMARY KEY CLUSTERED ([AbilityId] ASC),
    CONSTRAINT [AK_SpecAbilities_AbilitySpec] UNIQUE NONCLUSTERED ([AbilitySpec] ASC)
);

