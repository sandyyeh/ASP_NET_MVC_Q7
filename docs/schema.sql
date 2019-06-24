CREATE TABLE [dbo].[TodoModels] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    [Status]  BIT            NOT NULL,
    [URL]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.TodoModels] PRIMARY KEY CLUSTERED ([Id] ASC)
);