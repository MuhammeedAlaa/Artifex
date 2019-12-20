
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SignIn
	-- Add the parameters for the stored procedure here
   @EMAIL VARCHAR(50) ,
   @PASSWORD VARCHAR(70)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.[USER] 
	WHERE EMAIL=@EMAIL AND PASSWORD=@PASSWORD
 
END
GO
