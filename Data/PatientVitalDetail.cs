using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientVitalDetail
    {
        [Key]
        public int VitalId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        [StringLength(50)]
        public string Height { get; set; }
        [StringLength(50)]
        public string Weight { get; set; }
        [StringLength(50)]
        public string BloodPressure { get; set; }
        [StringLength(50)]
        public string BodyTemprature { get; set; }
        [StringLength(50)]
        public string RespirationRate { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("PatientVitalDetails")]
        public virtual Appointment Appointment { get; set; }
    }
}
