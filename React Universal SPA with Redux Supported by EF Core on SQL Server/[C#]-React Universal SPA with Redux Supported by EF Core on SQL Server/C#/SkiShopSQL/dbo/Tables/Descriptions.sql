CREATE TABLE [dbo].[Descriptions] (
    [DisplayIndex] TINYINT         NOT NULL,
    [StyleId]      INT             NOT NULL,
    [DescText]     NVARCHAR (1000) NOT NULL,
    CONSTRAINT [PK_Descriptions] PRIMARY KEY CLUSTERED ([StyleId] ASC, [DisplayIndex] ASC),
    CONSTRAINT [CK_Descriptions_DescText] CHECK ([DescText]<>''),
    CONSTRAINT [FK_Descriptions_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId])
);

