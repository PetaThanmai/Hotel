using Hotel.DTOs;

namespace Hotel.Models;

public record Schedule
{
    public long ScheduleId { get; set; }
    public DateTimeOffset CheckIn { get; set; }
    public DateTimeOffset CheckOut { get; set; }
    public double GuestCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    public int GuestId { get; set; }
    
    public int RoomId { get; set; }

     public ScheduleDTO asDto =>new ScheduleDTO{
         ScheduleId=ScheduleId,
         CheckIn=CheckIn,
         CheckOut=CheckOut,
         GuestCount=GuestCount,
         CreatedAt=CreatedAt,
         GuestId=GuestId,
         RoomId=RoomId,
     };



}