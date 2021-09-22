using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PatientDetail
    {
        [Key]
        public int PatientId { get; set; }
        public int? UserId { get; set; }
        public int? Age { get; set; }
        [StringLength(20)]
        public string Race { get; set; }
        [StringLength(20)]
        public string Ethnicity { get; set; }
        [StringLength(200)]
        public string LanguagesKnow { get; set; }
        [StringLength(200)]
        public string HomeAddress { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("PatientDetails")]
        public virtual User User { get; set; }
    }
}
