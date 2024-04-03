using Dapper;
using MicromaxApi.Context;
using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;

namespace MicromaxApi.Data.Repositories.Implementation
{

    public class LoginRepository : ILoginRepository
    {
        private readonly DapperContext _context;

        public LoginRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<LoginEntity> GetById(string userid)
        {
            var sql = "select * from tblUsers where UserID = @uid";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryFirstOrDefaultAsync<LoginEntity>(sql, new { uid = userid });
            return result;
        }
    }
}
