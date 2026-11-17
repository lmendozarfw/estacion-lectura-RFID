using Microsoft.AspNetCore.SignalR;
using RfidTraceability.Application.Readings;

public class ProductionAdvanceService
{
    private readonly IHubContext<ProductionHub> _hubContext;

    public ProductionAdvanceService(IHubContext<ProductionHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AdvanceNotification(ProductionAdvanceResponse advance)
    {
        await _hubContext.Clients.All.SendAsync("advanceUpdate", advance);
    }
}