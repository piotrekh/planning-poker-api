CREATE TABLE [dbo].[Game]
(
    [Id] int IDENTITY(1, 1) NOT NULL,
    [SessionId] int NOT NULL,
    [TaskName] nvarchar(200) NULL,
    [ExternalTaskUrl] nvarchar(max) NULL,
    [FinalEstimate] int NULL,
    [DateCreated] datetime NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED([Id]),
    CONSTRAINT [FK_Game_Session_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session]([Id])
)

CREATE INDEX [IX_Game_SessionId] ON [dbo].[Game]([SessionId])
-- index unfinished games
CREATE INDEX [IX_Game_FinalEstimate] ON [dbo].[Game]([FinalEstimate]) WHERE [FinalEstimate] IS NULL
