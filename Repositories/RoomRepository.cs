using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;
public interface IRoomRepository
{
    Task<Room> Create(Room Item);
    Task<bool> Update(Room item);
    Task<bool> Delete(long RoomId);
    Task<Room> GetById(long RoomId);
    Task<List<Room>> GetList();
    Task<List<Room>> GetListByGuestId(long ScheduleId);
    Task<List<Room>> GetRoomByScheduleId(long GuestId);
     Task<List<Room>> GetRoomByStaffId(long StaffId);
}
public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Room> Create(Room item)
    {


        var query = $@"INSERT INTO ""{TableNames.room}""
        (room_id,room_size,price,room_type)
        VALUES (@RoomId,  @RoomSize, @Price ,@RoomType) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Room>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long RoomId)
    {
        var query = $@"DELETE FROM ""{TableNames.room}""
        WHERE Room_id = @RoomId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { RoomId });
            return res > 0;
        }
    }

    public async Task<Room> GetById(long RoomId)
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE Room_id = @RoomId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Room>(query, new
            {
                Roomid = RoomId
            });

    }

    public  async Task<List<Room>> GetListByGuestId(long GuestId)
    {
         var query=$@"SELECT *FROM ""{TableNames.schedule}""
       
       WHERE schedule_id = @ScheduleId"; 

       using (var con=NewConnection){
           
           var res=(await con.QueryAsync<Room>(query, new{GuestId})).AsList();
           return res;
       }
    }

    public async Task<List<Room>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""";
        List<Room> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Room>(query)).AsList();
        return res;
    }
    public async Task<List<Room>> GetRoomByScheduleId(long ScheduleId)
    {
       var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE room_id = @ScheduleId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Room>(query,new{ScheduleId})).AsList();
           return res;
        }
    }
     public async Task<List<Room>> GetRoomByStaffId(long StaffId)
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE staff_id = @StaffId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Room>(query,new{StaffId})).AsList();
           return res;
        }
    }

    // public Task<List<RoomDTO>> GetList(object RoomId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Room item)
    {
        var query = $@"UPDATE ""{TableNames.room}"" SET room_type= @RoomType, room_size= @RoomSize, price= @Price WHERE room_id = @RoomId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    public Task<List<Room>> GetListByScheduleId(long GuestId)
    {
        throw new NotImplementedException();
    }
}