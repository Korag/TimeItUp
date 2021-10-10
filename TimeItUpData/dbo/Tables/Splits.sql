CREATE TABLE [dbo].[Splits] (
    [Id]            INT            NOT NULL,
    [TimerId]       INT            NOT NULL,
    [TimerId1]      INT            NOT NULL,
    [TimerUserId]   NVARCHAR (450) NOT NULL,
    [StartAt]       DATETIME2 (7)  NOT NULL,
    [EndAt]         DATETIME2 (7)  NOT NULL,
    [TotalDuration] NVARCHAR (30)  NOT NULL,
    CONSTRAINT [PK_Splits] PRIMARY KEY CLUSTERED ([Id] ASC, [TimerId] ASC),
    CONSTRAINT [FK_Splits_Timers_TimerId1_TimerUserId] FOREIGN KEY ([TimerId1], [TimerUserId]) REFERENCES [dbo].[Timers] ([Id], [UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Splits_TimerId1_TimerUserId]
    ON [dbo].[Splits]([TimerId1] ASC, [TimerUserId] ASC);

