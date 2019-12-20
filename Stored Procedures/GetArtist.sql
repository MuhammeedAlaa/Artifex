
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetArtist
@Email VARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT A.* FROM ARTIST A JOIN [dbo].[USER] U ON U.[USER_NAME]=A.ARTIST_UNAME
	WHERE U.EMAIL=@Email

END
GO
