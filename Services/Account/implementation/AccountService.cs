using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Service;
using Repository.Account.interfaces;
using Services.Account.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Account.implementation
{
    public class AccountService : GenericService<ApplicationUser>, IAccountService
    {
        private readonly IAccountRepository AccountRepository;
        public AccountService(IAccountRepository accountRepository):base(accountRepository)
        {
            AccountRepository = accountRepository;
        }

        public async Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration)
        {
            return await AccountRepository.AddUser(registration);
        }

        public async Task<LoginResponse> LoginUser(LoginRequest login)
        {
            return await AccountRepository.LoginUser(login);
        }
    }
}
