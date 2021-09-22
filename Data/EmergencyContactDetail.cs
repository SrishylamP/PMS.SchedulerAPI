using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class EmergencyContactDetail
    {
        [Key]
        public int EmergencyContactId { get; set; }
        public int PatientId { get; set; }
        [Column("EFisrtName")]
        [StringLength(20)]
        public string EfisrtName { get; set; }
        [Column("ELastName")]
        [StringLength(20)]
        public string ElastName { get; set; }
        [StringLength(30)]
        public string Email { get; set; }
        [StringLength(20)]
        public string RelationShip { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        public bool? Access { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
