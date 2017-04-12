CREATE TABLE [dbo].[GameEstimate]
(
    [GameId] int NOT NULL,
    [UserId] int NOT NULL,
    [Estimate] int NOT NULL,
    CONSTRAINT [PK_GameEstimate] PRIMARY KEY CLUSTERED ([GameId], [UserId]),
    CONSTRAINT [FK_GameEstimate_Game_GameId] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game]([Id]),
    CONSTRAINT [FK_GameEstimate_AspNetUser_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUser]([Id])
)