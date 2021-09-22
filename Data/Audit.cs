using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    [Table("Audit")]
    public partial class Audit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Operation { get; set; }
        [Required]
        [StringLength(150)]
        public string ObjectName { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
