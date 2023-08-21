using System.Data;

namespace MicromaxApi.Data.Repositories.Implementation
{
    public class DapperRepository
    {
        public DapperRepository(IDbConnection connection)
        {
            this.Connection = connection;
        }

        public IDbConnection Connection { get; set; }
    }
}
