using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientAllergyDetail
    {
        [Key]
        public int PatientId { get; set; }
        [Key]
        [Column("AllergyID")]
        public int AllergyId { get; set; }
        [StringLength(100)]
        public string AllergyType { get; set; }
        [StringLength(200)]
        public string AllergyName { get; set; }
        [StringLength(200)]
        public string AllergenDescription { get; set; }
        [StringLength(200)]
        public string AllergyClinicalInformation { get; set; }
        public bool? IsAllergyFatal { get; set; }
    }
}
