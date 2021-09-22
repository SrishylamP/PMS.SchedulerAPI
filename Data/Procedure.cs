using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class Procedure
    {
        public Procedure()
        {
            PatientProcedureDetails = new HashSet<PatientProcedureDetail>();
        }

        [Key]
        [Column("ProcedureID")]
        public int ProcedureId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProcedureCode { get; set; }
        [Required]
        [StringLength(300)]
        public string ProcedureName { get; set; }
        [Required]
        [StringLength(100)]
        public string ProcedureApproach { get; set; }
        public bool ProcedureIsDepricated { get; set; }

        [InverseProperty(nameof(PatientProcedureDetail.Procedure))]
        public virtual ICollection<PatientProcedureDetail> PatientProcedureDetails { get; set; }
    }
}
