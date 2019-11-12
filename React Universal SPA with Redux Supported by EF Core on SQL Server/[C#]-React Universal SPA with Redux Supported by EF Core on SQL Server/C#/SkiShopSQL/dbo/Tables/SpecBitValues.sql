CREATE TABLE [dbo].[SpecBitValues] (
    [StyleId]      INT     NOT NULL,
    [DisplayIndex] TINYINT NOT NULL,
    [SpecKeyId]    TINYINT NOT NULL,
    [SpecValue]    BIT     NOT NULL,
    CONSTRAINT [PK_SpecBitValues] PRIMARY KEY CLUSTERED ([StyleId] ASC, [DisplayIndex] ASC, [SpecKeyId] ASC),
    CONSTRAINT [FK_SpecBitValues_SpecKeys] FOREIGN KEY ([SpecKeyId]) REFERENCES [dbo].[SpecKeys] ([SpecKeyId]),
    CONSTRAINT [FK_SpecBitValues_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

