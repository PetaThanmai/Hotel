using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/Guest")]
public class GuestController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IGuestRepository _guest;
    private readonly IScheduleRepository _schedule;
    private readonly IRoomRepository _room;

    public GuestController(ILogger<GuestController> logger, IGuestRepository guest, IScheduleRepository schedule,
    IRoomRepository _room)
    {
        _logger = logger;
        _guest = guest;
        _schedule = schedule;
        this._room = _room;
    }
    [HttpGet]
    public async Task<ActionResult<List<GuestDTO>>> GetList()
    {
    var res =await _guest.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{guest_id}")]

    public async Task<ActionResult> GetById([FromRoute] long guest_id)
    {
        var res = await _guest.GetById(guest_id);
        if (res == null)
            return NotFound("No Product found with given employee number");
            var dto = res.asDto;
        dto.Schedules = (await _schedule.GetListByGuestId(guest_id))
                        .Select(x => x.asDto).ToList();
        dto.Rooms = (await _room.GetListByGuestId(guest_id)).Select(x => x.asDto).ToList();

        return Ok(dto);
    }

    [HttpPost]

    public async Task<ActionResult<GuestDTO>> CreateGuest([FromBody] CreateGuestDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateGuest= new Guest
        {

           GuestId=Data.GuestId,
           GuestName=Data.GuestName,
           Mobile=Data.Mobile,
           Email=Data.Email,
            DateOfBirth=Data.DateOfBirth,
            Gender=Data.Gender,

           

        };
        var createdGuest = await _guest.Create(toCreateGuest);

        return StatusCode(StatusCodes.Status201Created, createdGuest.asDto);


    }

    [HttpPut("{Guest_id}")]
    public async Task<ActionResult> UpdateGuest([FromRoute] long Guest_id,
    [FromBody] GuestUpdateDTO Data)
    {
        var existing = await _guest.GetById(Guest_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateGuest = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _guest.Update(toUpdateGuest);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{Guest_id}")]
    public async Task<ActionResult> DeleteGuest([FromRoute] long GuestId)
    {
        var existing = await _guest.GetById(GuestId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _guest.Delete(GuestId);
        return NoContent();
    }
}