CREATE TABLE [dbo].[Group] (
    [ID]       UNIQUEIDENTIFIER NOT NULL,
    [Name]     NVARCHAR (50)    NULL,
    [Capacity] INT              NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED ([ID] ASC)
);

