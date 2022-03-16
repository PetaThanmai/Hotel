using System.Text.Json.Serialization;

namespace Hotel.DTOs;
public record ScheduleDTO
{
    [JsonPropertyName("schedule_id")]
    public long ScheduleId { get; set; }

    [JsonPropertyName("check-in")]
    public DateTimeOffset CheckIn { get; set; }
    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }
    [JsonPropertyName("guest_count")]
    public double GuestCount { get; set; }
    
    [JsonPropertyName("created_at")]
     public DateTimeOffset  CreatedAt { get; set; }
     [JsonPropertyName("guest_id")]
    public int GuestId { get; set; }
    [JsonPropertyName("room_id")]
    public int RoomId { get; set; }
    [JsonPropertyName("guest")]
    

    // public List<RoomDTO> Room { get; set; }

    public List<GuestDTO> Guest { get; set; }

    [JsonPropertyName("room")]
    

    // public List<RoomDTO> Room { get; set; }

    public List<RoomDTO> Room { get; set; }
}

public record CreateScheduleDTO

{
    [JsonPropertyName("schedule_id")]
    public long ScheduleId { get; set; }

    [JsonPropertyName("check-in")]
    public DateTimeOffset CheckIn { get; set; }
    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }
    [JsonPropertyName("guest_count")]
    public double GuestCount { get; set; }
    
    [JsonPropertyName("created_at")]
     public DateTimeOffset  CreatedAt { get; set; }
     [JsonPropertyName("guest_id")]
    public int GuestId { get; set; }
    [JsonPropertyName("room_id")]
    public int RoomId { get; set; }
}




public record ScheduleUpdateDTO
{
    [JsonPropertyName("check-in")]
    public DateTimeOffset CheckIn { get; set; }
    [JsonPropertyName("check_out")]
    public DateTimeOffset CheckOut { get; set; }

}