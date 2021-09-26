CREATE TABLE [dbo].[Connector] (
    [ID]         INT              NOT NULL,
    [CStationId] UNIQUEIDENTIFIER NOT NULL,
    [MaxCurrent] INT              NULL,
    CONSTRAINT [FK_CStation_Id] FOREIGN KEY ([CStationId]) REFERENCES [dbo].[CStation] ([StationId])
);

