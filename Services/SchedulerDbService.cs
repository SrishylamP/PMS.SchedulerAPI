using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.SchedulerAPI.Data;
using PMS.SchedulerAPI.Models;

namespace PMS.SchedulerAPI
{
    public class SchedulerDbService : ISchedulerService
    {
        private readonly PMSDbContext _context;
        public SchedulerDbService(PMSDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseMessage> AddAppointment(AppointmentModel model)
        {
            try
            {
                var resObj = new ResponseMessage();

                Appointment app = new Appointment
                {
                    Title = model.Title,
                    Description = model.Description,
                    AppointmentDate = model.AppointmentDate.ToLocalTime(),
                    AppointmentTime = model.AppointmentTime,
                    AppointmentStatus = model.AppointmentStatus,
                    AppointmentType = model.AppointmentType,
                    PatientId = model.PatientId,
                    PhysicianId = model.PhysicianId,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    AppointmentSlotId = model.AppointmentSlotId
                };
                await _context.Appointments.AddAsync(app);
                await _context.SaveChangesAsync();
                //Audit 
                var audit = new AuditModel
                {
                    Operation = Constants.Create,
                    ObjectName = "Appointment",
                    Description = $"Appointment Created Id: {app.AppointmentId}",
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                AuditMe(audit);

                AppointmentHistory ahObj = new AppointmentHistory
                {
                    AppointmentId = app.AppointmentId,
                    Reason = "Not Applicable",
                    AppointmentDate = app.AppointmentDate.ToLocalTime(),
                    AppointmentTime = app.AppointmentTime,
                    Createdby = app.CreatedBy,
                    CreatedDate = app.CreatedDate
                };
                await _context.AppointmentHistories.AddAsync(ahObj);
                await _context.SaveChangesAsync();
                var auditobj = new AuditModel
                {
                    Operation = Constants.Create,
                    ObjectName = "Appointment History",
                    Description = $"Appointment History Created Id: {ahObj.AppointmentHistoryId}",
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                AuditMe(auditobj);

                resObj.IsSuccess = true;
                resObj.message = Constants.AppointmentBookedSuccessfully;

                var Physician = _context.Users.FirstOrDefault(e => e.UserId == model.PhysicianId);
                var patient = _context.Users.FirstOrDefault(e => e.UserId == model.PatientId);

                //Email to patient
                Common.SendEmail(patient.FirstName, Constants.FromEmail, patient.Email, Constants.AppointmentSubject,
                    Constants.AppointmentBookedSuccessfully + "<br/> Title: " + model.Title + " <br/> Date: " + model.AppointmentDate.ToShortDateString() + ", " +
                    model.AppointmentTime + " <br/> Physician: " + Physician.FirstName + " " + Physician.LastName);

                //Email to Physician
                Common.SendEmail(Physician.FirstName, Constants.FromEmail, Physician.Email, Constants.AppointmentSubject,
                    Constants.AppointmentBookedSuccessfully + "<br/> Title: " + model.Title + " <br/> Date: " + model.AppointmentDate.ToShortDateString() + ", " +
                    model.AppointmentTime + " <br/> Patient: " + patient.FirstName + " " + patient.LastName);
                return resObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ResponseMessage> EditAppointment(AppointmentModel model)
        {
            try
            {
                var resObj = new ResponseMessage();
                var currentDate = Convert.ToString(model.AppointmentDate.ToLocalTime().ToString("dd/MM/yyyy"));
                var modifiedDate = DateTime.ParseExact(currentDate, "dd/MM/yyyy", null);
                var aptObj = _context.Appointments.FirstOrDefault(e => e.AppointmentId == model.AppointmentId);

                AppointmentHistory ahObj = new AppointmentHistory
                {
                    AppointmentId = aptObj.AppointmentId,
                    Reason = model.Reason,
                    AppointmentDate = model.AppointmentDate,
                    AppointmentTime = model.AppointmentTime,
                    Createdby = model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedDate = DateTime.Now
                };
                await _context.AppointmentHistories.AddAsync(ahObj);
                aptObj.Title = model.Title;
                aptObj.Description = model.Description;
                aptObj.AppointmentDate = modifiedDate;
                aptObj.AppointmentTime = model.AppointmentTime;
                aptObj.AppointmentStatus = model.AppointmentStatus;
                aptObj.PhysicianId = model.PhysicianId;
                aptObj.PatientId = model.PatientId;
                aptObj.ModifiedBy = model.ModifiedBy;
                aptObj.ModifiedDate = DateTime.Now;
                aptObj.AppointmentSlotId = model.AppointmentSlotId;

                await _context.SaveChangesAsync();
                var auditobj = new AuditModel
                {
                    Operation = Constants.Create,
                    ObjectName = "Appointment History",
                    Description = $"Appointment History Created Id: {ahObj.AppointmentHistoryId}",
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                AuditMe(auditobj);
                var audit = new AuditModel
                {
                    Operation = Constants.Update,
                    ObjectName = "Appointment",
                    Description = $"Appointment Updated Id: {aptObj.AppointmentId}",
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                AuditMe(auditobj);
                resObj.IsSuccess = true;
                resObj.message = Constants.AppointmentRescheduledSuccessfully;

                var Physician = _context.Users.FirstOrDefault(e => e.UserId == model.PhysicianId);
                var patient = _context.Users.FirstOrDefault(e => e.UserId == model.PatientId);
                var ModifiedByName = _context.Users.Where(e => e.UserId == model.ModifiedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault();

                //Email to patient
                Common.SendEmail(patient.FirstName, Constants.FromEmail, patient.Email, Constants.EditAppointmentSubject,
                Constants.AppointmentRescheduledSuccessfully + "<br/> Title: " + model.Title +" <br/> Rescheduled By: " + ModifiedByName + " <br/> Rescheduled Appointment Date: " + model.AppointmentDate.ToShortDateString()
                + "<br/> Rescheduled Appointment Slot:  " + model.AppointmentTime + " <br/> Previous Appointment Date: " + aptObj.AppointmentDate.ToShortDateString()
                + "<br/> Rescheduled Appointment Slot:  " + aptObj.AppointmentTime+ " <br/> Physician: " + Physician.FirstName + " " + Physician.LastName);

                //Email to Physician
                Common.SendEmail(Physician.FirstName, Constants.FromEmail, Physician.Email, Constants.EditAppointmentSubject,
                    Constants.AppointmentRescheduledSuccessfully + "<br/> Title: " + model.Title + " <br/> Rescheduled By: " + ModifiedByName + " <br/> Rescheduled Appointment Date: " + model.AppointmentDate.ToShortDateString()
                + "<br/> Rescheduled Appointment Slot:  " + model.AppointmentTime + " <br/> Previous Appointment Date: " + aptObj.AppointmentDate.ToShortDateString()
                + "<br/> Rescheduled Appointment Slot:  " + aptObj.AppointmentTime + " <br/> Patient: " + patient.FirstName + " " + patient.LastName);

                //Constants.AppointmentRescheduledSuccessfully + "<br/> Title: " + model.Title + " <br/> Rescheduled By: " + ModifiedByName + " <br/> Date: " + model.AppointmentDate.ToShortDateString() + ", " +
                //    model.AppointmentTime + " was(" + ahObj.AppointmentDate.ToShortDateString() + ", " + ahObj.AppointmentTime + ") <br/> Patient: " + patient.FirstName + " " + patient.LastName);
                return resObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<AppointmentModel>> GetAllAppointments()
        {
            var list = await _context.Appointments
                .Where(e => !e.IsDeleted)
                .Select(e => new AppointmentModel
                {
                    Title = e.Title,
                    Description = e.Description,
                    AppointmentId = e.AppointmentId,
                    AppointmentStatus = e.AppointmentStatus,
                    AppointmentTime = e.AppointmentTime,
                    AppointmentDate = e.AppointmentDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate,
                    PatientId = e.PatientId,
                    PhysicianId = e.PhysicianId,
                    AppointmentSlotId = e.AppointmentSlotId,
                    CreatedByName = _context.Users.Where(a => a.UserId == e.CreatedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianEmployeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    PatientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    Reason = _context.AppointmentHistories.Where(a => a.AppointmentId == e.AppointmentId).OrderByDescending(a => a.AppointmentHistoryId).Select(e => e.Reason).FirstOrDefault(),
                }).ToListAsync();
            return list;
        }
        public async Task<List<AppointmentSlotModel>> GetAvailableSlots(int physicianId, string date, int patientId)
        {
            var appDate = DateTime.ParseExact(date, "dd/MM/yyyy", null);
            var physicianAppointments = await _context.Appointments
                .Where(e => e.PhysicianId == physicianId &&
                e.AppointmentDate.Date == appDate && !e.IsDeleted)
                .Select(e => e.AppointmentTime).ToListAsync();
            var patientAppointments = await _context.Appointments
                .Where(e => e.PatientId == patientId &&
                        e.AppointmentDate.Date == appDate && !e.IsDeleted)
                .Select(e => e.AppointmentTime).ToListAsync();

            var physicianAvailableSlots = await _context.AppointmentSlots.
                Where(e => !physicianAppointments.Contains(e.TimeSlot)).ToListAsync();
            var availableSlots = physicianAvailableSlots
                 .Where(e => !patientAppointments.Contains(e.TimeSlot))
                 .Select(e => new AppointmentSlotModel
                 {
                     Id = e.Id,
                     TimeSlot = e.TimeSlot,
                 }).ToList();
            return availableSlots;
        }
        public async Task<bool> DeleteAppointment(AppointmentModel model)
        {
            bool isDeleted = false;
            var appointment = await _context.Appointments.FirstOrDefaultAsync(e => e.AppointmentId == model.AppointmentId);
            if (appointment != null)
            {
                //var apHistory = await _context.AppointmentHistories.Where(e => e.AppointmentId == appointmentId).ToListAsync();
                //foreach (var item in apHistory)
                //{
                //    _context.AppointmentHistories.Remove(item);
                //    var audit = new AuditModel
                //    {
                //        Operation = Constants.Delete,
                //        ObjectName = "Appointment",
                //        Description = $"Appointment History Deleted Id: {item.AppointmentHistoryId}",
                //        CreatedBy = appointment.CreatedBy,
                //        CreatedDate = DateTime.Now
                //    };
                //    lookupDbService.AuditMe(audit);                    
                //};
                //_context.Appointments.Remove(appointment);
                appointment.IsDeleted = true;
                await _context.SaveChangesAsync();
                var auditobj = new AuditModel
                {
                    Operation = Constants.Update,
                    ObjectName = "Appointment",
                    Description = $"Appointment Updated  Id: {appointment.AppointmentId}",
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now
                };
                AuditMe(auditobj);
                isDeleted = true;


                var Physician = _context.Users.FirstOrDefault(e => e.UserId == model.PhysicianId);
                var patient = _context.Users.FirstOrDefault(e => e.UserId == model.PatientId);

                //Email to patient
                Common.SendEmail(patient.FirstName, Constants.FromEmail, patient.Email, Constants.AppointmentCancelledSubject,
                    Constants.AppointmentCancelledSuccessfully + "<br/> Title: " + model.Title + " <br/> Date: " + model.AppointmentDate.ToShortDateString() + ", " +
                    model.AppointmentTime + " <br/> Physician: " + Physician.FirstName + " " + Physician.LastName);

                //Email to Physician
                Common.SendEmail(Physician.FirstName, Constants.FromEmail, Physician.Email, Constants.AppointmentCancelledSubject,
                    Constants.AppointmentCancelledSuccessfully + "<br/> Title: " + model.Title + " <br/> Date: " + model.AppointmentDate.ToShortDateString() + ", " +
                    model.AppointmentTime + " <br/> Patient: " + patient.FirstName + " " + patient.LastName);
            }
            else isDeleted = false;
            return isDeleted;

        }

        public async Task<List<AppointmentHistoryModel>> GetAppointmentHistoryByAppointmentId(int appointmentId)
        {
            var list = await _context.AppointmentHistories
                .Where(e => e.AppointmentId == appointmentId)
                .OrderByDescending(e => e.CreatedDate)
                .Select(e => new AppointmentHistoryModel
                {
                    AppointmentHistoryId = e.AppointmentHistoryId,
                    AppointmentId = e.AppointmentId,
                    AppointmentDate = e.AppointmentDate,
                    AppointmentTime = e.AppointmentTime,
                    Createdby = e.Createdby,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate,
                    Reason = e.Reason,
                    CreatedbyName = _context.Users.Where(a => a.UserId == e.Createdby).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    ModifiedByName = _context.Users.Where(a => a.UserId == e.ModifiedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault()
                }).ToListAsync();
            return list;
        }

        public async Task<List<AppointmentModel>> GetDataCollectionAppointmentsByPatient(int patientId)
        {
            var list = await _context.Appointments
                .Where(e => !e.IsDeleted && e.PatientId == patientId && e.AppointmentType == "Data Collection")
                .Select(e => new AppointmentModel
                {
                    Title = e.Title,
                    Description = e.Description,
                    AppointmentId = e.AppointmentId,
                    AppointmentStatus = e.AppointmentStatus,
                    AppointmentType = e.AppointmentType,
                    AppointmentTime = e.AppointmentTime,
                    AppointmentDate = e.AppointmentDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    PatientId = e.PatientId,
                    PhysicianId = e.PhysicianId,
                    AppointmentSlotId = e.AppointmentSlotId,
                    CreatedByName = _context.Users.Where(a => a.UserId == e.CreatedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianEmployeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    PatientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    Reason = _context.AppointmentHistories.Where(a => a.AppointmentId == e.AppointmentId).OrderByDescending(a => a.AppointmentHistoryId).Select(e => e.Reason).FirstOrDefault(),
                }).ToListAsync();
            return list;
        }

        public async Task<List<AppointmentModel>> GetAllDataCollectionAppointments()
        {
            var list = await _context.Appointments
                .Where(e => !e.IsDeleted && e.AppointmentType == "Data Collection")
                .Select(e => new AppointmentModel
                {
                    Title = e.Title,
                    Description = e.Description,
                    AppointmentId = e.AppointmentId,
                    AppointmentStatus = e.AppointmentStatus,
                    AppointmentType = e.AppointmentType,
                    AppointmentTime = e.AppointmentTime,
                    AppointmentDate = e.AppointmentDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    PatientId = e.PatientId,
                    PhysicianId = e.PhysicianId,
                    AppointmentSlotId = e.AppointmentSlotId,
                    CreatedByName = _context.Users.Where(a => a.UserId == e.CreatedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianEmployeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    PatientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    Reason = _context.AppointmentHistories.Where(a => a.AppointmentId == e.AppointmentId).OrderByDescending(a => a.AppointmentHistoryId).Select(e => e.Reason).FirstOrDefault(),
                }).ToListAsync();
            return list;
        }
        public async Task<List<UserModel>> GetPhysiciansByPatient(int patientId)
        {
            var list = await (from ap in _context.Appointments
                              join u in _context.Users on ap.PhysicianId equals u.UserId
                              where ap.PatientId == patientId && ap.AppointmentStatus == "Closed"
                              select new UserModel { UserId = u.UserId, FirstName = u.FirstName, LastName = u.LastName, EmployeeId = u.EmployeeId }
                            ).ToListAsync();
            return list;
        }

        public async Task<List<AppointmentModel>> GetAllDeclinedAppointments()
        {
            var list = await _context.Appointments
                .Where(e => e.IsDeleted == true)
                .Select(e => new AppointmentModel
                {
                    Title = e.Title,
                    Description = e.Description,
                    AppointmentId = e.AppointmentId,
                    AppointmentStatus = e.AppointmentStatus,
                    AppointmentTime = e.AppointmentTime,
                    AppointmentDate = e.AppointmentDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate,
                    PatientId = e.PatientId,
                    PhysicianId = e.PhysicianId,
                    AppointmentSlotId = e.AppointmentSlotId,
                    CreatedByName = _context.Users.Where(a => a.UserId == e.CreatedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianEmployeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    PatientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    Reason = _context.AppointmentHistories.Where(a => a.AppointmentId == e.AppointmentId).OrderByDescending(a => a.AppointmentHistoryId).Select(e => e.Reason).FirstOrDefault(),
                }).ToListAsync();
            return list;
        }

        public async Task<List<AppointmentModel>> GetAllDeclinedDataCollectionAppointments()
        {
            var list = await _context.Appointments
                .Where(e => e.IsDeleted == true && e.AppointmentType == "Data Collection")
                .Select(e => new AppointmentModel
                {
                    Title = e.Title,
                    Description = e.Description,
                    AppointmentId = e.AppointmentId,
                    AppointmentStatus = e.AppointmentStatus,
                    AppointmentTime = e.AppointmentTime,
                    AppointmentDate = e.AppointmentDate,
                    CreatedBy = e.CreatedBy,
                    CreatedDate = e.CreatedDate,
                    ModifiedBy = e.ModifiedBy,
                    ModifiedDate = e.ModifiedDate,
                    PatientId = e.PatientId,
                    PhysicianId = e.PhysicianId,
                    AppointmentSlotId = e.AppointmentSlotId,
                    CreatedByName = _context.Users.Where(a => a.UserId == e.CreatedBy).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianName = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    PhysicianEmployeeId = _context.Users.Where(a => a.UserId == e.PhysicianId).Select(e => e.EmployeeId).FirstOrDefault(),
                    PatientName = _context.Users.Where(a => a.UserId == e.PatientId).Select(e => e.FirstName + " " + e.LastName).FirstOrDefault(),
                    Reason = _context.AppointmentHistories.Where(a => a.AppointmentId == e.AppointmentId).OrderByDescending(a => a.AppointmentHistoryId).Select(e => e.Reason).FirstOrDefault(),
                }).ToListAsync();
            return list;
        }
        public void AuditMe(AuditModel model)
        {
            var aObj = new Audit
            {
                Operation = model.Operation,
                Description = model.Description,
                ObjectName = model.ObjectName,
                CreatedBy = model.CreatedBy,
                CreatedDate = DateTime.Now
            };
            _context.Audits.Add(aObj);
            _context.SaveChanges();
        }
    }

}
