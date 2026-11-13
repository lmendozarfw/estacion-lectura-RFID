using System.Globalization;

public record Metric(
    string Title,
    string Value,
    string Description,
    string Variant, // 'default' | 'success' | 'danger'
    decimal? Progress
);

public record ProgramProgress(
    string ProgramCode,
    string PurchaseOrderCode,
    int CompletedPairs,
    int TargetPairs,
    int ClosedLots,
    int IncompleteLots,
    int UnmatchedPairs,
    string Status, // 'normal' | 'incomplete'
    DateTime UpdatedAt
);

public record AttentionAlert(
    int Quantity,
    string Title,
    string Description,
    string Severity // 'danger' | 'warning'
);