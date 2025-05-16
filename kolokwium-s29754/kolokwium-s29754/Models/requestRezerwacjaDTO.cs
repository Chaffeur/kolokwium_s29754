namespace kolokwium_s29754.Models;

public class requestRezerwacjaDTO
{
    public DateTime date { get; set; }
    public GuestDTO guest { get; set; }
    public EmployeeDTO employee { get; set; }
    public List<AttractionsDTO>? attractions { get; set; }
}

public class GuestDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime dateOfBirth { get; set; }
}

public class EmployeeDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string employeeNumber { get; set; }
}

public class AttractionsDTO
{
    public string? name { get; set; }
    public decimal price { get; set; }
    public int amount { get; set; }
}