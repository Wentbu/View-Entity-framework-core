public class Device
{
    public string DeviceCode { get; set; }
    public string DeviceName { get; set; }
    public string IpAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool Connected { get; set; }
    public bool OperationStatus { get; set; }

    public ICollection<Assignment> Assignments { get; set; }
}