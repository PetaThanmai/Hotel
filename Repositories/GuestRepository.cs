using Dapper;
using Hotel.Models;
using Hotel.Utilities;

namespace Hotel.Repositories;
public interface IGuestRepository
{
    Task<Guest> Create(Guest Item);
    Task<bool> Update(Guest item);
    Task<bool> Delete(long GuestId);
    Task<Guest> GetById(long GuestId);
    Task<List<Guest>> GetList();
    // Task<List<Guest>> GetListByGuestId(long ScheduleId);
     Task<List<Guest>> GetGuestByScheduleId(long GuestId);
    // Task<Guest> GetById(long Id);
}
public class GuestRepository : BaseRepository, IGuestRepository
{
    public GuestRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<Guest> Create(Guest item)
    {


        var query = $@"INSERT INTO ""{TableNames.guest}""
        (guest_id,guest_name,mobile,email,date_of_birth,gender)
        VALUES (@GuestId,  @GuestName, @Mobile, @Email, @DateOfBirth, @Gender) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Guest>(query, item);

            return res;
        }

    }

    public async Task<bool> Delete(long GuestId)
    {
        var query = $@"DELETE FROM ""{TableNames.guest}""
        WHERE guest_id = @GuestId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { GuestId });
            return res > 0;
        }
    }

    public async Task<Guest> GetById(long GuestId)
    {
        var query = $@"SELECT * FROM ""{TableNames.guest}""
        WHERE guest_id = @GuestId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Guest>(query, new
            {
                Guestid = GuestId
            });

    }

   

    public async Task<List<Guest>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.guest}""";
        List<Guest> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Guest>(query)).AsList();
        return res;
    }

    public async Task<List<Guest>> GetGuestByScheduleId(long GuestId)
    {
        var query = $@"SELECT * FROM ""{TableNames.guest}""
        WHERE guest_id = @GuestId";
 
        using(var con = NewConnection){
           var res = (await con.QueryAsync<Guest>(query,new{GuestId})).AsList();
           return res;
        }
    }
    // public Task<List<GuestDTO>> GetList(object GuestId)
    // {

    //     return null;
    // }

    public async Task<bool> Update(Guest item)
    {
        var query = $@"UPDATE ""{TableNames.guest}"" SET guest_name=@GuestName,
        mobile=@Mobile,email=@Email WHERE Guest_id = @GuestId";

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, item);
            return rowCount == 1;
        }
    }

    // public async Task<Guest> IGuestRepository.GetList()
    // {
    //     var query = $@"SELECT * FROM ""{TableNames.Guest}""";
    //     List<Guest> res;
    //     using (var con = NewConnection)
    //         res = (await con.QueryAsync<Guest>(query)).AsList();
    //     return res;

}

// public async Task<Guest> IGuestRepository.GetList()
// {
//     var query = $@"SELECT * FROM ""{TableNames.Guest}""";
//     List<Guest> res;
//     using (var con = NewConnection)
//         res = (await con.QueryAsync<Guest>(query)).AsList();
//     return res;
// }



