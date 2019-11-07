CREATE TABLE [dbo].[Orders] (
    [OrderId]            INT              IDENTITY (1, 1) NOT NULL,
    [CustomerOrderId]    UNIQUEIDENTIFIER NOT NULL,
    [Email]              NVARCHAR (100)   NOT NULL,
    [UserId]             INT              NULL,
    [FullName]           NVARCHAR (100)   NOT NULL,
    [ProvinceId]         SMALLINT         NOT NULL,
    [City]               NVARCHAR (50)    NOT NULL,
    [AddressLine]        NVARCHAR (100)   NOT NULL,
    [PostalCode]         NVARCHAR (7)     NOT NULL,
    [TotalValue]         DECIMAL (10, 2)  NOT NULL,
    [CreatedDateTime]    DATETIME2 (7)    NOT NULL,
    [CreatedDateTimeUTC] DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
    CONSTRAINT [FK_Orders_Provinces] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[Provinces] ([ProvinceId]),
    CONSTRAINT [FK_Orders_UserIdentities] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserIdentities] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Orders_Email]
    ON [dbo].[Orders]([Email] ASC)
    INCLUDE([CustomerOrderId], [TotalValue], [CreatedDateTime]);


GO

CREATE TRIGGER [dbo].[Trigger_Orders_Del_Prevent]
    ON [dbo].[Orders]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;