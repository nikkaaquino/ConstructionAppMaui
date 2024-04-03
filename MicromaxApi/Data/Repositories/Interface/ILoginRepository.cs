using MicromaxApi.Data.Entity;

namespace MicromaxApi.Data.Repositories.Interface
{
    public interface ILoginRepository
    {
        Task<LoginEntity> GetById(string userid);
    }
}
