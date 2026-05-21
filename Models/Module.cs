using System.ComponentModel.DataAnnotations;

namespace ProgrammeManagementSystemApi.Models
{
    public class Module
    {
        public int ModuleID { get; set; }

        [Required, StringLength(200)]
        public string ModuleName { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string ModuleCode { get; set; } = string.Empty;

        [Required, Range(1, 30)]
        public int Credits { get; set; }

        [Required, StringLength(20)]
        public string AcademicYear { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
        public ICollection<ModuleAssignment> ModuleAssignments { get; set; } = new List<ModuleAssignment>();

        public string ModuleDisplay => $"{ModuleCode} - {ModuleName}";
    }
}
