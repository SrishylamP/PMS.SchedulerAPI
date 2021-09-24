using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMS.SchedulerAPI.Models;

namespace PMS.SchedulerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class SchedulerController : ControllerBase
    {
        private readonly ISchedulerService SchedulerService;
        private readonly ILogger<SchedulerController> logger;

        public SchedulerController(ISchedulerService SchedulerService, ILogger<SchedulerController> logger)
        {
            this.SchedulerService = SchedulerService;
            this.logger = logger;
        }
        [HttpPost]
        [Route("AddAppointment")]
        public async Task<IActionResult> AddAppointment([FromBody] AppointmentModel model)
        {
            try
            {
                var result = await this.SchedulerService.AddAppointment(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error,ex,ex.Message);
                return BadRequest(ex);

            }

        }
        [HttpPost]
        [Route("EditAppointment")]
        public async Task<IActionResult> EditAppointment([FromBody] AppointmentModel model)
        {
            try
            {
                var result = await this.SchedulerService.EditAppointment(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error,ex,ex.Message);
                return BadRequest(ex);

            }

        }
        [HttpGet]
        [Route("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var result = await this.SchedulerService.GetAllAppointments();                
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error,ex,ex.Message);
                return BadRequest(ex);

            }

        }
        [HttpGet]
        [Route("GetAvailableSlotsByPhysician")]
        public async Task<IActionResult> GetAvailableSlots(int physicianId, string date, int patientId)
        {
            try
            {
                var result = await this.SchedulerService.GetAvailableSlots(physicianId, date,patientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error,ex,ex.Message);
                return BadRequest(ex);

            }

        }
        [HttpPost]
        [Route("DeleteAppointment")]
        public async Task<IActionResult> DeleteAppointment([FromBody] AppointmentModel model)
        {
            try
            {
                var result = await this.SchedulerService.DeleteAppointment(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex.Message);
                return BadRequest(ex);

            }

        }
        [HttpGet]
        [Route("GetAppointmentHistoryByAppointmentId")]
        public async Task<IActionResult> GetAppointmentHistoryByAppointmentId(int appointmentId)
        {
            try
            {
                var result = await this.SchedulerService.GetAppointmentHistoryByAppointmentId(appointmentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex.Message);
                return BadRequest(ex);

            }
        }

        [HttpGet]
        [Route("GetPhysiciansByPatient")]
        public async Task<IActionResult> GetPhysiciansByPatient(int patientId)
        {
            try
            {
                var result = await this.SchedulerService.GetPhysiciansByPatient(patientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex.Message);
                return BadRequest(ex);

            }
        }

        [HttpGet]
        [Route("GetDataCollectionAppointmentsByPatient")]
        public async Task<IActionResult> GetDataCollectionAppointmentsByPatient(int patientId)
        {
            try
            {
                var result = await this.SchedulerService.GetDataCollectionAppointmentsByPatient(patientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex.Message);
                return BadRequest(ex);

            }
        }

        [HttpGet]
        [Route("GetAllDeclinedAppointments")]
        public async Task<IActionResult> GetAllDeclinedAppointments()
        {
            try
            {
                var result = await this.SchedulerService.GetAllDeclinedAppointments();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, ex.Message);
                return BadRequest(ex);

            }

        }

    }
}
