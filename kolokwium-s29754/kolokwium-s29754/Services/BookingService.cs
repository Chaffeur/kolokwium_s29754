using System.Data.SqlTypes;
using kolokwium_s29754.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kolokwium_s29754.Services;

public class BookingService : IBookingService
{
    
    
    private readonly IConfiguration _configuration;
    
    public BookingService (IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<requestRezerwacjaDTO> getBooking(int id)
    {
        var connectionString = _configuration.GetConnectionString("Default");

        var query = @"SELECT b.[date], g.first_name, g.last_name, e.first_name, e.last_name,
        e.employee_number, g.date_of_birth, a.[name], a.price, amount, a.attraction_id
        FROM Booking b 
        JOIN Booking_Attraction ba ON b.booking_id = ba.booking_id
        JOIN Employee e ON b.employee_id = e.employee_id
        JOIN Guest g ON b.guest_id = g.guest_id
        JOIN Attraction a ON ba.attraction_id = a.attraction_id
        WHERE b.booking_id = @bookingId";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingId", id);
            
            
            requestRezerwacjaDTO? rezerwacja = null;

            var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                if (rezerwacja == null)
                {
                    rezerwacja = new requestRezerwacjaDTO
                    {
                        date = reader.GetDateTime(0),
                        
                        guest = new GuestDTO
                        {
                            firstName = reader.GetString(1),
                            lastName = reader.GetString(2),
                            dateOfBirth = reader.GetDateTime(6)
                        },
                        employee = new EmployeeDTO
                        {
                            firstName = reader.GetString(3),
                            lastName = reader.GetString(4),
                            employeeNumber = reader.GetString(5),
                        }
                        
                        
                    };
                    var attId = reader.GetString(7);

                    AttractionsDTO? booking = 
                        rezerwacja.attractions.FirstOrDefault(e => e.name.Equals(attId));

                    if (booking is null)
                    {
                        booking = new AttractionsDTO()
                        {
                            name = reader.GetString(7),
                            price = reader.GetDecimal(8),
                            amount = reader.GetInt32(9)
                        };
                        rezerwacja.attractions.Add(booking);
                    }
                    rezerwacja.attractions.Add(new AttractionsDTO()
                    {
                        name = reader.GetString(7),
                        price = reader.GetDecimal(8),
                        amount = reader.GetInt32(9)
                    });

                    


                }
            }
            
            return rezerwacja;
            
            
            
        }
        
    }

    public async Task<int> addBooking(addBookingDTO booking)
    {
        var connectionString = _configuration.GetConnectionString("Default");

        var query = "@INSERT INTO Booking (booking_id, guest_id, employee_id, date) " +
                    "VALUES(@bookingId, @guestId, @employeeId, @date)";

        using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            
            var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@bookingId", booking.bookingId);
            command.Parameters.AddWithValue("@guestId", booking.guestId);
            command.Parameters.AddWithValue("@employeeNumber", booking.employeeNumber);
            
            
            var result = await command.ExecuteNonQueryAsync();
            

            return result;

        }
        
        
        
    }
}