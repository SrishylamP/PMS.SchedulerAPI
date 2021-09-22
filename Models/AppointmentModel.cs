using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.SchedulerAPI.Models
{
    public class AppointmentModel
    {
        public int? AppointmentId { get; set; }
        public int PhysicianId { get; set; }
        public int PatientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string AppointmentStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? AppointmentSlotId { get; set; }
        public string Reason { get; set; }
        public string CreatedByName { get; set; }
        public string PhysicianName { get; set; }
        public string PatientName { get; set; }
        public string PhysicianEmployeeId { get; set; }
        public string AppointmentType { get; set; }
    }
}
