 --Username:Lovro Despot (prva osoba u bazi)
 --Password: 123


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER    PROCEDURE [dbo].[AddNewTagToApartment]
@tagID INT,
@apartmentID INT,
@guid uniqueidentifier 
AS
insert into TaggedApartment (TagId,ApartmentId, Guid) values (@tagID,@apartmentID, @guid)


CREATE OR ALTER   procedure [dbo].[AddPictures]
@apartmentId int,
@path nvarchar(max)
as
insert into ApartmentPicture(ApartmentId,Path,Name,CreatedAt,Guid)
values (@apartmentId,@path,'Slika',GETDATE(),NEWID())
GO

 CREATE OR  ALTER   procedure [dbo].[AddTag]
 @typeId int,
 @name nvarchar(50)
 as
 insert into Tag([Name],TypeId,CreatedAt,Guid)
 values (@name,@typeId,GETDATE(),NEWID())
 GO

CREATE OR  ALTER   PROC [dbo].[AddTaggedApartment]
	@apartmentName NVARCHAR(250),
	@tagName NVARCHAR(250)
AS
BEGIN
	INSERT INTO TaggedApartment(ApartmentId, TagId)
						 VALUES((SELECT Id FROM Apartment WHERE Name = @apartmentName), (SELECT Id FROM Tag WHERE Name = @tagName))
END
GO

--procedura potrebna za login (Auth u app(IRepo))
CREATE OR ALTER   proc [dbo].[AuthUser]
	@username NVARCHAR(50),
	@password NVARCHAR(128)
AS
BEGIN
	SELECT * FROM AspNetUsers WHERE Username=@username AND PasswordHash=@password AND DeletedAt IS NULL
END
GO

CREATE OR ALTER   procedure [dbo].[CreateApartment]
  @nameENG nvarchar(50),
  @price int,
  @maxAdults int,
  @maxChildren int,
  @totalRooms int,
  @beachDistance nvarchar(50),
  @cityID int,
  @ownerID int,
  @address nvarchar(150),
  @statusId int
  as
  insert into Apartment(Guid,Name,NameEng,Price,MaxAdults,MaxChildren,
  TotalRooms,BeachDistance,CityId,OwnerId, TypeId, StatusId, Address,
  CreatedAt)
  values (NEWID(), @nameENG, @nameENG,CONVERT(int, @price),@maxAdults,@maxChildren,@totalRooms,@beachDistance,@cityID,@ownerID,999,@statusId, @address,getdate())
  GO

  ----------------------------------------------------------Review-----------------------------------------------------
CREATE OR ALTER   procedure [dbo].[CreateNewReview]
@userId int,
@numberOfStars int,
@description nvarchar(max),
@apartmentId int
as
insert into ApartmentReview(Guid, CreatedAt, ApartmentId, UserId, Details, Stars)
values (NEWID(),GETDATE(),@apartmentId,@userId,@description,@numberOfStars)
GO
----------------------------------------------------------------USER--------------------------------------------------
CREATE OR ALTER   procedure [dbo].[CreatUser]
@email NVARCHAR(50),
@username NVARCHAR(50),
@password NVARCHAR(max),
@address NVARCHAR(50),
@phone NVARCHAR(50)
as
insert into AspNetUsers(Guid, CreatedAt, Email, UserName,PasswordHash, Address, PhoneNumber, EmailConfirmed, PhoneNumberConfirmed, LockoutEnabled,AccessFailedCount)
values(NEWID(),GETDATE(),@email,@username,@password,@address,@phone,1,1,0,0)
GO

CREATE OR ALTER   PROCEDURE [dbo].[DeleteApartment]
@apartmentId int
as
update Apartment
set DeletedAt = GETDATE()
where Id = @apartmentId
GO


CREATE OR ALTER   procedure [dbo].[DeleteApartmentPictureByID]
@pictureID int
AS
UPDATE ApartmentPicture
SET DeletedAt=GETDATE()
where Id = @pictureID
GO

