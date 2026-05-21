using System.ComponentModel.DataAnnotations;

namespace ProgrammeManagementSystemApi.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ModuleID { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateRegistered { get; set; } = DateTime.Now;

        // Navigation properties
        public Student? Student { get; set; }
        public Module? Module { get; set; }
    }
}
