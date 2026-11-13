using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RfidTraceability.Api.Extensions;
using RfidTraceability.Application.Readings;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RfidReadingEventsController(
    ISender sender, 
    ILogger<RfidReadingEventsController> logger,
    ProductionAdvanceService productionAdvanceService
    ) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> Register([FromBody] List<RfidWorkerEvent> events, CancellationToken cancellationToken)
    {
        logger.LogInformation("Eventos recibidos desde el worker:");
        foreach (var eventW in events)
        {
            logger.LogInformation($"EventId: {eventW.EventId}, Epc: {eventW.Epc}, ReaderDate: {eventW.ReaderDate}");
        }

        var command = new RegisterRfidReadingEventsCommand(
            events.Select(eventW => new RfidReadingEventInput(
                eventW.EventId,
                eventW.Epc,
                eventW.ReaderDate,
                eventW.LectorRfidId)).ToList(),
            User.GetOrganizationId());

        var processed = await sender.Send(command, cancellationToken);
        logger.LogInformation("Eventos procesados: {Processed}", processed);

        await productionAdvanceService.AdvanceNotification(new RfidWorkerEvent(Guid.NewGuid(), "EPC-01", DateTime.UtcNow, Guid.NewGuid()));
        return Ok(true);
    }
}
