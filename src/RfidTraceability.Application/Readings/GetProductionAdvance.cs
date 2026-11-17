using MediatR;
using RfidTraceability.Application.Common.Exceptions;
using RfidTraceability.Domain.Entities;
using RfidTraceability.Domain.Repositories;

namespace RfidTraceability.Application.Readings;

public sealed record GetProductionAdvanceQuery(Guid LectorRfidId, DateOnly Date) : IRequest<ProductionAdvanceResponse>;

public sealed class GetProductionAdvanceHandler(
    IRfidReaderRepository readers,
    IProgramGoalRepository programGoals,
    IPieceControlPointAdvanceReadingRepository advanceReadings)
    : IRequestHandler<GetProductionAdvanceQuery, ProductionAdvanceResponse>
{
    private const int PairsPerLot = 10;
    private const int LeftSide = 1;
    private const int RightSide = 2;

    public async Task<ProductionAdvanceResponse> Handle(GetProductionAdvanceQuery request, CancellationToken cancellationToken)
    {
        var reader = await readers.GetByIdAsync(request.LectorRfidId, cancellationToken)
            ?? throw new EntityNotFoundException(nameof(RfidReader), request.LectorRfidId);

        var goals = await programGoals.ListByControlPointAndDateAsync(reader.ControlPointId, request.Date, cancellationToken);
        var readings = await advanceReadings.ListByControlPointAndDateWithPieceAsync(reader.ControlPointId, request.Date, cancellationToken);

        var lotCounts = readings
            .Where(r => GetSide(r.RfidPiece.SideCode) != 0)
            .GroupBy(r => new
            {
                ProgramId = r.RfidPiece.Lot.Block.ProgramId,
                LotId = r.RfidPiece.LotId,
                Side = GetSide(r.RfidPiece.SideCode)
            })
            .Select(g => new
            {
                g.Key.ProgramId,
                g.Key.LotId,
                g.Key.Side,
                Pieces = g.Select(r => r.RfidPieceId).Distinct().Count()
            })
            .ToList();

        var byProgram = new Dictionary<Guid, ProgramCounts>();

        foreach (var lot in lotCounts.GroupBy(x => new { x.ProgramId, x.LotId }))
        {
            var left = lot.Where(x => x.Side == LeftSide).Sum(x => x.Pieces);
            var right = lot.Where(x => x.Side == RightSide).Sum(x => x.Pieces);
            var counts = GetCounts(byProgram, lot.Key.ProgramId);

            counts.CompletePairs += Math.Min(left, right);
            counts.IncompletePairs += Math.Abs(left - right);
        }

        foreach (var reading in readings)
        {
            var counts = GetCounts(byProgram, reading.RfidPiece.Lot.Block.ProgramId);
            if (counts.LastUpdate is null || reading.Date > counts.LastUpdate)
            {
                counts.LastUpdate = reading.Date;
            }
        }

        var totalGoal = goals.Sum(g => g.TargetQuantity);
        var completePairs = byProgram.Values.Sum(c => c.CompletePairs);
        var incompletePairs = byProgram.Values.Sum(c => c.IncompletePairs);
        var compliance = totalGoal == 0 ? 0 : (int)Math.Round(completePairs * 100m / totalGoal);
        var remainingPairs = Math.Max(totalGoal - completePairs, 0);

        var metrics = new List<Metric>
        {
            new("Meta diaria total", totalGoal.ToString(), "Pares objetivo de los programas de este punto de control", "default", null),
            new("Pares completos", completePairs.ToString(), "Pares con izquierdo y derecho en el mismo lote", "default", null),
            new("Cumplimiento global", $"{compliance}%", $"Faltan {remainingPairs} pares para cumplir la meta", compliance >= 100 ? "success" : "default", compliance / 100m),
            new("Pares incompletos", incompletePairs.ToString(), "Pares a los que les falta un lado", incompletePairs > 0 ? "danger" : "default", null)
        };

        var programProgress = goals.Select(g =>
        {
            var counts = byProgram.GetValueOrDefault(g.ProgramId, new ProgramCounts());
            return new ProgramProgress(
                g.Program.Code,
                g.Program.PurchaseOrder.Code,
                counts.CompletePairs,
                g.TargetQuantity,
                counts.CompletePairs / PairsPerLot,
                counts.IncompletePairs > 0 ? "incomplete" : "normal",
                counts.LastUpdate ?? DateTime.UtcNow);
        }).ToList();

        var alerts = incompletePairs > 0
            ? new List<AttentionAlert>
            {
                new(incompletePairs, "Pares incompletos", "Hay piezas sin su lado correspondiente en el mismo lote", "warning")
            }
            : new List<AttentionAlert>();

        return new ProductionAdvanceResponse(metrics, programProgress, alerts);
    }

    private static ProgramCounts GetCounts(Dictionary<Guid, ProgramCounts> byProgram, Guid programId)
    {
        if (!byProgram.TryGetValue(programId, out var counts))
        {
            counts = new ProgramCounts();
            byProgram[programId] = counts;
        }
        return counts;
    }

    private static int GetSide(string sideCode) => sideCode.Length == 0 ? 0 : sideCode[^1] switch
    {
        '1' => LeftSide,
        '2' => RightSide,
        _ => 0
    };

    private sealed class ProgramCounts
    {
        public int CompletePairs { get; set; }
        public int IncompletePairs { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
