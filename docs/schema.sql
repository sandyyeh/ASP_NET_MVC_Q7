CREATE TABLE [dbo].[TodoModel] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Content] NVARCHAR (MAX) NOT NULL,
    [Status]  BIT            NOT NULL,
    [URL]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_dbo.TodoModels] PRIMARY KEY CLUSTERED ([Id] ASC)
);
