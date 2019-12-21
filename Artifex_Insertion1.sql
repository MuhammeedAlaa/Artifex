GO 
USE ARTIFEX

INSERT INTO [dbo].[USER] VALUES
('Alia241','aliam24@gmail.com',HASHBYTES('SHA1','ox1eeM9O'),'Aliaa','M','Ali','7036594912',' '),
('MM2020','mmarwan20@gmail.com',HASHBYTES('SHA1','ohB3amoan'),'Mostafa','A','Marwan','4847397862',' '),
('soha19','soha_ahmed@gmail.com',HASHBYTES('SHA1','aipe6Io0hu'),'Soha','S','Ahmed','7016884367',' '),
('amany23','amany_23@gmail.com',HASHBYTES('SHA1','zoo2Eejei'),'Amany','A','Muhammed','3044215927',' '),
('amgedXD','amgedXD@gmail.com',HASHBYTES('SHA1','IeX6hio8e'),'Muhammed','A','Amged','8635663564',' '),
('bmohab44','bmohab@gmail.com',HASHBYTES('SHA1','ongeiC5d'),'Mohab','B','Islam','2139853701',' '),
('mazen03','mazen03@gmail.com',HASHBYTES('SHA1','uNgaexai7oh'),'Mazen','M','Omar','3038949921',' ');
/*----------------------------*/
INSERT INTO BILLING_INFO VALUES
('4539303552691255','Alia241','9 street','Cairo','Aliaa',194,'2020-09-01'),
('4556732819701754','MM2020','Al_Sood','Cairo','Mostafa',164,'2022-03-24'),
('5340739926780520','soha19','Mossadak','Cairo','Soha',164,'2020-04-12'),
('4929208720629517','amany23','Tomanbay','Cairo','Amany',751,'2021-02-27'),
('4716382280754572','amgedXD','Al Haram','Cairo','Muhammed',309,'2023-11-09'),
('5369266596085937','bmohab44','Haroon','Cairo','Mohab',803,'2023-07-23'),
('5568847857119942','mazen03','Victoria','Alex','Mazen',303,'2020-02-05');
/*----------------------------*/
INSERT INTO ARTIST VALUES 
('amgedXD','',1994,500,2500),
('Alia241','',1990,200,1000),
('mazen03','',1998,700,3000),
('amany23','',1980,300,1700);
/*----------------------------*/
INSERT INTO Expert VALUES 
('bmohab44',' ',1987),
('amany23',' ',1980),
('soha19',' ',1992),
('MM2020',' ',1992)
;
/*----------------------------*/
INSERT INTO CATEGORY VALUES
('PAINTING'),
('DRAWING'),
('DIGITAL');
/*----------------------------*/
INSERT INTO SHIPPING_COMPANY VALUES
('ARAMEX', 'ARAMEX@GMAIL.COM', '7083647283',400),
('DHL', 'DHL@GMAIL.COM', '6984759364', 450)
;
/*----------------------------*/
INSERT INTO ADMIN VALUES
('mahmou@gmail.com',HASHBYTES('SHA1','Mahmoud@12'),450),
('MuhammedH@gmail.com',HASHBYTES('SHA1','MohamedH@12'),450),
('MohamedAl@gmail.com',HASHBYTES('SHA1','MohamedAlaa@12'),450);
/*----------------------------*/
