CREATE TABLE [dbo].[Skus] (
    [SkuId]       INT           NOT NULL,
    [SkuNo]       CHAR (8)      NOT NULL,
    [StyleId]     INT           NOT NULL,
    [Size]        NVARCHAR (20) NOT NULL,
    [Quantity]    SMALLINT      NOT NULL,
    [SoftDeleted] BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Skus] PRIMARY KEY CLUSTERED ([SkuId] ASC),
    CONSTRAINT [CK_Skus_SkuNo] CHECK ([SkuNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT [FK_Skus_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId]),
    CONSTRAINT [AK_Skus_SkuNo] UNIQUE NONCLUSTERED ([SkuNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Skus_StyleId]
    ON [dbo].[Skus]([StyleId] ASC)
    INCLUDE([Quantity]);


GO

CREATE TRIGGER [dbo].[Trigger_Skus_Del_Prevent]
    ON [dbo].[Skus]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;
GO

CREATE TRIGGER [dbo].[Trigger_Skus_Insert_Update]
    ON [dbo].[Skus]
    AFTER INSERT, UPDATE
    AS
    BEGIN
        SET NoCount ON;
		SET ROWCOUNT 0;
        
		BEGIN TRY
            DECLARE @tbUpdated TABLE(StyleId INT, TotalQuantity SMALLINT);
            
			INSERT INTO @tbUpdated (StyleId, TotalQuantity)
				SELECT StyleId, SUM(Quantity)
					FROM Skus
					WHERE StyleId IN (SELECT StyleId FROM inserted)
					GROUP BY StyleId;
            
			UPDATE  dbo.StyleStates
				SET SoldOut = (CASE WHEN tu.TotalQuantity > 0 THEN 0 ELSE 1 END)
					FROM StyleStates AS ss
					INNER JOIN @tbUpdated AS tu ON ss.StyleId = tu.StyleId;
		END TRY
		BEGIN CATCH
			IF XACT_STATE() <> 0 
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH
    END;