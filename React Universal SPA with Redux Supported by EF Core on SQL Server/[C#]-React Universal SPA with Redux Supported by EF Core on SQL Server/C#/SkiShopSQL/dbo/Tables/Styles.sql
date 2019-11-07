CREATE TABLE [dbo].[Styles] (
    [StyleId]      INT            NOT NULL,
    [StyleNo]      CHAR (6)       NOT NULL,
    [StyleName]    NVARCHAR (200) NOT NULL,
    [BrandId]      SMALLINT       NOT NULL,
    [CategoryId]   TINYINT        NOT NULL,
    [GenderId]     TINYINT        NOT NULL,
    [ImageBig]     NVARCHAR (300) NOT NULL,
    [ImageSmall]   NVARCHAR (300) NOT NULL,
    [PriceCurrent] DECIMAL (8, 2) NOT NULL,
    [PriceRegular] DECIMAL (8, 2) NOT NULL,
    [SoftDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Styles] PRIMARY KEY CLUSTERED ([StyleId] ASC),
    CONSTRAINT [CK_Styles_StyleNo] CHECK ([StyleNo] like '[0-9][0-9][0-9][0-9][0-9][0-9]'),
    CONSTRAINT [FK_Styles_Brands] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[Brands] ([BrandId]),
    CONSTRAINT [FK_Styles_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([CategoryId]),
    CONSTRAINT [FK_Styles_Genders] FOREIGN KEY ([GenderId]) REFERENCES [dbo].[Genders] ([GenderId]),
    CONSTRAINT [AK_Styles_StyleNo] UNIQUE NONCLUSTERED ([StyleNo] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Styles_BrandId]
    ON [dbo].[Styles]([BrandId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Styles_CategoryId]
    ON [dbo].[Styles]([CategoryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Styles_GenderId]
    ON [dbo].[Styles]([GenderId] ASC);


GO

CREATE TRIGGER [dbo].[Trigger_Styles_Del_Prevent]
    ON [dbo].[Styles]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;
GO

CREATE TRIGGER [dbo].[Trigger_Styles_Insert]
    ON [dbo].[Styles]
    AFTER INSERT
    AS
    BEGIN
        SET NoCount ON;
		SET ROWCOUNT 0;

		BEGIN TRY
			INSERT INTO dbo.StyleStates (StyleId)
					SELECT StyleId FROM inserted;
		END TRY
		BEGIN CATCH
			IF XACT_STATE() <> 0 
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH
    END