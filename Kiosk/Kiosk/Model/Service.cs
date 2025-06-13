public class Service
{
    public string ServiceCode { get; set; }
    public string ServiceName { get; set; }
    public string Description { get; set; }
    public bool IsInOperation { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}
