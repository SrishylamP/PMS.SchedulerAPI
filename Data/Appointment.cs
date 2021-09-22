using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    [Table("Appointment")]
    public partial class Appointment
    {
        public Appointment()
        {
            AppointmentHistories = new HashSet<AppointmentHistory>();
            PatientDiagnosisDetails = new HashSet<PatientDiagnosisDetail>();
            PatientMedicationsDetails = new HashSet<PatientMedicationsDetail>();
            PatientProcedureDetails = new HashSet<PatientProcedureDetail>();
            PatientVitalDetails = new HashSet<PatientVitalDetail>();
        }

        [Key]
        public int AppointmentId { get; set; }
        public int PhysicianId { get; set; }
        public int PatientId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [StringLength(200)]
        public string AppointmentTime { get; set; }
        [Required]
        [StringLength(200)]
        public string AppointmentStatus { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public int? AppointmentSlotId { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        public string AppointmentType { get; set; }

        [ForeignKey(nameof(PatientId))]
        [InverseProperty(nameof(User.AppointmentPatients))]
        public virtual User Patient { get; set; }
        [ForeignKey(nameof(PhysicianId))]
        [InverseProperty(nameof(User.AppointmentPhysicians))]
        public virtual User Physician { get; set; }
        [InverseProperty(nameof(AppointmentHistory.Appointment))]
        public virtual ICollection<AppointmentHistory> AppointmentHistories { get; set; }
        [InverseProperty(nameof(PatientDiagnosisDetail.Appointment))]
        public virtual ICollection<PatientDiagnosisDetail> PatientDiagnosisDetails { get; set; }
        [InverseProperty(nameof(PatientMedicationsDetail.Appointment))]
        public virtual ICollection<PatientMedicationsDetail> PatientMedicationsDetails { get; set; }
        [InverseProperty(nameof(PatientProcedureDetail.Appointment))]
        public virtual ICollection<PatientProcedureDetail> PatientProcedureDetails { get; set; }
        [InverseProperty(nameof(PatientVitalDetail.Appointment))]
        public virtual ICollection<PatientVitalDetail> PatientVitalDetails { get; set; }
    }
}
