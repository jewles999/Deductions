EXEC master..sp_addsrvrolemember @loginame = N'admin', @rolename = N'dbcreator'

CREATE LOGIN [admin] WITH PASSWORD = 'topsecret';
CREATE USER [admin] FOR LOGIN [admin];
exec sp_addrolemember 'db_owner', 'admin'