using System.ComponentModel.DataAnnotations;

namespace ProgrammeManagementSystemApi.Models
{
    public class ModuleAssignment
    {
        [Key]
        public int AssignmentID { get; set; }

        [Required]
        public int LecturerID { get; set; }

        [Required]
        public int ModuleID { get; set; }

        // Navigation properties
        public Lecturer? Lecturer { get; set; }
        public Module? Module { get; set; }
    }
}