CREATE OR  ALTER   proc [dbo].[DeleteApartmentTag]
 @tagId int,
 @apartmentId int
 as 
 delete from TaggedApartment
 where @apartmentId=ApartmentId and @tagId=TagId
 GO

CREATE OR  ALTER   PROCEDURE [dbo].[DeleteApartmentTagByID]
@tagID INT,
@apartmentID INT
AS
DELETE from TaggedApartment
where @apartmentID = ApartmentId and @tagID = TagId
GO

 -- DELETE TAG


CREATE OR  ALTER   PROCEDURE [dbo].[DeleteTag]
@Id int
as
delete from Tag
where Id=@Id
GO


CREATE OR ALTER   proc [dbo].[EditApartment]
@apartmentId int,
@apartmentName nvarchar(50),
@price decimal,
@maxAdults int,
@maxChildren int,
@beachDistance int,
@statusId int,
@totalRooms int
as
UPDATE Apartment
set Name=@apartmentName,Price=@price,MaxAdults=@maxAdults,MaxChildren=@maxChildren,BeachDistance=@beachDistance,StatusId=@statusId,TotalRooms=@totalRooms
where Id=@apartmentId
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetAllApartments]
AS
SELECT Id, CreatedAt, Address, OwnerId, StatusId, CityId, Address, Name, NameEng, CONVERT(int, Price) AS Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance
FROM Apartment
where DeletedAt is null
GO


CREATE OR ALTER   PROCEDURE [dbo].[GetAllApartmentsByStatusId]
@statusId int
as
SELECT Id, CreatedAt, Address, OwnerId, StatusId, CityId, Address, Name, NameEng, CONVERT(int, Price) AS Price, MaxAdults, MaxChildren, TotalRooms, BeachDistance
from Apartment 
where StatusId=@statusId and DeletedAt is null
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetAllCities]
AS
SELECT Id,[Name]
FROM City 
GO


CREATE OR ALTER   PROCEDURE [dbo].[GetAllUsers]
AS
SELECT * FROM AspNetUsers
WHERE DeletedAt IS NULL
GO

CREATE OR ALTER   proc [dbo].[GetApartmentByID]
@apartmentId int
as 
select * 
from Apartment
where DeletedAt is null AND Id=@apartmentId
GO

CREATE OR ALTER   procedure [dbo].[GetApartmentId]
@name nvarchar(50),
@address nvarchar(50),
@rooms int,
@adults int,
@children int
as
select [Name], [Address],Id
from Apartment
where [Name]=@name and [Address]=@address and TotalRooms=@rooms and MaxChildren=@children and MaxAdults=@adults
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetApartmentOwnerById]
@ID int
as
SELECT Id, Name
FROM [dbo].[ApartmentOwner] WHERE Id = @ID
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetApartmentOwners]
AS
SELECT Id, [Name]
FROM dbo.ApartmentOwner
GO

-- (Public procedure)
CREATE OR ALTER   PROCEDURE [dbo].[GetApartmentPictures]  
as
select * 
from ApartmentPicture
where DeletedAt is null
GO


CREATE OR ALTER    PROCEDURE [dbo].[GetApartmentPicturesByID]
@id int
AS
SELECT * FROM ApartmentPicture
WHERE ApartmentId = @id AND DeletedAt is null
GO

--get apartement
CREATE OR ALTER   PROC [dbo].[GetApartments]
AS
BEGIN
	select *
	from Apartment
END
GO

CREATE OR ALTER   procedure [dbo].[GetApartmentsByStatusId]
@StatusID INT
as
select * 
from Apartment
where DeletedAt is null and StatusId=@StatusID
GO

