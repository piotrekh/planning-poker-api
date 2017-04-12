CREATE TABLE [dbo].[SessionPlayer]
(
    [SessionId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_SessionPlayer] PRIMARY KEY CLUSTERED ([SessionId], [UserId]),
    CONSTRAINT [FK_SessionPlayer_Session_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [dbo].[Session]([Id]),
    CONSTRAINT [FK_SessionPlayer_AspNetUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUser]([Id])
)