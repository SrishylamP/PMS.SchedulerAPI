using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.SchedulerAPI.Models
{
   public class UserModel
    {
        public string Title { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public int NoOfWrongAttempts { get; set; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }        
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Gender { get; set; }

        public List<PatientModel> Patient { get; set; }
    }
}
