using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientProcedureDetail
    {
        [Key]
        public int PatientProcedureId { get; set; }
        public int ProcedureId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? AddedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AddedDate { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("PatientProcedureDetails")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(ProcedureId))]
        [InverseProperty("PatientProcedureDetails")]
        public virtual Procedure Procedure { get; set; }
    }
}
