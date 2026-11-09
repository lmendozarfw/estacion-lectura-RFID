namespace RfidTraceability.Domain.Entities;

public class Organization
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    public ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
    public ICollection<Sku> Skus { get; set; } = new List<Sku>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public Organization(string code, string name)
    {
        Code = DomainGuard.NotNullOrWhiteSpace(code, nameof(code));
        Name = DomainGuard.NotNullOrWhiteSpace(name, nameof(name));
    }
}
