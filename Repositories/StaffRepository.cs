using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;
public interface IStaffRepository
{
    Task<Staff> Create(Staff Item);
    Task<bool> Update(Staff item);
    Task<bool> Delete(long StaffId);
    Task<Staff> GetById(long StaffId);
    Task<List<Staff>> GetList();
    Task<List<Staff>>GetStaffByRoomId(long RoomId);
}
public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Staff> Create(Staff item)
    {


        var query = $@"INSERT INTO ""{TableNames.staff}""
        (staff_id,staff_name,date_of_birth,gender,mobile,shift)
        VALUES (@StaffId,  @StaffName, @DateOfBirth, @Gender, @Mobile, @Shift) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Staff>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long StaffId)
    {
        var query = $@"DELETE FROM ""{TableNames.staff}""
        WHERE Staff_id = @StaffId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { StaffId });
            return res > 0;
        }
    }

    public async Task<Staff> GetById(long StaffId)
    {
        var query = $@"SELECT * FROM ""{TableNames.staff}""
        WHERE Staff_id = @StaffId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Staff>(query, new
            {
                Staffid = StaffId
            });

    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.staff}""";
        List<Staff> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Staff>(query)).AsList();
        return res;
    }

    public async Task<List<Staff>> GetStaffByRoomId(long RoomId)
    {
       var query=$@"SELECT *FROM ""{TableNames.room}""
       
       WHERE room_id = @RoomId"; 

       using (var con=NewConnection){
           
           var res=(await con.QueryAsync<Staff>(query, new{RoomId})).AsList();
           return res;
       }
    }



    public async Task<bool> Update(Staff item)
    {
        var query = $@"UPDATE ""{TableNames.staff}"" SET 
        mobile=@Mobile,shift=@Shift WHERE Staff_id = @StaffId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

//     public async Task<Staff> IStaffRepository.GetListByRoom(long RoomId)()
//     {
//         var query = $@"SELECT * FROM ""{TableNames.staff}""";
//         List<Staff> res;
//         using (var con = NewConnection)
//             res = (await con.QueryAsync<Staff>(query)).AsList();
//         return res;

}

// public async Task<Staff> IStaffRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.Staff}""";
//     List<Staff> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Staff>(query)).AsList();
//     return res;
// }



