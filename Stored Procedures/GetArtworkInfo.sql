
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetArtworkInfo
@Tittle VARCHAR(20)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM ARTWORK
	WHERE TITLE=@Tittle
END
GO
