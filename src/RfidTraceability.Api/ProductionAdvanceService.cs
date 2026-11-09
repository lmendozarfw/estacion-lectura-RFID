using Microsoft.AspNetCore.SignalR;

public class ProductionAdvanceService
{
    private readonly IHubContext<ProductionHub> _hubContext;

    public ProductionAdvanceService(IHubContext<ProductionHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AdvanceNotification(object advance)
    {
        await _hubContext.Clients.All.SendAsync("advanceUpdate", advance);
    }
}