CREATE TABLE [dbo].[StyleAbilities] (
    [StyleId]   INT     NOT NULL,
    [AbilityId] TINYINT NOT NULL,
    CONSTRAINT [PK_StyleAbilities] PRIMARY KEY CLUSTERED ([StyleId] ASC, [AbilityId] ASC),
    CONSTRAINT [FK_StyleAbilities_SpecAbilities] FOREIGN KEY ([AbilityId]) REFERENCES [dbo].[SpecAbilities] ([AbilityId]),
    CONSTRAINT [FK_StyleAbilities_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

