using Helper.Request;
using LuciaTech.Helper.Controller.Api.V1;
using LuciaTech.Helper.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Support.interfaces;
using Services.Support.implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountMicroservice.Controllers
{
    public class SupportController : V1Controller
    {
        private readonly ISupportService SupportService;
        public SupportController(ISupportService supportService)
        {
            SupportService = supportService;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddClient(RegistrationRequestViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await SupportService.AddUser(model);                   
                    return Ok(new ApiResponse<bool>().Success(true));
                }
                return BadRequest(new ApiResponse<string>().Success("failed"));
            }
            catch (Exception ex)
            {
                return AppError(ex);
            }
        }
    }
}
