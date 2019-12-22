USE ARTIFEX
GO 


CREATE PROCEDURE AddAdmin 
	@EMAIL VARCHAR(50) ,
	@PASSWORD VARCHAR(70) ,
	@SALARY INT
AS
BEGIN
	INSERT INTO [ADMIN] VALUES (@EMAIL,HASHBYTES('SHA1',@PASSWORD),@SALARY)
END
GO

/**************************************************************/
CREATE PROCEDURE SignIn
	
   @EMAIL VARCHAR(50) ,
   @PASSWORD VARCHAR(70)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.[USER] 
	WHERE EMAIL=@EMAIL AND HASHBYTES('SHA1',@PASSWORD) IN (SELECT PASSWORD FROM dbo.[USER])
 
END;
GO
/******************************************/
CREATE PROCEDURE SignInAdmin
	
   @EMAIL VARCHAR(50) ,
   @PASSWORD VARCHAR(70)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM dbo.[ADMIN] 
	WHERE EMAIL=@EMAIL AND HASHBYTES('SHA1',@PASSWORD) IN (SELECT PASSWORD FROM dbo.[ADMIN])
 
END;
GO
/******************************************/
CREATE PROCEDURE InsertRating
   @uname varchar(20),
   @AWCODE int,
   @rating int 
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO RATE_AW VALUES(@uname,@AWCODE,@rating);
	UPDATE ARTWORK SET STATUS =0 WHERE AW_CODE=@AWCODE;
END;
GO
/******************************************/
CREATE PROCEDURE OldRating

AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT SUM(RATING) AS SUM,COUNT(*) AS COUNT, A.ARTIST_UNAME AS ARTIST  FROM RATE_AW AS RW JOIN ARTWORK A ON RW.AW_CODE = A.AW_CODE GROUP BY A.ARTIST_UNAME
END;
GO
/******************************************/
CREATE PROCEDURE GetAdminID
	
   @EMAIL VARCHAR(50) ,
   @PASSWORD VARCHAR(70)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT ADMIN_ID FROM dbo.[ADMIN] 
	WHERE EMAIL=@EMAIL AND HASHBYTES('SHA1',@PASSWORD) IN (SELECT PASSWORD FROM dbo.[ADMIN])
 
END;
GO
/******************************************/
CREATE PROCEDURE SignUp
	-- Add the parameters for the stored procedure here
	@USER_NAME VARCHAR(20) ,
	@EMAIL VARCHAR(50) ,
	@PASSWORD VARCHAR(70) ,
	@FNAME VARCHAR(10) ,
	@MINIT CHAR(1) ,
	@LNAME VARCHAR(20) ,
	@PHONE VARCHAR(13),
	@PROFILE_PIC VARCHAR(max)=null
	
AS
BEGIN

	SET NOCOUNT ON;
	BEGIN
    INSERT INTO [USER] VALUES (@USER_NAME,@EMAIL,HASHBYTES('SHA1',@PASSWORD),@FNAME,@MINIT,@LNAME,@PHONE,@PROFILE_PIC)
	END
	IF (@PROFILE_PIC=NULL)
	BEGIN 
	Update dbo.[USER] Set PROFILE_PIC= '~/Images/def.png' where EMAIL=@EMAIL
    END
END
GO
/********************************************/
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
/*******************************************/
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
/*******************************************************/
CREATE PROCEDURE AddQualifications
	@USERNAME VARCHAR(20),
	@QUALI VARCHAR(20)
AS
BEGIN
	
	SET NOCOUNT ON;
	INSERT INTO EXP_QUALIFICATIONS VALUES (@USERNAME,@QUALI)
END
GO
/****************************************************/
CREATE PROCEDURE EmailAvailable

	 @Email VARCHAR(20) 
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM dbo.[USER] 
	WHERE EMAIL=@Email

END
GO
/*****************************************/
CREATE PROCEDURE GetArtist
	@Email VARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT A.* FROM ARTIST A JOIN [dbo].[USER] U ON U.[USER_NAME]=A.ARTIST_UNAME
	WHERE U.EMAIL=@Email

