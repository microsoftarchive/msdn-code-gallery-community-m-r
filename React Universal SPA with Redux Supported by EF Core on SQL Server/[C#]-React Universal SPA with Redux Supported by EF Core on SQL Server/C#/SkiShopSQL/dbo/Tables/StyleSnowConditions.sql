CREATE TABLE [dbo].[StyleSnowConditions] (
    [StyleId]         INT     NOT NULL,
    [SnowConditionId] TINYINT NOT NULL,
    CONSTRAINT [PK_StyleSnowConditions] PRIMARY KEY CLUSTERED ([StyleId] ASC, [SnowConditionId] ASC),
    CONSTRAINT [FK_StyleSnowConditions_SpecSnowConditions] FOREIGN KEY ([SnowConditionId]) REFERENCES [dbo].[SpecSnowConditions] ([SnowConditionId]),
    CONSTRAINT [FK_StyleSnowConditions_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

