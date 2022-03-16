using Hotel.DTOs;

namespace Hotel.Models;

public enum Gender{
    Male=1,
    Female=2,
}
public record Guest
{
    public long GuestId { get; set; }
    public string GuestName { get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    
    public Gender Gender { get; set; }
    public GuestDTO asDto =>new GuestDTO{
        GuestId=GuestId,
        GuestName=GuestName,
        Mobile=Mobile,
        Email=Email,
        DateOfBirth=DateOfBirth,
        Gender=Gender,
        
    };

 

}