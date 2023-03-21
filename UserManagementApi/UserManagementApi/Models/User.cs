using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementApi.Models
{
    public class User
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public string UserID { get; set; }
        public string UserRole { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UserDateOfBirth { get; set; }
        public DateTime UserCreationTime { get; set; }
        public DateTime UserLastModified { get; set; }
        public int UserSalary { get; set; }
        public string UserDescription { get; set; }
    }
}