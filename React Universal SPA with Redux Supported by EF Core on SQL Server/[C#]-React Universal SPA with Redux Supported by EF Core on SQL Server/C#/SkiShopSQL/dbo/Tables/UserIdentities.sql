CREATE TABLE [dbo].[UserIdentities] (
    [UserId]     INT            IDENTITY (1, 1) NOT NULL,
    [Email]      NVARCHAR (100) NOT NULL,
    [LastName]   NVARCHAR (50)  NOT NULL,
    [FirstName]  NVARCHAR (50)  NOT NULL,
    [ScreenName] NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [AK_UserProfiles_Email] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [AK_UserProfiles_ScreenName] UNIQUE NONCLUSTERED ([ScreenName] ASC)
);


GO

CREATE TRIGGER [dbo].[Trigger_UserIdentities_Del_Prevent]
    ON [dbo].[UserIdentities]
    INSTEAD OF DELETE
    AS
    BEGIN
        ;THROW 50001, 'Records cannot be deleted', 1;
    END;