END
GO

/*********************************************/
CREATE PROCEDURE GetArtworkInfo
	@Title VARCHAR(20)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT * FROM ARTWORK
	WHERE TITLE=@Title
END
GO
/*************************************/
CREATE PROCEDURE GetArtworks
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM ARTWORK WHERE  PRIVACY = 0  
  
END
GO
/**************************************/
CREATE PROCEDURE GetEmail
	@UserName varchar(20)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT EMAIL FROM dbo.[USER]
	WHERE [USER_NAME]=@UserName 
  
END
GO
/**************************************/
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
/*******************************************/
CREATE PROCEDURE  GetOrderById
	@Id INT 
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM [ORDER] 
	WHERE ORDER_ID=@Id

END
GO
/*******************************************/
CREATE PROCEDURE GetPassword

	@Email varchar(50)
AS
BEGIN

	SET NOCOUNT ON;
    SELECT PASSWORD FROM dbo.[USER]
	WHERE EMAIL=@Email

END
GO
/*******************************************/
CREATE PROCEDURE GetProposedArtworksByArtist
	@NAME varchar(20)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM ARTWORK
	WHERE ADMIN_ID IS NULL AND ARTIST_UNAME LIKE '%'+@NAME+'%'

END
GO
/*****************************************/
CREATE PROCEDURE GetReportById
	@Id INT
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM REPORT 
	WHERE REPORT_ID=@Id

END
GO
/******************************************/
CREATE PROCEDURE GetSortedOrders
	@Criteria varchar(50),
	@asc varchar(10)
AS
BEGIN

	DECLARE @QUERY  varchar(300)
	SET NOCOUNT ON;
	SELECT @QUERY='SELECT * FROM dbo.[ORDER] ORDER BY '+@Criteria+' ' +@asc
    EXEC(@QUERY)

END
GO
/*****************************************/
CREATE PROCEDURE GetSortedProposedArtworks
    @Criteria varchar(50),
    @asc varchar(10)
AS
BEGIN

	DECLARE @QUERY  varchar(300)
	SET NOCOUNT ON;
	SELECT @QUERY='SELECT * FROM Artwork WHERE ADMIN_ID IS NULL ORDER BY '+@Criteria+' ' +@asc
    EXEC(@QUERY)

END
GO
/******************************************/
CREATE PROCEDURE GetSortedReports
	@Criteria varchar(50),
	@asc varchar(10)
AS
BEGIN

	DECLARE @QUERY  varchar(300)
	SET NOCOUNT ON;
	SELECT @QUERY='SELECT * FROM dbo.[REPORT] ORDER BY '+@Criteria+' ' +@asc
    EXEC(@QUERY)

