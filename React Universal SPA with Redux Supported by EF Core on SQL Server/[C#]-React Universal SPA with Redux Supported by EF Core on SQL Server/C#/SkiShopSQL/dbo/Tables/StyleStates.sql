CREATE TABLE [dbo].[StyleStates] (
    [StyleId]        INT            NOT NULL,
    [AverageRatings] DECIMAL (2, 1) DEFAULT ((0.0)) NOT NULL,
    [ReviewCount]    INT            DEFAULT ((0)) NOT NULL,
    [SoldOut]        BIT            DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_StyleStates] PRIMARY KEY CLUSTERED ([StyleId] ASC),
    CONSTRAINT [FK_StyleStates_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

