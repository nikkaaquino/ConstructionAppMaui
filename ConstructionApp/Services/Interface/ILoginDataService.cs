using ConstructionApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionApp.DataServices.Interface
{
    public interface ILoginDataService
    {
        Task<LoginModel> LoginUser(string username, string password);
    }
}
