using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Service;
using Repository.Support.interfaces;
using Services.Support.interfaces;
using System.Threading.Tasks;

namespace Services.Support.implementation
{
    public class SupportService : GenericService<ApplicationUser>, ISupportService
    {
        private readonly ISupportRepository SupportRepository;
        public SupportService(ISupportRepository supportRepository) : base(supportRepository)
        {
            SupportRepository = supportRepository;
        }

        public async Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration)
        {
            return await SupportRepository.AddUser(registration);
        }

        public async Task<LoginResponse> LoginUser(LoginRequest login)
        {
            return await SupportRepository.LoginUser(login);
        }
    }
}
