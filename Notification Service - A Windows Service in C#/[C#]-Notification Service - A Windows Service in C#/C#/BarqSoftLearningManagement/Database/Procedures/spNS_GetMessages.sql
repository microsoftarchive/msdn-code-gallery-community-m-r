SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spNS_GetMessages]
AS
BEGIN
/******************************************************************************
**	Name:			spNS_GetMessages.sql
**
**	Test:			EXEC spNS_GetMessages
**
**	Description:	This procedure returns all the unprocessed message
**
**	Called by:		
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

	SELECT
	 Id
	, [Message]
	, ReceivedDatetime
	, [Status]
	FROM [dbo].[tblMessage]
	WHERE [Status] = 1
	
	SELECT	  @iError		= @@ERROR
			, @iRowCount	= @@ROWCOUNT

	RETURN @iError
END