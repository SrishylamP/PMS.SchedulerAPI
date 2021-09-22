using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class DiagnosesType
    {
        public DiagnosesType()
        {
            Diagnoses = new HashSet<Diagnosis>();
        }

        [Key]
        public int DiagnosesTypeId { get; set; }
        [Required]
        [Column("DiagnosesType")]
        [StringLength(250)]
        public string DiagnosesType1 { get; set; }

        [InverseProperty(nameof(Diagnosis.DiagnosisType))]
        public virtual ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
