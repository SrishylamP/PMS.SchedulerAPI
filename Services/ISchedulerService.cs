using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMS.SchedulerAPI.Models;

namespace PMS.SchedulerAPI
{
    public interface ISchedulerService
    {
        Task<ResponseMessage> AddAppointment(AppointmentModel model);
        Task<ResponseMessage> EditAppointment(AppointmentModel model);
        Task<List<AppointmentSlotModel>> GetAvailableSlots(int physicianId, string date, int patientId);
        Task<List<AppointmentModel>> GetAllAppointments();
        Task<bool> DeleteAppointment(AppointmentModel model);
        Task<List<AppointmentHistoryModel>> GetAppointmentHistoryByAppointmentId(int appointmentId);
        Task<List<AppointmentModel>> GetDataCollectionAppointmentsByPatient(int patientId);
        Task<List<PhysicianModel>> GetPhysiciansByPatient(int patientId);
        Task<List<AppointmentModel>> GetAllDeclinedAppointments();
        Task<List<AppointmentModel>> GetAllDeclinedDataCollectionAppointments();
    }
}
