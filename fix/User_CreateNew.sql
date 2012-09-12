-- создание пользователя 
простой скрип - упрощенный вариант dbo.aspnet_Membership_CreateUser 
ALTER PROCEDURE dbo.Member_CreateUser    
    @UserName                               nvarchar(256),
    @Password                               nvarchar(128),    
    @Email                                  nvarchar(256),    
    @UserId                                 uniqueidentifier OUTPUT
AS
BEGIN

	@UserId = NEWID();
	INSERT INTO dbo.aspnet_Membership
                ( UserId, Password, Email, LoweredEmail, CreateDate )
         VALUES ( @UserId, @Password, @Email, LOWER(@Email), @GetDATE() )
END






---------------------------
альтернативная реализация с ролями, организацией и др. параметрами



-- Заполнение ролей из xml
ALTER PROCEDURE [dbo].[Role_CreateByXml]
	@userid			uniqueidentifier,
	@xroles			xml
AS
BEGIN	
	INSERT INTO is_roleuser(roleid, userid)
	SELECT x.r.value('.', 'uniqueidentifier') roleid, @userid
	FROM @xroles.nodes('/Roles/guid') x(r);
END

ALTER PROCEDURE [dbo].[User_CreateNew]
	@login			varchar(50),
	@password		varchar(50),	
	@lastname		nvarchar(50),		
	@firstname		nvarchar(50),		
	@middlename		nvarchar(50)		= null,			
	@locked			bit,
	@reason			nvarchar(50)		= null,	
	@departmentid	uniqueidentifier,
	@xroles			xml,
	@xscope			xml,	
	@usercreate		uniqueidentifier,
	@newid			uniqueidentifier	OUTPUT	
AS
BEGIN	
	SET @newid = NEWID();		

	INSERT INTO is_users (id, login, password, locked, reason, lastname, firstname, middlename, departmentid, scope, usercreate)	
	VALUES(@newid, @login, @password, @locked, @reason, @lastname, @firstname, @middlename, @departmentid, @xscope, @usercreate)		

	IF (@@ERROR = 0)					
		EXEC [dbo].Role_CreateByXml @newid, @xroles						-- список новых ролей
END
