CREATE TABLE [dbo].[Alarms] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [TimerId]        INT            NOT NULL,
    [Name]           NVARCHAR (100) NOT NULL,
    [Description]    NVARCHAR (255) NULL,
    [ActivationTime] DATETIME2 (7)  NOT NULL,
    CONSTRAINT [PK_Alarms] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Alarms_Timers_TimerId] FOREIGN KEY ([TimerId]) REFERENCES [dbo].[Timers] ([Id]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_Alarms_TimerId]
    ON [dbo].[Alarms]([TimerId] ASC);

