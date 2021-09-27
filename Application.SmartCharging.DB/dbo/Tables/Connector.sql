CREATE TABLE [dbo].[Connector] (
    [ID]         INT              NOT NULL,
    [CStationId] UNIQUEIDENTIFIER NOT NULL,
    [MaxCurrent] INT              NULL,
    CONSTRAINT [PK_Connector] PRIMARY KEY CLUSTERED ([ID] ASC, [CStationId] ASC),
    CONSTRAINT [FK_CStation_Id] FOREIGN KEY ([CStationId]) REFERENCES [dbo].[CStation] ([StationId])
);

