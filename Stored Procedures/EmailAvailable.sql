
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EmailAvailable

	 @Email VARCHAR(20) 
AS
BEGIN
	
	SET NOCOUNT ON;

	SELECT COUNT(*) FROM dbo.[USER] 
	WHERE EMAIL=@Email
END
GO
