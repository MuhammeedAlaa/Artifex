
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetExpert
@Email VARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT E.*,EQ.QUALIFICATIONS FROM EXPERT E JOIN [dbo].[USER] U ON U.[USER_NAME]=E.EXPERT_UNAME
	JOIN EXP_QUALIFICATIONS EQ ON E.EXPERT_UNAME=EQ.EXPERT_UNAME
	WHERE U.EMAIL=@Email
END
GO
