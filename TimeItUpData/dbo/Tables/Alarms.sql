CREATE TABLE [dbo].[Alarms] (
    [Id]          INT            NOT NULL,
    [TimerId]     INT            NOT NULL,
    [TimerId1]    INT            NOT NULL,
    [TimerUserId] NVARCHAR (450) NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [Time]        DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Alarms] PRIMARY KEY CLUSTERED ([Id] ASC, [TimerId] ASC),
    CONSTRAINT [FK_Alarms_Timers_TimerId1_TimerUserId] FOREIGN KEY ([TimerId1], [TimerUserId]) REFERENCES [dbo].[Timers] ([Id], [UserId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Alarms_TimerId1_TimerUserId]
    ON [dbo].[Alarms]([TimerId1] ASC, [TimerUserId] ASC);

