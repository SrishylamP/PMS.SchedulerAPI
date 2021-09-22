using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class Diagnosis
    {
        public Diagnosis()
        {
            PatientDiagnosisDetails = new HashSet<PatientDiagnosisDetail>();
        }

        [Key]
        public int DiagnosisId { get; set; }
        [Required]
        [StringLength(50)]
        public string DiagnosisCode { get; set; }
        [Required]
        [StringLength(250)]
        public string DiagnosisName { get; set; }
        public int DiagnosisTypeId { get; set; }
        public bool DiagnosisIsDepricated { get; set; }

        [ForeignKey(nameof(DiagnosisTypeId))]
        [InverseProperty(nameof(DiagnosesType.Diagnoses))]
        public virtual DiagnosesType DiagnosisType { get; set; }
        [InverseProperty(nameof(PatientDiagnosisDetail.Diagnosis))]
        public virtual ICollection<PatientDiagnosisDetail> PatientDiagnosisDetails { get; set; }
    }
}
