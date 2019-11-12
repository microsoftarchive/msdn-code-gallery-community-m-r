CREATE TABLE [dbo].[OrderItems] (
    [OrderItemId] INT            IDENTITY (1, 1) NOT NULL,
    [OrderId]     INT            NOT NULL,
    [SkuId]       INT            NOT NULL,
    [Price]       DECIMAL (8, 2) NOT NULL,
    [Quantity]    INT            NOT NULL,
    [SubTotal]    AS             ([Price]*[Quantity]) PERSISTED,
    CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED ([OrderItemId] ASC),
    CONSTRAINT [FK_OrderItems_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]),
    CONSTRAINT [FK_OrderItems_Skus] FOREIGN KEY ([SkuId]) REFERENCES [dbo].[Skus] ([SkuId])
);


GO
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId]
    ON [dbo].[OrderItems]([OrderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_OrderItems_SkuId]
    ON [dbo].[OrderItems]([SkuId] ASC);


GO

CREATE TRIGGER [dbo].[Trigger_OrderItems_Del_Prevent]
    ON [dbo].[OrderItems]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;
GO

CREATE TRIGGER [dbo].[Trigger_OrderItems_Insert]
    ON [dbo].[OrderItems]
    AFTER INSERT
    AS
    BEGIN
        SET NoCount ON;
		SET ROWCOUNT 0;

		BEGIN TRY
			UPDATE  dbo.Skus
				SET Quantity = s.Quantity - inserted.Quantity
					FROM inserted
					INNER JOIN dbo.Skus AS s ON inserted.SkuId = s.SkuId;
		END TRY
		BEGIN CATCH
			IF XACT_STATE() <> 0 
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH

    END