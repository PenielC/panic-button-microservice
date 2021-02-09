using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper.Config;
using Helper.Request;
using Helper.Response;
using LuciaTech.Helper.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Panic.interfaces;
using Database;
using LuciaTech.Helper.Controller.Api.V1;

namespace PanicMicroservice.Controllers
{
    public class PanicController : V1Controller
    {
        private readonly IPanicService PanicService;
        private readonly AppCustomConfig Config;
        public PanicController(IPanicService panicService, IOptions<AppCustomConfig> config)
        {
            PanicService = panicService;
            Config = config.Value;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<PanicAlerts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePanicAlert(CreatePanicAlertRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await PanicService.InsertAsync(buildPanic(model));

                    if (response != null)
                        return Ok(new ApiResponse<PanicAlerts>().Success(response));
                    throw new HttpException(400, "Panic Alert not created");
                }
                throw new HttpException(400, "failed");
            }
            catch (Exception ex)
            {
                return AppError(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePanicAlertResolution(PanicAlertResolution model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await buildPanicResolution(model);
                    var response = PanicService.CreatePanicAlertResolution(model);
                    if (response != false)
                        return Ok(new ApiResponse<bool>().Success(response));
                    throw new HttpException(400, "Panic Alert resolution not created");
                }
                throw new HttpException(400, "failed");
            }
            catch (Exception ex)
            {
                return AppError(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<PanicAlerts>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TakeOnPanicAlert(PanicAlerts model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await PanicService.UpdateAsync( await buildUpdate(model));

                    if (response != null)
                        return Ok(new ApiResponse<PanicAlerts>().Success(response));
                    throw new HttpException(400, "No record updated");
                }
                throw new HttpException(400, "failed");
            }
            catch (Exception ex)
            {
                return AppError(ex);
            }
        }

        private PanicAlerts buildPanic(CreatePanicAlertRequest model)
        {
            return new PanicAlerts
            {
                alertId = 0,
                alertType = model.alertType,
                panicStatus = model.panicStatus,
                isActive = model.isActive,
                latitude = model.latitude,
                longitude = model.longitude,
                create_at = DateTime.Now
            };
        }

        private async Task<PanicAlerts> buildUpdate(PanicAlerts model)
        {
            var panicAlert = await PanicService.GetByIdAsync(model.alertId);
            return new PanicAlerts
            {
                alertId = panicAlert.alertId,
                alertType = panicAlert.alertType,
                panicStatus = PanicStatus.INPROGRESS.ToString(),
                isActive = true,
                latitude = panicAlert.latitude,
                longitude = panicAlert.longitude,
                updated_at = DateTime.Now
            };
        }

        private async Task<PanicAlerts> buildPanicResolution(PanicAlertResolution model)
        {
            var panicAlert = await PanicService.GetByIdAsync(model.alertId);
            panicAlert.panicStatus = PanicStatus.RESOLVED.ToString();
            panicAlert.isActive = false;
            panicAlert.updated_at = DateTime.Now;
            return await PanicService.UpdateAsync(panicAlert);        
        }
    }
}
