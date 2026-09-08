/*
   Test data for ConferenceRoomBooking.
   Prerequisite: all EF Core migrations have been applied.
   The script is idempotent: it can be run more than once without duplicating rows.
*/

SET XACT_ABORT ON;
BEGIN TRANSACTION;

/* Make the three standard services available for every seeded conference room. */
INSERT INTO [RoomService] ([ConferenceRoomId], [AdditionalServiceId])
SELECT [data].[ConferenceRoomId], [data].[AdditionalServiceId]
FROM (VALUES
    (1, 1), (1, 2), (1, 3),
    (2, 1), (2, 2), (2, 3),
    (3, 1), (3, 2), (3, 3)
) AS [data] ([ConferenceRoomId], [AdditionalServiceId])
WHERE NOT EXISTS (
    SELECT 1
    FROM [RoomService] AS [roomService]
    WHERE [roomService].[ConferenceRoomId] = [data].[ConferenceRoomId]
      AND [roomService].[AdditionalServiceId] = [data].[AdditionalServiceId]
);

/* Statuses: 1 = Pending, 2 = Confirmed, 3 = Cancelled. */
INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 1, N'Test Client 1', '2026-09-10', '10:00', '12:00', 2, SYSUTCDATETIME(), 4000.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 1 AND [Date] = '2026-09-10' AND [StartTime] = '10:00' AND [EndTime] = '12:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 1, N'Test Client 2', '2026-09-11', '12:00', '14:00', 2, SYSUTCDATETIME(), 4600.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 1 AND [Date] = '2026-09-11' AND [StartTime] = '12:00' AND [EndTime] = '14:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 1, N'Test Client 3', '2026-09-12', '18:00', '20:00', 2, SYSUTCDATETIME(), 3200.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 1 AND [Date] = '2026-09-12' AND [StartTime] = '18:00' AND [EndTime] = '20:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 1, N'Test Client 4', '2026-09-13', '08:00', '10:00', 1, SYSUTCDATETIME(), 3800.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 1 AND [Date] = '2026-09-13' AND [StartTime] = '08:00' AND [EndTime] = '10:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 1, N'Test Client 5', '2026-09-14', '09:00', '11:00', 3, SYSUTCDATETIME(), 4000.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 1 AND [Date] = '2026-09-14' AND [StartTime] = '09:00' AND [EndTime] = '11:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 2, N'Test Client 6', '2026-09-10', '09:00', '13:00', 2, SYSUTCDATETIME(), 14525.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 2 AND [Date] = '2026-09-10' AND [StartTime] = '09:00' AND [EndTime] = '13:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 2, N'Test Client 7', '2026-09-11', '14:00', '17:00', 2, SYSUTCDATETIME(), 10500.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 2 AND [Date] = '2026-09-11' AND [StartTime] = '14:00' AND [EndTime] = '17:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 2, N'Test Client 8', '2026-09-12', '18:00', '22:00', 2, SYSUTCDATETIME(), 11200.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 2 AND [Date] = '2026-09-12' AND [StartTime] = '18:00' AND [EndTime] = '22:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 2, N'Test Client 9', '2026-09-15', '10:00', '12:00', 1, SYSUTCDATETIME(), 7000.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 2 AND [Date] = '2026-09-15' AND [StartTime] = '10:00' AND [EndTime] = '12:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 3, N'Test Client 10', '2026-09-10', '06:00', '09:00', 2, SYSUTCDATETIME(), 4050.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 3 AND [Date] = '2026-09-10' AND [StartTime] = '06:00' AND [EndTime] = '09:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 3, N'Test Client 11', '2026-09-11', '12:00', '14:00', 2, SYSUTCDATETIME(), 3450.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 3 AND [Date] = '2026-09-11' AND [StartTime] = '12:00' AND [EndTime] = '14:00');

INSERT INTO [Bookings] ([ConferenceRoomId], [UserName], [Date], [StartTime], [EndTime], [Status], [CreatedAt], [TotalPrice])
SELECT 3, N'Test Client 12', '2026-09-13', '15:00', '18:00', 2, SYSUTCDATETIME(), 4500.00
WHERE NOT EXISTS (SELECT 1 FROM [Bookings] WHERE [ConferenceRoomId] = 3 AND [Date] = '2026-09-13' AND [StartTime] = '15:00' AND [EndTime] = '18:00');

COMMIT TRANSACTION;
