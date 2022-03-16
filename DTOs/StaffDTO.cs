using System.Text.Json.Serialization;

namespace Hotel.DTOs;
public record StaffDTO
{
    [JsonPropertyName("staff_id")]
    public long StaffId { get; set; }

    [JsonPropertyName("staff_name")]
    public string StaffName { get; set; }
    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth{ get; set; }
    [JsonPropertyName("gender")]
    public int Gender { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("shift")]
    public int Shift { get; set; }
    // [JsonPropertyName("gender")]
    //  public Gender Gender { get; set; }

    // public List<RoomDTO> Room { get; set; }
        [JsonPropertyName("room")]
    public List<RoomDTO> Room { get; set; }

}

public record CreateStaffDTO

{    
    [JsonPropertyName("staff_id")]
    public long StaffId { get; set; }

    [JsonPropertyName("staff_name")]
    public string StaffName { get; set; }
    [JsonPropertyName("date-of_birth")]
    public DateTimeOffset DateOfBirth{ get; set; }
    [JsonPropertyName("gender")]
    public int Gender { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("shift")]
    public int Shift { get; set; }
}




public record StaffUpdateDTO
{
   
 [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("shift")]
    public int Shift { get; set; }

}