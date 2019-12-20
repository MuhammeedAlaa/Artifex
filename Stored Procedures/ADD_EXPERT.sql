
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ADD_EXPERT

@USER_NAME VARCHAR(20),
@Bio VARCHAR(200),
@BYEAR INT
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO [EXPERT] VALUES (@USER_NAME,@BIO,@BYEAR)

END
GO
