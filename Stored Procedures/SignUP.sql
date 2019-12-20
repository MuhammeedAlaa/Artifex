SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SignUp
	-- Add the parameters for the stored procedure here
	@USER_NAME VARCHAR(20) ,
	@EMAIL VARCHAR(50) ,
	@PASSWORD VARCHAR(70) ,
	@FNAME VARCHAR(10) ,
	@MINIT CHAR(1) ,
	@LNAME VARCHAR(20) ,
	@PHONE VARCHAR(13),
	@PROFILE_PIC VARCHAR(max)
	
	 
AS
BEGIN

	SET NOCOUNT ON;

   INSERT INTO [USER] VALUES (@USER_NAME,@EMAIL,@PASSWORD,@FNAME,@MINIT,@LNAME,@PHONE,@PROFILE_PIC)
	
END
GO
