using System.Text.Json.Serialization;
using Hotel.Models;

namespace Hotel.DTOs;
public record GuestDTO
{
    [JsonPropertyName("guest_id")]
    public  long GuestId { get; set; }

    [JsonPropertyName("guest_name")]
    public string GuestName { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile{ get; set; }
     [JsonPropertyName("email")]
      public string Email { get; set; }
        [JsonPropertyName("date_of_birth")]
        public DateTimeOffset DateOfBirth { get; set; }
        [JsonPropertyName("gender")]
         public Gender Gender { get; set; }
         [JsonPropertyName("schedules")]
    public List<ScheduleDTO> Schedules { get; set; }

    [JsonPropertyName("rooms")]
    public List<RoomDTO> Rooms { get; set; }
}
    
    // public List<GuestDTO> Guest { get; set; }


public record CreateGuestDTO

{
 [JsonPropertyName("guest_id")]
    public long GuestId { get; set; }

    [JsonPropertyName("guest-name")]
    public string GuestName { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile{ get; set; }
     [JsonPropertyName("email")]
      public string Email { get; set; }
        [JsonPropertyName("date_of_birth")]
        public DateTimeOffset DateOfBirth { get; set; }
        [JsonPropertyName("gender")]
     
         public Gender Gender { get; set; }
  [JsonPropertyName("schedule")]
         public List<ScheduleDTO> Schedule { get; set; }



}  


   


public record GuestUpdateDTO
{
        [JsonPropertyName("guest_name")]
    public string GuestName { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile{ get; set; }
     [JsonPropertyName("email")]
      public string Email { get; set; }
        
}