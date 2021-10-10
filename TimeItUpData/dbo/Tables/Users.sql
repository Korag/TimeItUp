CREATE TABLE [dbo].[Users] (
    [Id]           NVARCHAR (450) NOT NULL,
    [EmailAddress] NVARCHAR (40)  NOT NULL,
    [FirstName]    NVARCHAR (40)  NOT NULL,
    [LastName]     NVARCHAR (40)  NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

