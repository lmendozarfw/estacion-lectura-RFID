using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RfidReadingEventsController(ISender sender, ILogger<RfidReadingEventsController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<bool>> Register([FromBody] List<RfidWorkerEvent> events, CancellationToken cancellationToken)
    {
        logger.LogInformation("Eventos recibidos desde el worker:");
        foreach (var eventW in events)
        {
            logger.LogInformation($"EventId: {eventW.EventId}, Epc: {eventW.Epc}, ReaderDate: {eventW.ReaderDate}");
        }

        // iterar los eventos
        // revisar si existe un "pieza_rfid" con el epc que viene del evento
        // si ya existe, no se crea
        // si no existe, se crea agregando la información del lote, bloque, programa, sku, lado
        // se obtiene informacion de qué lector mando el evento
        // se obtiene el punto de control de donde está el lector
        // se registra un "avances_pieza_punto_control" con la información obtenida antes
        // se calculan avances por programa y punto de control para mandarlo mediante SignalR al cliente
        return Ok(true);
    }
}