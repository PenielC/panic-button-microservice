using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Account.interfaces
{
    public interface IAccountRepository : IGenericRepository<ApplicationUser>
    {
        Task<LoginResponse> LoginUser(LoginRequest login);
        Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration);
    }
}
