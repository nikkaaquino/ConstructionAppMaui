using Autofac.Features.AttributeFilters;
using Dapper;
using MicromaxApi.Context;
using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;
using System.Data;

namespace MicromaxApi.Data.Repositories.Implementation
{

    public class LoginRepository : ILoginRepository
    {
        private readonly DapperContext _context;

        public LoginRepository(DapperContext context)
        {
            _context =  context;
        }

        public async Task<LoginEntity> GetById(string userid)
        {
            try
            {                
                var sql = "select * from tblUsers where UserID = @uid";
                using (var connection = _context.CreateConnection())
                {
                    var result =  connection.QueryFirstOrDefault<LoginEntity>(sql, new { uid = userid });
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }

        public async Task<bool> VerifyPassword(LoginEntity user, string password)
        {
            return user.UsrPassword == password;
        }
    }
}