CREATE OR ALTER   proc [dbo].[GetApartmentStatus]
@apartmentId int
as 
select StatusId
from Apartment
where Id=@apartmentId
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetApartmentTags]
@Id INT
AS
SELECT t.Id,t.Name, t.NameEng, tt.NameEng AS 'TagTypeNameEng', tt.Id as 'TagTypeID'
FROM TaggedApartment AS ta
INNER JOIN Tag AS t ON ta.TagId = t.Id
INNER JOIN Apartment AS a ON ta.ApartmentId = a.Id
INNER JOIN TagType AS tt ON t.TypeId = tt.Id
WHERE a.Id = @Id
GO

CREATE OR ALTER   procedure [dbo].[GetCities]
as
select *
from City
GO

CREATE OR ALTER   PROC [dbo].[GetCityByID]
@id int
AS
SELECT * FROM City WHERE Id = @id
GO

CREATE OR ALTER   PROC [dbo].[GetCityName]
@cityId int
as
select * 
from City
where Id=@cityId
GO

CREATE OR  ALTER   procedure [dbo].[GetImagePath]
@imageId int
as
select Id, Path
from ApartmentPicture
where Id=@imageId
GO

CREATE OR ALTER   procedure [dbo].[GetNumberOfApartmentPictures]
@apartmentId int
as 
select COUNT(*) as 'NumberOfPictures' from ApartmentPicture
where ApartmentId = @apartmentId AND DeletedAt is null
GO

CREATE OR ALTER   proc [dbo].[GetOwnerName]
@ownerId int
as
select * 
from ApartmentOwner
where Id=@ownerId
GO


CREATE OR ALTER   procedure [dbo].[GetOwners]
as
select * 
from ApartmentOwner
GO

CREATE OR ALTER   procedure [dbo].[GetReview]
@apartmentId int
as 
select *
from ApartmentReview
where ApartmentId=@apartmentId
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetStatuses]
as
select *
from ApartmentStatus
GO

CREATE OR ALTER   procedure [dbo].[GetTaggedApartments]
@tagId int
as 
select COUNT(*) as 'TaggedApartments'
from TaggedApartment 
where TagId = @tagId
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetTags]
as
select Id, [NameEng] 
from Tag
GO

CREATE OR ALTER   procedure [dbo].[GetTypeNameEng]
@typeID int
as
select TagType.NameEng
from TagType
where Id = @typeID
GO

CREATE OR ALTER   procedure [dbo].[GetUnregisteredUser]
@apartmentId int
as
select UserName, UserEmail as 'Email', UserPhone as 'PhoneNumber', Details, UserAddress as 'Address'
from ApartmentReservation
where ApartmentId=@apartmentId and UserId is null
GO

CREATE OR ALTER     PROCEDURE [dbo].[GetUnusedTagsOnApartment]
@apartmentID INT
AS
select t.Id, t.NameEng, tt.NameEng as 'TagTypeNameEng', tt.Id as 'TagTypeID' from tag AS t
inner join TagType as tt on tt.Id = t.TypeId
where t.Id not in(select TaggedApartment.TagId from TaggedApartment where TaggedApartment.ApartmentId = @apartmentID) 
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetUsedTags]
@Id int
as
select count(*) as 'Used'
from TaggedApartment
where TagId = @Id
GO

CREATE OR ALTER   PROCEDURE [dbo].[GetUserByID]
@apartmentId INT
AS
SELECT ar.UserId as 'Id', au.UserName as 'UserName', ar.Details as 'Details'
FROM ApartmentReservation as ar
inner join AspNetUsers as au on ar.UserId=au.Id
WHERE ApartmentId = @apartmentId and UserId is not null
GO

CREATE OR ALTER   PROCEDURE [dbo].[LoadAllTags]
as
SELECT t.Id, t.Name, t.NameEng, tt.Id as 'TagTypeID', tt.Name as 'TagTypeName', tt.NameEng from Tag as t
INNER JOIN TagType as tt on t.TypeId = tt.Id 
GO

CREATE OR ALTER   PROCEDURE [dbo].[LoadAllTagTypes]
AS
SELECT NameEng as 'TagTypeNameEng'
FROM TagType
GO

