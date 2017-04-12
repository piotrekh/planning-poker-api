INSERT INTO [dbo].[AspNetRole]
([ConcurrencyStamp], [Name], [NormalizedName])
VALUES
(NEWID(), 'Admin', 'ADMIN'),
(NEWID(), 'Moderator', 'MODERATOR'),
(NEWID(), 'Player', 'PLAYER')

declare @adminId int = (SELECT TOP 1 [Id] FROM [dbo].[AspNetRole] WHERE [Name] = 'Admin');
declare @moderatorId int = (SELECT TOP 1 [Id] FROM [dbo].[AspNetRole] WHERE [Name] = 'Moderator');
declare @playerId int = (SELECT TOP 1 [Id] FROM [dbo].[AspNetRole] WHERE [Name] = 'Player');

INSERT INTO [dbo].[AspNetRoleClaim]
([ClaimType], [ClaimValue], [AspNetRoleId])
VALUES
('CanManageUsers', 'true', @adminId),
('CanManageSessions', 'true', @adminId),
('CanManageSessions', 'true', @moderatorId),
('CanPlayGame', 'true', @adminId),
('CanPlayGame', 'true', @moderatorId),
('CanPlayGame', 'true', @playerId)