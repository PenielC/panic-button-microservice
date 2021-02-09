using Database;
using Helper.Request;
using Helper.Response;
using LuciaTech.Helper.Provider;
using LuciaTech.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Support.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Support.implementation
{
    public class SupportRepository : GenericRepository<ApplicationUser>, ISupportRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public SupportRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> AddUser(RegistrationRequestViewModel registration)
        {
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                Email = registration.email,
                UserName = $"{registration.username}",
                PhoneNumber = registration.phoneNumber
            }, registration.password);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync($"{registration.username}");
                var profile = new Profile
                {
                    status = Enum.GetName(typeof(ProfileStatus), ProfileStatus.NEW),
                    lastName = registration.lastName,
                    isSupportUser = true,
                    create_at = DateTime.Now,
                    email = registration.email,
                    firstName = registration.firstName,
                    isActive = true,
                    phoneNumber = registration.phoneNumber,
                    updated_at = DateTime.Now,
                    userId = user.Id,
                };

                await ((ApplicationDbContext)DbContext).Profiles.AddAsync(profile);
                await ((ApplicationDbContext)DbContext).SaveChangesAsync();
                //var _adminRole = await _userManager.AddToRoleAsync(user, Role.Admin);
                return user;
            }
            var erorrs = new StringBuilder();
            foreach (var erorr in result.Errors)
            {
                erorrs.Append(erorr.Description + ",");
            }
            throw new HttpException(400, $"user not registered {erorrs.ToString()}");
        }

        public async Task<LoginResponse> LoginUser(LoginRequest login)
        {
            var user = await _userManager.FindByNameAsync(login.username);
            if (user == null)
                throw new HttpException(400, "invalid credentials");
            var result = await _signInManager.PasswordSignInAsync(user, login.password, true, false);
            if (result.Succeeded)
            {
                var profile = await ((ApplicationDbContext)DbContext).Profiles.FirstOrDefaultAsync(x => x.userId == user.Id);

                return new LoginResponse
                {
                    email = user.Email,
                    firstname = profile.firstName,
                    username = user.UserName,
                    profileId = profile.profileId.EncryptId(),
                    userId = user.Id
                };
            }
            throw new HttpException(400, "invalid credentials");
        }
    }
}
