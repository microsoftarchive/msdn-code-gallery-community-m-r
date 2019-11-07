CREATE DATABASE [OwinSignalR];
GO

USE OwinSignalR;

CREATE TABLE [Application]
(
	ApplicationId INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Application] PRIMARY KEY,
	ApplicationName VARCHAR(250) NOT NULL,
	ApiToken VARCHAR(250) NOT NULL,
	ApplicationSecret VARCHAR(250) NOT NULL
)

CREATE TABLE [ApplicationReferralUrl]
(
	ApplicationReferralUrlId INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ApplicationReferralUrl] PRIMARY KEY,
	ApplicationId INT NOT NULL,
	Url VARCHAR(250) NOT NULL
)

ALTER TABLE [ApplicationReferralUrl]
ADD CONSTRAINT [FK_ApplicationReferralUrl_Application] FOREIGN KEY(ApplicationId)
REFERENCES [Application](ApplicationId)

INSERT INTO [Application] (ApplicationName, ApiToken, ApplicationSecret) VALUES ('OwinSignalR', '48a2000467394b008938cc77b4529e3a', 'Owin and SignalR is cool!');
INSERT INTO ApplicationReferralUrl (ApplicationId, Url) VALUES (SCOPE_IDENTITY(), 'localhost');