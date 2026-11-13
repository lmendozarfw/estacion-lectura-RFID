using Microsoft.AspNetCore.SignalR;

public class ProductionAdvanceService
{
    private readonly IHubContext<ProductionHub> _hubContext;

    public ProductionAdvanceService(IHubContext<ProductionHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AdvanceNotification(RfidWorkerEvent advance)
    {
        await _hubContext.Clients.All.SendAsync("advanceUpdate", advance);
    }
}