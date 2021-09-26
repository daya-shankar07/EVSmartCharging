CREATE TABLE [dbo].[CStation] (
    [StationId] UNIQUEIDENTIFIER NOT NULL,
    [GroupId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]      NVARCHAR (50)    NULL,
    CONSTRAINT [PK_CStation] PRIMARY KEY CLUSTERED ([StationId] ASC),
    CONSTRAINT [FK_Group_Id] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[Group] ([ID])
);

