using System.Text.Json.Serialization;

namespace Hotel.DTOs;
public record RoomDTO
{
    [JsonPropertyName("room_id")]
    public long RoomId { get; set; }

    [JsonPropertyName("room_type")]
    public int RoomType { get; set; }
    [JsonPropertyName("room_size")]
    public int RoomSize { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; }


    [JsonPropertyName("staff")]
    public List<StaffDTO> Staff { get; set; }
    [JsonPropertyName("schedule")]
    public List<ScheduleDTO> Schedule { get; set; }


}

public record CreateRoomDTO

{
    [JsonPropertyName("room_id")]
    public long RoomId { get; set; }

    [JsonPropertyName("room_type")]
    public int RoomType { get; set; }
    [JsonPropertyName("room_size")]
    public int RoomSize { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; }
    [JsonPropertyName("staff_id")]
    public long StaffId { get; set; }
}




public record RoomUpdateDTO
{
    [JsonPropertyName("room_type")]
    public int RoomType { get; set; }
    [JsonPropertyName("room_size")]
    public int RoomSize { get; set; }
    [JsonPropertyName("price")]
    public double Price { get; set; }


}