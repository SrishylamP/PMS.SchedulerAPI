using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.SchedulerAPI.Models
{
    public class AppointmentHistoryModel
    {
        public int AppointmentHistoryId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Reason { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedbyName { get; set; }
        public string ModifiedByName { get; set; }

    }
}
