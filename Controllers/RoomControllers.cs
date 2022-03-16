using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/Room")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;

    private readonly IRoomRepository _Room;
    private readonly IStaffRepository _staff;
    private readonly IScheduleRepository _schedule;

    public RoomController(ILogger<RoomController> logger, IRoomRepository Room,IStaffRepository Staff,IScheduleRepository Schedule)
    {
        _logger = logger;
        _Room = Room;
        _staff=Staff;
        _schedule=Schedule;
    }
    [HttpGet]
    public async Task<ActionResult<List<RoomDTO>>> GetList()
    {
    var res =await _Room.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{room_id}")]

    public async Task<ActionResult> GetById([FromRoute] long room_id)
    {
        var res = await _Room.GetById(room_id);
        if (res is null)
            return NotFound("No Product found with given employee number");
            var dto = res.asDto;
            dto.Staff =(await _staff.GetStaffByRoomId(room_id)).Select(x =>x.asDto).ToList();
            dto.Schedule =(await _schedule.GetScheduleByRoomId(room_id)).Select(x =>x.asDto).ToList();
            
        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<RoomDTO>> CreateRoom([FromBody] CreateRoomDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateRoom= new Room
        {

           RoomId=Data.RoomId,
           RoomSize=Data.RoomSize,
           Price=Data.Price,
           
           RoomType=Data.RoomType,
        };
        var createdRoom = await _Room.Create(toCreateRoom);

        return StatusCode(StatusCodes.Status201Created, createdRoom.asDto);


    }

    [HttpPut("{room_id}")]
    public async Task<ActionResult> UpdateRoom([FromRoute] long room_id,
    [FromBody] RoomUpdateDTO Data)
    {
        var existing = await _Room.GetById(room_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateRoom = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Room.Update(toUpdateRoom);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{Room_id}")]
    public async Task<ActionResult> DeleteRoom([FromRoute] long RoomId)
    {
        var existing = await _Room.GetById(RoomId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Room.Delete(RoomId);
        return NoContent();
    }
}