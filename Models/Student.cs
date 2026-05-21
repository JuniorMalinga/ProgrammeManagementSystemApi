using System.ComponentModel.DataAnnotations;

namespace ProgrammeManagementSystemApi.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required, Range(1, 4)]
        public int YearOfStudy { get; set; }

        // Navigation property
        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();

        public string FullName => $"{FirstName} {LastName}";
    }
}
