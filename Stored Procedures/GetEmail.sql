
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetEmail
@UserName varchar(20)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT EMAIL FROM dbo.[USER]
	WHERE [USER_NAME]=@UserName 
  
END
GO
