public class MiServicio
{
    private Guid Id {get; set;} = Guid.NewGuid();

    public Guid GetId()
    {
        return Id;
    }
}