END
GO
/*******************************************/
CREATE PROCEDURE IsArtist
	@Email varchar(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT COUNT(*) FROM ARTIST A JOIN dbo.[USER] U ON A.ARTIST_UNAME=U.USER_NAME
	WHERE EMAIL=@Email
  
END
GO
/*****************************************/
CREATE PROCEDURE  IsExpert
	@Email varchar(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT COUNT(*) FROM EXPERT E JOIN dbo.[USER] U ON E.EXPERT_UNAME=U.USER_NAME
	WHERE EMAIL=@Email

END
GO
/*****************************************/
CREATE PROCEDURE ProfileImagePath
	@email  VARCHAR(50)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT PROFILE_PIC FROM dbo.[USER] 
	WHERE EMAIL=@email
	
END
GO
/*******************************************************/
CREATE PROCEDURE UpdatePassword
	@Email VARCHAR(50),
	@NewPassword varchar(70)
AS
BEGIN
	
	SET NOCOUNT ON;
	UPDATE dbo.[USER] 
	SET PASSWORD=@NewPassword
	WHERE EMAIL=@Email
   
END
GO
/*************************************************/
CREATE PROCEDURE USERNAME_BY_EMAIL
	@Email  VARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT [USER_NAME] FROM dbo.[USER]
	WHERE EMAIL=@Email

END
GO
/************************************************/
CREATE PROCEDURE UserNameAvailable
    @Username VARCHAR(20) 
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT COUNT(*) FROM dbo.[USER] 
	WHERE USER_NAME=@Username

END
GO
/*****************Start from here*******************************/
CREATE PROCEDURE SellTicket
      @title VARCHAR(20) 
AS
BEGIN

	SET NOCOUNT ON;
	UPDATE [dbo].[EVENT] SET TICKETS_NUM=TICKETS_NUM -1
	WHERE TITLE=@title

END
GO
/***********************************************/
CREATE PROCEDURE GetEventInfo
      @title VARCHAR(20) 
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM dbo.[EVENT]
	WHERE TITLE=@title

END
GO
/*****************************************************/
CREATE PROCEDURE EditEvent
    @title VARCHAR(20) ,
	@ADMIN_ID INT,
	@IMAGE VARCHAR(MAX),
	@TICKET_PRICE INT,
	@EVENTDATE DATE,
	@LOCATION VARCHAR(20),
	@TICKETS_NUM INT,
	@INFO VARCHAR(200),
	@oldtitle VARCHAR(20)
AS
BEGIN

	SET NOCOUNT ON;
	UPDATE dbo.[EVENT] SET
	TITLE=@title,ADMIN_ID=@ADMIN_ID,[IMAGE]=@IMAGE,TICKET_PRICE=@TICKET_PRICE,
	EVENTDATE=@EVENTDATE,LOCATION=@LOCATION,TICKETS_NUM=@TICKETS_NUM,INFO=@INFO
	WHERE TITLE=@oldtitle

END
GO
/********************************************************/
CREATE PROCEDURE GetEvents
     @NowDate Date 
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM dbo.[EVENT]
	WHERE EVENTDATE>=@NowDate

END
GO
/*********************************************************/
CREATE PROCEDURE GetArtWorksByArtist
     @Artist_UName VARCHAR(20)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM ARTWORK
	WHERE ARTIST_UNAME=@Artist_UName

END
GO
/********************************************************/
CREATE PROCEDURE GetCategories
   
AS
BEGIN

	SET NOCOUNT ON;
	SELECT NAME FROM CATEGORY

END
GO
/********************************************************/
CREATE PROCEDURE InsertArtwork
--CATEGORY_NAME /\ ARTIST_UNAME /\ ADMIN_ID-NULL /\ TITLE /\ ACCEPTED-0 /\ PRIVACY /\ STATUS-1
--DESCRIPTION /\ WIDTH /\ HEIGHT /\ DEPTH /\ PRICE /\ MATERIAL /\ MEDIUM  /\ SUBJECT /\ PHOTO /\ YEAR
@CATEGORY_NAME VARCHAR(10),
@ARTIST_UNAME VARCHAR(20),
--@ADMIN_ID INT,
@TITLE VARCHAR(20),
--@ACCEPTED BIT,
@PRIVACY BIT,
--@STATUS BIT,
@DESCRIPTION VARCHAR(100),
@WIDTH INT,
@HEIGHT INT,
@DEPTH INT,
@PRICE INT,
@MATERIAL VARCHAR(10),
@MEDIUM VARCHAR(10),
@SUBJECT VARCHAR(10),
@PHOTO VARCHAR(MAX),
@YEAR INT
AS
BEGIN

	SET NOCOUNT ON;
	INSERT INTO ARTWORK VALUES
	(@CATEGORY_NAME,@ARTIST_UNAME,NULL,@TITLE,0,@PRIVACY,1,@DESCRIPTION,@WIDTH,@HEIGHT,@DEPTH,@PRICE,@MATERIAL,@MEDIUM,@SUBJECT,@PHOTO,@YEAR);

END
GO
/********************************************************/
CREATE PROCEDURE GetRequestedSurvey
@EXPERT_UNAME VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM SURVEY WHERE EXPERT_UNAME= @EXPERT_UNAME
END
GO
/********************************************************/
CREATE PROCEDURE GetSurveyInfo
@code INT 
AS
BEGIN
	SET NOCOUNT ON
		SELECT * FROM SURVEY WHERE SURVEY_ID = @code
	END
GO
/********************************************************/
CREATE PROCEDURE InsertSurveyRequest
 --EXPERT_UNAME /\ USER_NAME /\ BUDGET /\ MORE_INFO /\ [RATING-NULL]
 @EXPERT_UNAME VARCHAR(20),
 @USER_NAME VARCHAR(20),
 @BUDGET INT,
 @MORE_INFO VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON
		INSERT INTO SURVEY VALUES
		(@EXPERT_UNAME,@USER_NAME,@BUDGET,@MORE_INFO,NULL);
	END
GO
/********************************************************/
CREATE PROCEDURE GetArtworkWithCode
@code INT  
AS
BEGIN
	SET NOCOUNT ON
		SELECT * FROM Artwork WHERE AW_CODE = @code	
	END
GO
/********************************************************/
CREATE PROCEDURE InsertBillingInfo
--CARD_NUM /\ uname /\ STREET /\ CITY /\ CARD_HOLDER_NAME /\ CVV /\ EXPIRY_DATE
@CARD_NUM VARCHAR(18),
@uname VARCHAR(20),
@STREET VARCHAR(20),
@CITY VARCHAR(10),
@CARD_HOLDER_NAME VARCHAR(20),
@CVV INT,
@EXPIRY_DATE DATE
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO BILLING_INFO VALUES
	(@CARD_NUM,@uname,@STREET,@CITY,@CARD_HOLDER_NAME,@CVV,@EXPIRY_DATE);
	END
GO
/********************************************************/
CREATE PROCEDURE ApproveArtwork
@state BIT,
@adminId INT,
@code INT
AS
BEGIN
	SET NOCOUNT ON
		UPDATE ARTWORK 
		SET ACCEPTED = @state , ADMIN_ID = @adminId
		WHERE AW_CODE = @code
END
GO
/*********************************************************/
CREATE PROCEDURE CreateEvent
--TITLE ADMIN_ID IMAGE TICKET_PRICE EVENTDATE LOCATION TICKETS_NUM INFO
@TITLE VARCHAR(20),
@ADMIN_ID INT,
@IMAGE VARCHAR(MAX),
@TICKET_PRICE INT,
@EVENTDATE DATE,
@LOCATION VARCHAR(20),
@TICKETS_NUM INT,
@INFO VARCHAR(200)
AS
BEGIN
	SET NOCOUNT ON
		INSERT INTO EVENT VALUES
		(@TITLE,@ADMIN_ID,@IMAGE,@TICKET_PRICE,@EVENTDATE,@LOCATION,@TICKETS_NUM,@INFO);
END
GO
/*********************************************************/
CREATE PROCEDURE GetArtists

AS
BEGIN
	SELECT * FROM ARTIST
END
GO
/*********************************************************/
CREATE PROCEDURE InviteArtist
@EventTitle VARCHAR(20),
@artist VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
		INSERT INTO INVITED VALUES
		(@EventTitle,@artist);
END
GO
/**********************************************************/
CREATE PROCEDURE GetCompanies

AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM SHIPPING_COMPANY
END
GO
/***********************************************************/
CREATE PROCEDURE Addcompany
-- NAME /\ EMAIL /\ PHONE /\ SHIPPING_FEES
@NAME VARCHAR(20),
@EMAIL VARCHAR(50),
@PHONE VARCHAR(13),
@SHIPPING_FEES INT
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO SHIPPING_COMPANY VALUES
	(@NAME,@EMAIL,@PHONE,@SHIPPING_FEES);
END
GO
/*************************************************************/
CREATE PROCEDURE deleteCompany
@Shipping_name VARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	DELETE FROM SHIPPING_COMPANY WHERE NAME = @Shipping_name
END 
GO
/*************************************************************/
CREATE PROCEDURE GetOrderInfo
@ORDER_ID INT
AS
BEGIN 
	SET NOCOUNT ON
	SELECT * FROM ORDER_INFO WHERE ORDER_ID = @ORDER_ID
END
GO
/*************************************************************/
CREATE PROCEDURE addFav
@code INT,
@uname VARCHAR(20)
AS
BEGIN 
	INSERT INTO FAV_AW VALUES
	(@code,@uname);
END
GO
/***************************************************************/
CREATE PROCEDURE GetFavourite
@EMAIL VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON
	SELECT * 
	FROM FAV_AW FA JOIN ARTWORK A ON FA.AW_CODE = A.AW_CODE JOIN [dbo].[USER] U ON FA.USER_NAME = U.USER_NAME
	WHERE U.EMAIL = @EMAIL
END
GO
/****************************************************************/
CREATE PROCEDURE GetExperts

AS
BEGIN
	SET NOCOUNT ON
	SELECT * FROM EXPERT
END
GO
/*****************************************************************/
CREATE PROCEDURE GetExpertMails

AS
BEGIN
	SET NOCOUNT ON
	SELECT EMAIL FROM [USER] JOIN EXPERT ON EXPERT_UNAME = USER_NAME
END
GO


/****************************************************************/
CREATE PROCEDURE AttendUserEvent 
	@TITLE VARCHAR(20),
	@USER_NAME VARCHAR(20)
AS
BEGIN
    INSERT INTO ATTEND VALUES (@TITLE, @USER_NAME)
END
GO
/************************* Statistics *****************************/
--Number of Users Who attended Each Event hosted Uptill now
CREATE PROCEDURE NumAttendEvent
AS
BEGIN
	SET NOCOUNT ON
	SELECT A.TITLE AS EventTitle, COUNT(*) AS NUMATTENDING 
	FROM [EVENT] E JOIN ATTEND A ON E.TITLE = A.TITLE 
	GROUP BY A.TITLE
END
GO

--Number of orders assigned for each Shipping company 
CREATE PROCEDURE NumOrderShipped
AS
BEGIN
	SET NOCOUNT ON
	SELECT SHIPPING_NAME AS ShippingCompany, COUNT(*) AS NumberOfOrders
	FROM ORDER_INFO
	WHERE SHIPPING_NAME IS NOT NULL
	GROUP BY SHIPPING_NAME
END
GO

--Number of artworks in each category
CREATE PROCEDURE NumArtwrkCategory
AS
BEGIN
	SET NOCOUNT ON
	SELECT CATEGORY_NAME AS Category, COUNT(*) AS NUMOFARTWORKS
	FROM ARTWORK
	GROUP BY CATEGORY_NAME
END
GO

--Number of surveys for each expert
CREATE PROCEDURE NumSurvExp
AS
BEGIN
	SET NOCOUNT ON
	SELECT EXPERT_UNAME AS EXPERT, COUNT(*) AS NUMBER_OF_SURVEYS
	FROM SURVEY
	WHERE EXPERT_UNAME IS NOT NULL
	GROUP BY EXPERT_UNAME
END
GO

--Number of users for our website
CREATE PROCEDURE NumUsers
AS
BEGIN
	SET NOCOUNT ON
	Select Count(*) AS NumOfUsers
	From [User]
END
GO

--Number of artists in our website
CREATE PROCEDURE NumArtists
AS
BEGIN
	SET NOCOUNT ON
	SELECT Count(*) As NumOfArtists 
	FROM Artist
END
GO

--Number of Experts in our website
CREATE PROCEDURE NumExperts
AS
BEGIN
	SET NOCOUNT ON
	Select Count(*) AS NumOfExperts
	From Expert
END
GO

--Total Revenue From an Event
CREATE PROCEDURE EventsRevenue
AS
BEGIN
	SET NOCOUNT ON
	Select E.Title, SUM(TICKET_PRICE) AS EVENT_REVENUE
	FROM ATTEND A JOIN EVENT E ON E.TITLE = A.TITLE
	GROUP BY E.TITLE,E.TICKET_PRICE
END
GO
