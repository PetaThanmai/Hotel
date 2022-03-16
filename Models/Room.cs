using Hotel.DTOs;

namespace Hotel.Models;
public enum RoomType{
    Regular=1,
    Double=2,
    Master=3,
    Suite=4,
}


public record Room
{
    public long RoomId { get; set; }
    public int RoomType { get; set; }
    public  int RoomSize{ get; set; }
    public double Price { get; set; }
    public long StaffId { get; set; }
    public RoomDTO asDto =>new RoomDTO
    {
        RoomId=RoomId,
        RoomType=RoomType,
        RoomSize=RoomSize,
        Price=Price,
        
    };

}