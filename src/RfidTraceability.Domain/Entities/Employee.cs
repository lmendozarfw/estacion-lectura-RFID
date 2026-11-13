namespace RfidTraceability.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string PaternalSurname { get; set; } = string.Empty;
    public string MaternalSurname { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }

    public Employee(string employeeNumber, string firstName, string paternalSurname, string maternalSurname)
    {
        EmployeeNumber = DomainGuard.NotNullOrWhiteSpace(employeeNumber, nameof(employeeNumber));
        FirstName = DomainGuard.NotNullOrWhiteSpace(firstName, nameof(firstName));
        PaternalSurname = DomainGuard.NotNullOrWhiteSpace(paternalSurname, nameof(paternalSurname));
        MaternalSurname = DomainGuard.NotNullOrWhiteSpace(maternalSurname, nameof(maternalSurname));
    }
}
