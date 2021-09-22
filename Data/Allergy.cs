using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    [Keyless]
    [Table("Allergy")]
    public partial class Allergy
    {
        [Column("AllergyID")]
        public int AllergyId { get; set; }
        [Required]
        [StringLength(150)]
        public string AllergyCode { get; set; }
        [StringLength(100)]
        public string AllergyType { get; set; }
        [StringLength(200)]
        public string AllergyName { get; set; }
        [StringLength(200)]
        public string AllergenDescription { get; set; }
        [StringLength(200)]
        public string AllergyClinicalInformation { get; set; }
        [StringLength(100)]
        public string Allerginicity { get; set; }
    }
}
