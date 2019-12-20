SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE InsertArtist
@UserName VARCHAR(20),
@Bio VARCHAR(200),
@BrithYear INT ,
@StrSalary INT,
@EndSalary INT
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO ARTIST VALUES (@UserName,@Bio,@BrithYear,@StrSalary,@EndSalary)


END
GO
