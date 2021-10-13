CREATE TABLE [dbo].[Timers] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [UserId]                 NVARCHAR (450) NULL,
    [Name]                   NVARCHAR (100) NOT NULL,
    [Description]            NVARCHAR (255) NULL,
    [StartAt]                DATETIME2 (7)  NOT NULL,
    [EndAt]                  DATETIME2 (7)  NOT NULL,
    [TotalDurationTimeSpan]  TIME (7)       NOT NULL,
    [TotalPausedTimeSpan]    TIME (7)       NOT NULL,
    [TotalCountdownTimeSpan] TIME (7)       NOT NULL,
    [TotalDuration]          NVARCHAR (30)  NOT NULL,
    [TotalPausedTime]        NVARCHAR (30)  NOT NULL,
    [TotalCountdownTime]     NVARCHAR (30)  NOT NULL,
    [Paused]                 BIT            NOT NULL,
    [Finished]               BIT            NOT NULL,
    CONSTRAINT [PK_Timers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Timers_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Timers_UserId]
    ON [dbo].[Timers]([UserId] ASC);

