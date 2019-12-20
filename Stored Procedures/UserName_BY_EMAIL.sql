
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE USERNAME_BY_EMAIL
@Email  VARCHAR(50)

AS
BEGIN

	SET NOCOUNT ON;
	SELECT [USER_NAME] FROM dbo.[USER]
	WHERE EMAIL=@Email
END
GO
