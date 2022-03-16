using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;
public interface IScheduleRepository
{
    Task<Schedule> Create(Schedule Item);
    Task<bool> Update(Schedule item);
    Task<bool> Delete(long ScheduleId);
    Task<Schedule> GetById(long ScheduleId);
    Task<List<Schedule>> GetList();
     Task<List<Schedule>> GetListByGuestId(long GuestId);

    Task<List<Schedule>> GetScheduleByRoomId(long Schedule);

}
public class ScheduleRepository : BaseRepository, IScheduleRepository
{
    public ScheduleRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Schedule> Create(Schedule item)
    {


        var query = $@"INSERT INTO ""{TableNames.schedule}""
        (schedule_id,check_in,check_out,guest_count,created_at,guest_id,room_id)
        VALUES (@ScheduleId,  @CheckIn, @CheckOut, @GuestCount, @CreatedAt, @GuestId,@RoomId) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Schedule>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long ScheduleId)
    {
        var query = $@"DELETE FROM ""{TableNames.schedule}""
        WHERE schedule_id = @ScheduleId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { ScheduleId });
            return res > 0;
        }
    }

    public async Task<Schedule> GetById(long ScheduleId)
    {
        var query = $@"SELECT * FROM ""{TableNames.schedule}"" WHERE schedule_id = @ScheduleId";

        using (var con = NewConnection)
        return await con.QuerySingleOrDefaultAsync<Schedule>(query, new{ScheduleId});


    }

   

    public async Task<List<Schedule>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.schedule}""";
        List<Schedule> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Schedule>(query)).AsList();
        return res;
    }

    public async  Task<List<Schedule>> GetListByGuestId(long GuestId)
    {
         var query = $@"SELECT * FROM {TableNames.guest} 
        WHERE guest_id = @GuestId";

        using (var con = NewConnection)
            return (await con.QueryAsync<Schedule>(query, new { GuestId })).AsList();
    }    


    public async Task<List<Schedule>> GetScheduleByRoomId(long RoomId)
    {
        var query=$@"SELECT *FROM ""{TableNames.room}""
       
       WHERE room_id = @RoomId"; 

       using (var con=NewConnection){
           
           var res=(await con.QueryAsync<Schedule>(query, new{RoomId})).AsList();
           return res;
       }
    }

    // public Task<List<ScheduleDTO>> GetList(object ScheduleId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Schedule item)
    {
        var query = $@"UPDATE ""{TableNames.schedule}"" SET check_in=@CheckIn,check_out=@CheckOut
         WHERE Schedule_id = @ScheduleId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    // public async Task<Schedule> IScheduleRepository.GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.Schedule}""";
    //     List<Schedule> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Schedule>(query)).AsList();
    //     return res;

}

// public async Task<Schedule> IScheduleRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.Schedule}""";
//     List<Schedule> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Schedule>(query)).AsList();
//     return res;
// }



