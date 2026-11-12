public record RfidWorkerEvent(
    Guid EventId,
    string Epc,
    DateTime ReaderDate,
    Guid LectorRfidId
);