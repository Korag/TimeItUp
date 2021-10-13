CREATE TABLE [dbo].[Pauses] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [TimerId]               INT           NOT NULL,
    [StartAt]               DATETIME2 (7) NOT NULL,
    [EndAt]                 DATETIME2 (7) NOT NULL,
    [TotalDuration]         NVARCHAR (30) NOT NULL,
    [TotalDurationTimeSpan] TIME (7)      NOT NULL,
    CONSTRAINT [PK_Pauses] PRIMARY KEY CLUSTERED ([Id] ASC, [TimerId] ASC),
    CONSTRAINT [FK_Pauses_Timers_TimerId] FOREIGN KEY ([TimerId]) REFERENCES [dbo].[Timers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Pauses_TimerId]
    ON [dbo].[Pauses]([TimerId] ASC);