--slike prebrojavanje (koliko ih ima po apartmanu)
CREATE OR ALTER   PROC [dbo].[LoadApartmentPictures]
AS
BEGIN
	SELECT aptP.Id,aptP.Guid,aptP.CreatedAt,aptP.DeletedAt,aptP.Path,aptP.Name,
	(
	select count(*)
	from Apartment as apt
	where apt.Id = aptP.Id
	) AS ApartmentPictures
	
	FROM ApartmentPicture AS aptP
	inner join Apartment as ap on ap.Id = aptP.ApartmentId
END
GO

CREATE OR ALTER   proc [dbo].[LoadApartments]
AS
SELECT * FROM Apartment
WHERE DeletedAt IS NULL
GO


CREATE OR ALTER   PROC [dbo].[LoadApartmentTags]
@apartmentID INT
as
select ta.TagId as 'Id', t.Name, tt.Name as 'Type'
from TaggedApartment as ta
inner join Tag as t on ta.TagId = t.Id
inner join Apartment as a on ta.ApartmentId = a.Id
inner join TagType as tt on t.TypeId=tt.Id
where a.Id=@apartmentID
GO

CREATE OR ALTER   procedure [dbo].[LoadCities]
AS
BEGIN
	SELECT * FROM City
END
GO

CREATE OR ALTER   procedure [dbo].[LoadOwners]
AS
BEGIN
	SELECT * FROM ApartmentOwner
END
GO

CREATE OR ALTER   procedure [dbo].[LoadStatus]
AS
BEGIN
	SELECT * FROM ApartmentStatus
END
GO

--tagani apartmani u tablici tagova (koliko ih ima)
CREATE OR ALTER   PROC [dbo].[LoadTag]
AS
BEGIN
	SELECT t.Id,
	(
	select count(*)
	from TaggedApartment as taggApt
	where taggApt.TagId = t.Id
	) 
	
	FROM Tag AS t
	inner join TagType as tagty on tagty.Id = t.TypeId
END
GO

--tagani apartmani (koliko ih ima tagganih)
CREATE OR ALTER   PROC [dbo].[LoadTags]
as
SELECT t.Id, t.Name, t.NameEng, tt.Id as 'TagTypeID', tt.Name as 'TagTypeName', tt.NameEng from Tag as t
INNER JOIN TagType as tt on t.TypeId = tt.Id 
GO

 CREATE OR ALTER   procedure [dbo].[LoadTagType]
 as
 select Name as 'Type'
 from TagType
 GO

 --loadanje usera za Registered users
 CREATE OR ALTER   procedure [dbo].[LoadUsers]
AS
BEGIN
	SELECT * FROM AspNetUsers
END
 GO

 CREATE OR ALTER   PROCEDURE [dbo].[SetRepresentativePicture]
@apartmentId int,
@imgId int
AS
update ApartmentPicture
set IsRepresentative = 0
where ApartmentId = @apartmentId
update ApartmentPicture
set IsRepresentative = 1
where Id = @imgId
 GO

 --updateanje apartmana
 CREATE OR ALTER   PROCEDURE [dbo].[UpdateApartment]
@nameEng nvarchar(50),
@maxChildren int,
@maxAdults int,
@beachDistance int,
@price int,
@statusID int,
@OwnerId int,
@Rooms int,
@apartmentID int,
@address nvarchar(80)
AS
update Apartment
set NameEng = @nameEng, MaxChildren = @maxChildren, MaxAdults = @maxAdults, BeachDistance = @beachDistance, Price = CONVERT(int, @price),
	StatusId = @statusID, OwnerId = @OwnerId, TotalRooms = @Rooms, Address=@address
where Id = @apartmentID
 GO

 --upoloadanje slika
 CREATE OR ALTER   procedure [dbo].[UploadPictures]
@path nvarchar(200)
as
begin
insert into ApartmentPicture(Path)
values (@path)
end
 GO
