/****** Object:  Table [dbo].[tblMessage]    Script Date: 10/05/2011 20:28:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](100) NULL,
	[ReceivedDatetime] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_tblMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


