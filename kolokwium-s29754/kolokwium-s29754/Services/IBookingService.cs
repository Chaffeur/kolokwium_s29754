using kolokwium_s29754.Models;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium_s29754.Services;

public interface IBookingService
{
    public Task<requestRezerwacjaDTO> getBooking(int userId);
    public Task<int> addBooking(addBookingDTO booking);
}