USE [Boss]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spNS_UpdateStatus]
(
@iMessageId	int
, @iStatus int
)
AS
BEGIN
/******************************************************************************
**	Name:			spNS_UpdateStatus.sql
**
**	Test:			EXEC spNS_UpdateStatus
**
**	Description:	This procedure updates the message status
**
**	Called by:		Boss.NotificationService
**
**	Return values:  0 - Success
**					N - Failure (where n is the error code from SQL Server)
**
**	Author:			Ghouse
**
**	Date:			10/01/2011
*******************************************************************************
**		Modification History
*******************************************************************************
**  Log:
*******************************************************************************/
	SET NOCOUNT ON

	DECLARE   @iError		int
			, @iRowCount	int

	UPDATE [dbo].[tblMessage] 
	SET [Status] = @iStatus
	WHERE Id = @iMessageId
	
	SELECT	  @iError		= @@ERROR
			, @iRowCount	= @@ROWCOUNT

	RETURN @iError
END