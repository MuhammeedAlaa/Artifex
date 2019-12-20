
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ProfileImagePath
@email  VARCHAR(50)

AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT PROFILE_PIC FROM dbo.[USER] 
	WHERE EMAIL=@email
	
END
GO
