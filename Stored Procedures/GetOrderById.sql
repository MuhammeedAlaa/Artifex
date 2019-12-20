
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  GetOrderById
@Id INT 
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM [ORDER] 
	WHERE ORDER_ID=@Id
END
GO
