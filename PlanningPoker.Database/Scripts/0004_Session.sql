CREATE TABLE [dbo].[Session]
(
    [Id] int IDENTITY(1, 1) NOT NULL,
    [Title] nvarchar(200) NOT NULL,
    [ModeratorId] int NOT NULL,
    [EstimationUnit] nvarchar(20) NOT NULL,
	[IsFinished] bit NOT NULL,
    [DateCreated] datetime NOT NULL,	
    CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED([Id]),
    CONSTRAINT [FK_Session_AspNetUser_ModeratorId] FOREIGN KEY([ModeratorId]) REFERENCES [dbo].[AspNetUser]([Id])
)

CREATE INDEX [IX_Session_ModeratorId] ON [dbo].[Session]([ModeratorId])
-- index unfinished sessions
CREATE INDEX [IX_Session_IsFinished] ON [dbo].[Session]([IsFinished]) WHERE [IsFinished] = 0