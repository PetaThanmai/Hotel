using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/Schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ILogger<ScheduleController> _logger;
    private readonly IScheduleRepository _Schedule;
    private readonly IGuestRepository _guest;
     private readonly IRoomRepository _room;


    public ScheduleController(ILogger<ScheduleController> logger, IScheduleRepository Schedule,IGuestRepository Guest,IRoomRepository room)
    {
        _logger = logger;
        _Schedule = Schedule;
        _guest=Guest;
        _room = room;
    }
    [HttpGet]
    public async Task<ActionResult<List<ScheduleDTO>>> GetList()
    {
        var res = await _Schedule.GetList();
        return Ok(res.Select(x => x.asDto));
    }



    [HttpGet("{schedule_id}")]

    public async Task<ActionResult<ScheduleDTO>> GetById([FromRoute] long schedule_id)
    {
        var res = await _Schedule.GetById(schedule_id);
        if (res is null)
            return NotFound("No Product found with given employee number");

            var dto = res.asDto;
        dto.Guest = (await _guest.GetGuestByScheduleId(res.GuestId)).Select(x => x.asDto).ToList(); 
        dto.Room = (await _room.GetRoomByScheduleId(res.RoomId)).Select(x => x.asDto).ToList(); 
        return Ok(dto);
        
    
    }

    [HttpPost]

    public async Task<ActionResult<ScheduleDTO>> CreateSchedule([FromBody] CreateScheduleDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateSchedule = new Schedule
        {

            ScheduleId = Data.ScheduleId,
            CheckIn = Data.CheckIn,
            CheckOut = Data.CheckOut,
            GuestCount = Data.GuestCount,
            CreatedAt = Data.CreatedAt,



        };
        var createdSchedule = await _Schedule.Create(toCreateSchedule);

        return StatusCode(StatusCodes.Status201Created, createdSchedule.asDto);


    }

    [HttpPut("{schedule_id}")]
    public async Task<ActionResult> UpdateSchedule([FromRoute] long schedule_id,
    [FromBody] ScheduleUpdateDTO Data)
    {
        var existing = await _Schedule.GetById(schedule_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateSchedule = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Schedule.Update(toUpdateSchedule);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{schedule_id}")]
    public async Task<ActionResult> DeleteSchedule([FromRoute] long schedule_id)
    {
        var existing = await _Schedule.GetById(schedule_id);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Schedule.Delete(schedule_id);
        return NoContent();
    }
}