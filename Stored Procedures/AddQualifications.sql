
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ADD_QUALIGICATIONS]
@USERNAME VARCHAR(20),
@QUALI VARCHAR(20)
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO EXP_QUALIFICATIONS VALUES (@USERNAME,@QUALI)
END
GO
