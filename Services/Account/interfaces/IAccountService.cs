using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.interfaces
{
    public interface IAccountService : IGenericService<ApplicationUser>
    {
        Task<LoginResponse> LoginUser(LoginRequest login);
        Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration);
    }
}
