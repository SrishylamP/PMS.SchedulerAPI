using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class AppointmentSlot
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string TimeSlot { get; set; }
    }
}
