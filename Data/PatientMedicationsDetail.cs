using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientMedicationsDetail
    {
        [Key]
        public int PatientMedicationId { get; set; }
        public int DrugId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? AddedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AddedDate { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("PatientMedicationsDetails")]
        public virtual Appointment Appointment { get; set; }
        [ForeignKey(nameof(DrugId))]
        [InverseProperty(nameof(Medication.PatientMedicationsDetails))]
        public virtual Medication Drug { get; set; }
    }
}
