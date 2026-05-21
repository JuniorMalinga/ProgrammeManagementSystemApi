using Microsoft.EntityFrameworkCore;
using ProgrammeManagementSystemApi.Models;

namespace ProgrammeManagementSystemApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<ModuleAssignment> ModuleAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraints
            modelBuilder.Entity<Module>()
                .HasIndex(m => m.ModuleCode)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Lecturer>()
                .HasIndex(l => l.Email)
                .IsUnique();

            // Table names
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<Module>().ToTable("Module");
            modelBuilder.Entity<Registration>().ToTable("Registration");
            modelBuilder.Entity<ModuleAssignment>().ToTable("ModuleAssignment");

            // Relationships and Cascade Delete behavior
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Registrations)
                .HasForeignKey(r => r.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Module)
                .WithMany(m => m.Registrations)
                .HasForeignKey(r => r.ModuleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ModuleAssignment>()
                .HasOne(ma => ma.Lecturer)
                .WithMany(l => l.ModuleAssignments)
                .HasForeignKey(ma => ma.LecturerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ModuleAssignment>()
                .HasOne(ma => ma.Module)
                .WithMany(m => m.ModuleAssignments)
                .HasForeignKey(ma => ma.ModuleID)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique indexes for join tables
            modelBuilder.Entity<Registration>()
                .HasIndex(r => new { r.StudentID, r.ModuleID })
                .IsUnique();

            modelBuilder.Entity<ModuleAssignment>()
                .HasIndex(ma => new { ma.LecturerID, ma.ModuleID })
                .IsUnique();
        }
    }
}
