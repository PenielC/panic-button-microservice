using Helper.Config;
using Helper.Request;
using Helper.Response;
using LuciaTech.Helper.Controller.Api.V1;
using LuciaTech.Helper.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Account.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Controllers
{
    public class AccountController: V1Controller
    {
        private readonly IAccountService AccountService;
        private readonly AppCustomConfig Config;
        public AccountController(IAccountService accountService,IOptions<AppCustomConfig> config)
        {
            AccountService = accountService;
            Config = config.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await AccountService.LoginUser(model);
                    return Ok(new ApiResponse<LoginResponse>().Success(loginResponseBuild(response)));
                }
                return BadRequest(new ApiResponse<string>().Success("failed"));
            }
            catch (Exception ex)
            {

                return AppError(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUser([FromBody] RegistrationRequestViewModel registration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await AccountService.AddUser(registration);
                    return Ok(new ApiResponse<bool>().Success(true));
                }
                return BadRequest(new ApiResponse<string>().Success("failed"));
            }
            catch (Exception ex)
            {

                return AppError(ex);
            }
        }

        private LoginResponse loginResponseBuild(LoginResponse user)
        {
            user.token = TokenGen.GenerateTokenJWT($"{user.userId.Encrypt()}#{user.email.Encrypt()}#{user.username.Encrypt()}#{user.profileId}", Config.JWTISSUER, Config.JWTKEY, "user");
            return user;

            //user.token = TokenGen.GenerateTokenJWT($"{user.userId.Encrypt()}#{user.email.Encrypt()}#{user.username.Encrypt()}#{user.employeeId}#{user.clientId}", Config.JWTISSUER, Config.JWTKEY, "user");
            //return user;
        }
       
    }
}
