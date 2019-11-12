CREATE TABLE [dbo].[StyleIdealFors] (
    [StyleId]    INT     NOT NULL,
    [IdealForId] TINYINT NOT NULL,
    CONSTRAINT [PK_StyleIdealFors_StyleId_IdealForId] PRIMARY KEY CLUSTERED ([StyleId] ASC, [IdealForId] ASC),
    CONSTRAINT [FK_StyleIdealFors_IdealFors] FOREIGN KEY ([IdealForId]) REFERENCES [dbo].[IdealFors] ([IdealForId]),
    CONSTRAINT [FK_StyleIdealFors_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

