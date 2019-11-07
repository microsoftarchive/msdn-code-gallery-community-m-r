CREATE TABLE [dbo].[Reviews] (
    [ReviewId]           INT            IDENTITY (1, 1) NOT NULL,
    [UserId]             INT            NOT NULL,
    [StyleId]            INT            NOT NULL,
    [Rating]             TINYINT        NOT NULL,
    [ReviewText]         NVARCHAR (MAX) NOT NULL,
    [CreatedDateTime]    DATETIME2 (7)  NOT NULL,
    [CreatedDateTimeUTC] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED ([ReviewId] ASC),
    CONSTRAINT [CK_Reviews_Rating] CHECK ([Rating]>=(1) AND [Rating]<=(5)),
    CONSTRAINT [CK_Reviews_Text] CHECK ([ReviewText]<>''),
    CONSTRAINT [FK_Reviews_Styles] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([StyleId]),
    CONSTRAINT [FK_Reviews_UserIdentities] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserIdentities] ([UserId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId]
    ON [dbo].[Reviews]([UserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reviews_StyleId]
    ON [dbo].[Reviews]([StyleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Reviews_CreatedDateTime]
    ON [dbo].[Reviews]([CreatedDateTime] ASC);


GO

CREATE TRIGGER [dbo].[Trigger_Reviews_Del_Prevent]
    ON [dbo].[Reviews]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;
GO

CREATE TRIGGER [dbo].[Trigger_Reviews_Insert]
    ON [dbo].[Reviews]
    FOR INSERT
    AS
    SET NoCount ON;
		SET ROWCOUNT 0;

		BEGIN TRY
			UPDATE dbo.StyleStates 
					SET AverageRatings = (ss.AverageRatings * ss.ReviewCount + inserted.Rating) / (ss.ReviewCount + 1),
						ReviewCount = ss.ReviewCount + 1
					FROM inserted
					INNER JOIN dbo.StyleStates ss ON inserted.StyleId = ss.StyleId;
		END TRY
		BEGIN CATCH
			IF XACT_STATE() <> 0 
				ROLLBACK TRANSACTION;
				THROW;
		END CATCH