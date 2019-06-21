CREATE TABLE [dbo].[ListModels] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Content] NVARCHAR (MAX) NOT NULL,
    [Status]  BIT            NOT NULL,
    CONSTRAINT [PK_dbo.ListModels] PRIMARY KEY CLUSTERED ([Id] ASC)
);
