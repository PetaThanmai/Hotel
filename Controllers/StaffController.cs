using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/Staff")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _Staff;
    private readonly IRoomRepository _room;


    public StaffController(ILogger<StaffController> logger, IStaffRepository Staff,IRoomRepository room)
    {
        _logger = logger;
        _Staff = Staff;
        _room = room;
    }
    [HttpGet]
    public async Task<ActionResult<List<StaffDTO>>> GetList()
    {
    var res =await _Staff.GetList();
    return Ok(res.Select(x=>x.asDto));
    }

    

    [HttpGet("{Staff_id}")]

    public async Task<ActionResult<StaffDTO>> GetById([FromRoute] long Staff_id)
    {
        var res = await _Staff.GetById(Staff_id);
        if (res is null)
            return NotFound("No Product found with given employee number");
            var dto = res.asDto;
        dto.Room = (await _room.GetRoomByStaffId(Staff_id)).Select(x => x.asDto).ToList();
        return Ok(dto); 
 
    
    }

    [HttpPost]

    public async Task<ActionResult<StaffDTO>> CreateStaff([FromBody] CreateStaffDTO Data)
    {
        // if (!(new string[] { "male", "female" }.Contains(Data.Gender.Trim().ToLower())))
        // return BadRequest("Gender value is not recognized");

        // var subtractDate = DateTimeOffset.Now - Data.DateOfBirth;
        // if (subtractDate.TotalDays / 365 < 18.0)
        // return BadRequest("Employee must be at least 18 years old");/

        var toCreateStaff= new Staff
        {

           StaffId=Data.StaffId,
           StaffName=Data.StaffName,
           DateOfBirth=Data.DateOfBirth,
           Gender=Data.Gender,
           Mobile=Data.Mobile,
           Shift=Data.Shift,

        };
        var createdStaff = await _Staff.Create(toCreateStaff);

        return StatusCode(StatusCodes.Status201Created);


    }

    [HttpPut("{Staff_id}")]
    public async Task<ActionResult> UpdateStaff([FromRoute] long Staff_id,
    [FromBody] StaffUpdateDTO Data)
    {
        var existing = await _Staff.GetById(Staff_id);
        if (existing is null)
            return NotFound("No Product found with given customer number");

        var toUpdateStaff = existing with
        {
            // Email = Data.Email?.Trim()?.ToLower() ?? existing.Email,
            // LastName = Data.LastName?.Trim() ?? existing.LastName,
            // Mobile = Data.Mobile ?? existing.Mobile,
            // DateOfBirth = existing.DateOfBirth.UtcDateTime,
        };

        var didUpdate = await _Staff.Update(toUpdateStaff);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update");
        return NoContent();
    }

    [HttpDelete("{Staff_id}")]
    public async Task<ActionResult> DeleteStaff([FromRoute] long StaffId)
    {
        var existing = await _Staff.GetById(StaffId);
        if (existing is null)
            return NotFound("No Product found with given employee number");
        await _Staff.Delete(StaffId);
        return NoContent();
    }
}