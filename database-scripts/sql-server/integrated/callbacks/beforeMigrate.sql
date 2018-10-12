DECLARE @UserName NVARCHAR(100);  
DECLARE @Password NVARCHAR(100);  
DECLARE @Schema NVARCHAR(100);  
DECLARE @Base NVARCHAR(100);  
SET @UserName = N'${ChristmasMothers_database_user}'
SET @Password = N'${ChristmasMothers_database_password}'
SET @Schema = '${ChristmasMothers_database_schema}'
SET @Base = '${ChristmasMothers_database_service_name}'

DECLARE @Query NVARCHAR(1000); 
DECLARE @ParmDefinition NVARCHAR(500);  
DECLARE @Exists INT;  

DECLARE @Edition NVARCHAR(128);
SELECT @Edition = CONVERT(NVARCHAR(128), serverproperty('Edition'));

IF(@Edition = 'SQL Azure')
BEGIN

	-- Create User
	IF NOT EXISTS (SELECT [name] FROM sys.sysusers WHERE [name] = @UserName)
	BEGIN
		SET @Query = N'CREATE USER [' + @UserName + N'] WITH PASSWORD = ''' + @Password + N'''';
		EXECUTE sp_executesql @Query;
	END

END
ELSE
BEGIN
	-- Create Login in SQL instance
	SET @Query = N'SELECT @LoginOUT = COUNT(*) FROM [master].sys.syslogins WHERE [name] = @User';  
	SET @ParmDefinition = N'@User NVARCHAR(100), @LoginOUT INT OUTPUT';  
	EXECUTE sp_executesql @Query, @ParmDefinition, @User = @UserName, @LoginOUT = @Exists OUTPUT;  
	IF (@Exists = 0)
	BEGIN
		SET @Query = N'CREATE LOGIN [' + @UserName + N'] WITH PASSWORD=N''' + @Password + N''', DEFAULT_DATABASE=[' + @Base + N'], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF';
		EXECUTE sp_executesql @Query;
	END
	
	-- Create user in database
	IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE [name] = @UserName)
	BEGIN
		SET @Query = N'CREATE USER [' + @UserName + N'] FOR LOGIN [' + @UserName + N'] WITH DEFAULT_SCHEMA= [' + @Schema + N']';
		EXECUTE sp_executesql @Query;
	END	
END

-- db_owner
SET @Query = N'EXEC sp_addrolemember ''db_owner'', ''' + @UserName + N''';';
EXECUTE sp_executesql @Query;

-- Creer le schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = @Schema)
BEGIN
	SET @Query = N'CREATE SCHEMA [' + @Schema + '] AUTHORIZATION [dbo];';
	EXECUTE sp_executesql @Query;
END