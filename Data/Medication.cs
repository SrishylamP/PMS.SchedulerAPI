using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class Medication
    {
        public Medication()
        {
            PatientMedicationsDetails = new HashSet<PatientMedicationsDetail>();
        }

        [Key]
        public int DrugId { get; set; }
        [StringLength(255)]
        public string DrugName { get; set; }
        [StringLength(255)]
        public string DrugForm { get; set; }
        [StringLength(255)]
        public string DrugStrength { get; set; }
        [StringLength(255)]
        public string DrugBrandName { get; set; }
        [StringLength(50)]
        public string ReferenceStandard { get; set; }

        [InverseProperty(nameof(PatientMedicationsDetail.Drug))]
        public virtual ICollection<PatientMedicationsDetail> PatientMedicationsDetails { get; set; }
    }
}
