CREATE TABLE [dbo].[SpecSnowConditions] (
    [SnowConditionId]   TINYINT       NOT NULL,
    [SnowConditionSpec] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_SpecSnowConditions] PRIMARY KEY CLUSTERED ([SnowConditionId] ASC),
    CONSTRAINT [AK_SpecSnowConditions_SnowConditionSpec] UNIQUE NONCLUSTERED ([SnowConditionSpec] ASC)
);

