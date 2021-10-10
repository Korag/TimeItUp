CREATE TABLE [dbo].[Timers] (
    [Id]              INT            NOT NULL,
    [UserId]          NVARCHAR (450) NOT NULL,
    [Name]            NVARCHAR (100) NOT NULL,
    [Description]     NVARCHAR (255) NULL,
    [StartAt]         DATETIME2 (7)  NOT NULL,
    [EndAt]           DATETIME2 (7)  NOT NULL,
    [TotalDuration]   NVARCHAR (30)  NOT NULL,
    [TotalPausedTime] NVARCHAR (MAX) NOT NULL,
    [Paused]          BIT            NOT NULL,
    [Finished]        BIT            NOT NULL,
    [SplitsNumber]    INT            NOT NULL,
    [AlarmsNumber]    INT            NOT NULL,
    CONSTRAINT [PK_Timers] PRIMARY KEY CLUSTERED ([Id] ASC, [UserId] ASC),
    CONSTRAINT [FK_Timers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Timers_UserId]
    ON [dbo].[Timers]([UserId] ASC);

