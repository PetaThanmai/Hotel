using Hotel.DTOs;

namespace Hotel.Models;
public enum staffShift{
    Day=1,
    Night=2,
}

public record Staff
{
    public long StaffId { get; set; }
    public string StaffName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public int Gender { get; set; }
    public long Mobile { get; set; }
    public int Shift { get; set; }
     public StaffDTO asDto =>new StaffDTO{
   
         StaffId=StaffId,
           StaffName=StaffName,
           DateOfBirth=DateOfBirth,
           Gender=Gender,
           Mobile=Mobile,
           Shift=Shift,
     };


}