public class Assignment
{
    public string Code { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string Telephone { get; set; }
    public DateTime AssignmentDate { get; set; }
    public DateTime ExpireDate { get; set; }
    public short Status { get; set; }

    public string ServiceCode { get; set; }
    public Service Service { get; set; }

    public string DeviceCode { get; set; }
    public Device Device { get; set; }
}