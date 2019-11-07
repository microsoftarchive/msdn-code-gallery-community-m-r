CREATE TABLE [dbo].[SpecTextValues] (
    [StyleId]      INT            NOT NULL,
    [DisplayIndex] TINYINT        NOT NULL,
    [SpecKeyId]    TINYINT        NOT NULL,
    [TextValue]    NVARCHAR (300) NOT NULL,
    CONSTRAINT [PK_SpecTextValues] PRIMARY KEY CLUSTERED ([StyleId] ASC, [DisplayIndex] ASC, [SpecKeyId] ASC),
    CONSTRAINT [FK_SpecTextValues_SpecKeys] FOREIGN KEY ([SpecKeyId]) REFERENCES [dbo].[SpecKeys] ([SpecKeyId]),
    CONSTRAINT [FK_SpecTextValues_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

