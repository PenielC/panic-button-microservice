using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Support.interfaces
{
    public interface ISupportService : IGenericService<ApplicationUser>
    {
        Task<LoginResponse> LoginUser(LoginRequest login);
        Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration);
    }
}
