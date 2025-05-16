namespace kolokwium_s29754.Models;

public class addBookingDTO
{
    public int bookingId { get; set; }
    public int guestId { get; set; }
    public string employeeNumber { get; set; }
    public List<AttractionsNewDTO> attractions { get; set; }
}

public class AttractionsNewDTO
{
    public string name { get; set; }
    public int amount { get; set; }
}