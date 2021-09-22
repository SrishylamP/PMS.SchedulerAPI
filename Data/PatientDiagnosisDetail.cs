using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientDiagnosisDetail
    {
        [Key]
        public int PatientDiagnosisId { get; set; }
        public int DiagnosisId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? AddedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AddedDate { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("PatientDiagnosisDetails")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(DiagnosisId))]
        [InverseProperty("PatientDiagnosisDetails")]
        public virtual Diagnosis Diagnosis { get; set; }
    }
}
