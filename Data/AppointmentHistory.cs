using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    [Table("AppointmentHistory")]
    public partial class AppointmentHistory
    {
        [Key]
        public int AppointmentHistoryId { get; set; }
        public int AppointmentId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [StringLength(200)]
        public string AppointmentTime { get; set; }
        public int Createdby { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(200)]
        public string Reason { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(AppointmentId))]
        [InverseProperty("AppointmentHistories")]
        public virtual Appointment Appointment { get; set; }
    }
}
