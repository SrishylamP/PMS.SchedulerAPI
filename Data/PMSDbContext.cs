using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PMS.SchedulerAPI.Data
{
    public partial class PMSDbContext : DbContext
    {
        public PMSDbContext()
        {
        }

        public PMSDbContext(DbContextOptions<PMSDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allergy> Allergies { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<AppointmentHistory> AppointmentHistories { get; set; }
        public virtual DbSet<AppointmentSlot> AppointmentSlots { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<DiagnosesType> DiagnosesTypes { get; set; }
        public virtual DbSet<Diagnosis> Diagnoses { get; set; }
        public virtual DbSet<EmergencyContactDetail> EmergencyContactDetails { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<PatientAllergyDetail> PatientAllergyDetails { get; set; }
        public virtual DbSet<PatientDetail> PatientDetails { get; set; }
        public virtual DbSet<PatientDiagnosisDetail> PatientDiagnosisDetails { get; set; }
        public virtual DbSet<PatientMedicationsDetail> PatientMedicationsDetails { get; set; }
        public virtual DbSet<PatientProcedureDetail> PatientProcedureDetails { get; set; }
        public virtual DbSet<PatientVitalDetail> PatientVitalDetails { get; set; }
        public virtual DbSet<Procedure> Procedures { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Allergy>(entity =>
            {
                entity.Property(e => e.AllergenDescription).IsUnicode(false);

                entity.Property(e => e.Allerginicity).IsUnicode(false);

                entity.Property(e => e.AllergyClinicalInformation).IsUnicode(false);

                entity.Property(e => e.AllergyCode).IsUnicode(false);

                entity.Property(e => e.AllergyId).ValueGeneratedOnAdd();

                entity.Property(e => e.AllergyName).IsUnicode(false);

                entity.Property(e => e.AllergyType).IsUnicode(false);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentStatus).IsUnicode(false);

                entity.Property(e => e.AppointmentTime).IsUnicode(false);

                entity.Property(e => e.AppointmentType).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.AppointmentPatients)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Patie__70DDC3D8");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.AppointmentPhysicians)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Physi__6FE99F9F");
            });

            modelBuilder.Entity<AppointmentHistory>(entity =>
            {
                entity.Property(e => e.AppointmentTime).IsUnicode(false);

                entity.Property(e => e.Reason).IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentHistories)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Appoi__73BA3083");
            });

            modelBuilder.Entity<AppointmentSlot>(entity =>
            {
                entity.Property(e => e.TimeSlot).IsUnicode(false);
            });

            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.ObjectName).IsUnicode(false);

                entity.Property(e => e.Operation).IsUnicode(false);
            });

            modelBuilder.Entity<DiagnosesType>(entity =>
            {
                entity.Property(e => e.DiagnosesType1).IsUnicode(false);
            });

            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.Property(e => e.DiagnosisCode).IsUnicode(false);

                entity.Property(e => e.DiagnosisName).IsUnicode(false);

                entity.HasOne(d => d.DiagnosisType)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.DiagnosisTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diagnoses__Diagn__7B5B524B");
            });

            modelBuilder.Entity<EmergencyContactDetail>(entity =>
            {
                entity.HasKey(e => e.EmergencyContactId)
                    .HasName("PK__Emergenc__E8A61D8E3FB36417");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.EfisrtName).IsUnicode(false);

                entity.Property(e => e.ElastName).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.RelationShip).IsUnicode(false);
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasKey(e => e.DrugId)
                    .HasName("PK__Medicati__908D661653FD92F8");

                entity.Property(e => e.DrugBrandName).IsUnicode(false);

                entity.Property(e => e.DrugForm).IsUnicode(false);

                entity.Property(e => e.DrugName).IsUnicode(false);

                entity.Property(e => e.DrugStrength).IsUnicode(false);

                entity.Property(e => e.ReferenceStandard).IsUnicode(false);
            });

            modelBuilder.Entity<PatientAllergyDetail>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.AllergyId })
                    .HasName("PK__PatientA__AD47288072B4A369");

                entity.Property(e => e.AllergenDescription).IsUnicode(false);

                entity.Property(e => e.AllergyClinicalInformation).IsUnicode(false);

                entity.Property(e => e.AllergyName).IsUnicode(false);

                entity.Property(e => e.AllergyType).IsUnicode(false);
            });

            modelBuilder.Entity<PatientDetail>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__PatientD__970EC366BC1D9C0D");

                entity.Property(e => e.Ethnicity).IsUnicode(false);

                entity.Property(e => e.HomeAddress).IsUnicode(false);

                entity.Property(e => e.LanguagesKnow).IsUnicode(false);

                entity.Property(e => e.Race).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PatientDetails)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PatientDe__UserI__02FC7413");
            });

            modelBuilder.Entity<PatientDiagnosisDetail>(entity =>
            {
                entity.HasKey(e => e.PatientDiagnosisId)
                    .HasName("PK__PatientD__B0473866C5278466");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientDiagnosisDetails)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientDi__Appoi__6CD828CA");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.PatientDiagnosisDetails)
                    .HasForeignKey(d => d.DiagnosisId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientDi__Diagn__6BE40491");
            });

            modelBuilder.Entity<PatientMedicationsDetail>(entity =>
            {
                entity.HasKey(e => e.PatientMedicationId)
                    .HasName("PK__PatientM__B08C9294BB4D4C20");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientMedicationsDetails)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientMe__Appoi__74794A92");

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.PatientMedicationsDetails)
                    .HasForeignKey(d => d.DrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientMe__DrugI__73852659");
            });

            modelBuilder.Entity<PatientProcedureDetail>(entity =>
            {
                entity.HasKey(e => e.PatientProcedureId)
                    .HasName("PK__PatientP__A23496BF9E0F8BAA");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientProcedureDetails)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientPr__Appoi__70A8B9AE");

                entity.HasOne(d => d.Procedure)
                    .WithMany(p => p.PatientProcedureDetails)
                    .HasForeignKey(d => d.ProcedureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientPr__Proce__6FB49575");
            });

            modelBuilder.Entity<PatientVitalDetail>(entity =>
            {
                entity.HasKey(e => e.VitalId)
                    .HasName("PK__PatientV__725DA54A9CA25F84");

                entity.Property(e => e.BloodPressure).IsUnicode(false);

                entity.Property(e => e.BodyTemprature).IsUnicode(false);

                entity.Property(e => e.Height).IsUnicode(false);

                entity.Property(e => e.RespirationRate).IsUnicode(false);

                entity.Property(e => e.Weight).IsUnicode(false);

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientVitalDetails)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PatientVi__Appoi__625A9A57");
            });

            modelBuilder.Entity<Procedure>(entity =>
            {
                entity.Property(e => e.ProcedureApproach).IsUnicode(false);

                entity.Property(e => e.ProcedureCode).IsUnicode(false);

                entity.Property(e => e.ProcedureName).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.Title).IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__398D8EEE");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__UserRole__8AFACE1AB62AD8FF");

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
