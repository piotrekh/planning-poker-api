INSERT INTO [dbo].[AspNetUser]
(
	[AccessFailedCount],
	[ConcurrencyStamp],
	[Email],
	[EmailConfirmed],
	[LockoutEnabled],
	[NormalizedEmail],
	[NormalizedUserName],
	[PasswordHash],
	[PhoneNumberConfirmed],
	[SecurityStamp],
	[TwoFactorEnabled],
	[UserName],
	[FirstName],
	[LastName]
)
values
(
	0,
	'e1707534-65e5-4779-8869-cab1020131a4',
	'admin1@test.com',
	0,
	1,
	'ADMIN1@TEST.COM',
	'ADMIN1@TEST.COM',
	'AQAAAAEAACcQAAAAEIIbHX1QituHd1Z7tvdzhY8SkuSsgTWWgmi7iKXb7CG/1QjaLph9j5HfkbrF0hOL4Q==', -- password: !Qaz123
	0,
	'0ea82b19-d352-4edc-844b-1ed70674bd80',
	0,
	'admin1@test.com',
	'Dora',
	'Lund'
),
(
	0,
	'0ffb2256-7b7e-487b-8c94-233ad16268fa',
	'moderator1@test.com',
	0,
	1,
	'MODERATOR1@TEST.COM',
	'MODERATOR1@TEST.COM',
	'AQAAAAEAACcQAAAAEHsIq2hFovYnTEsecngHdzHgoF9BgczHnsxa6oFOVb5hg4yKY2YGdvzbIemu29LYIg==', -- password: !Qaz123
	0,
	'22219a29-324f-48f2-8340-13da3c2fb9cf',
	0,
	'moderator1@test.com',
	'Edwin',
	'Kimpel'
),
(
	0,
	'195ca310-ec7b-45fa-b614-96a011839161',
	'player1@test.com',
	0,
	1,
	'PLAYER1@TEST.COM',
	'PLAYER1@TEST.COM',
	'AQAAAAEAACcQAAAAEAE5wayO4pEImdaDfffRmufUlgclQu8A4Z1r5bEzrrqfARarXHzhH0MYEib4arrHBg==', -- password: !Qaz123
	0,
	'10dfb9a1-e810-4eb4-9bf2-51901eaf03ac',
	0,
	'player1@test.com',
	'Adam',
	'Erbe'
)

INSERT INTO [dbo].[AspNetUserRole]
([AspNetUserId], [AspNetRoleId])
values
((SELECT TOP 1 [Id] FROM [dbo].[AspNetUser] WHERE [Email] = 'admin1@test.com'), 1),
((SELECT TOP 1 [Id] FROM [dbo].[AspNetUser] WHERE [Email] = 'moderator1@test.com'), 2),
((SELECT TOP 1 [Id] FROM [dbo].[AspNetUser] WHERE [Email] = 'player1@test.com'), 3)