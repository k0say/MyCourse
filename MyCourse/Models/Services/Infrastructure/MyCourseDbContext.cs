using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyCourse.Models.Entities;

namespace MyCourse.Models.Services.Infrastructure
{
    public partial class MyCourseDbContext : DbContext
    {
        public MyCourseDbContext(DbContextOptions<MyCourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");  // Superfluo se la tab si chiama come la proprietà che va a mappare
                entity.HasKey(course => course.Id); // superfluo se la proprietà si chiama Id oppure CoursesId
                                                    //entity.HasKey(course => new { course.Id, course.Author });

                //Mapping per gli owned types
                entity.OwnsOne(course => course.CurrentPrice, builder =>
                {
                    builder.Property(money => money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("CurrentPrice_Currency"); //superfluo giacchè le colonne attuali seguono la convenzione di nomi
                    builder.Property(money => money.Amount).HasColumnName("CurrentPrice_Amount"); //superfluo giacchè le colonne attuali seguono la convenzione di nomi
                });

                entity.OwnsOne(course => course.FullPrice, builder =>
                {
                    builder.Property(money => money.Currency).HasConversion<string>();
                });

                //Mapping per le relazioni
                entity.HasMany(course => course.Lessons)
                    .WithOne(lesson => lesson.Course)
                    .HasForeignKey(lesson => lesson.CourseId); //Superflua se la proprietà si chiama CourseId

                #region Mapping generato automaticamente
                //entity.HasIndex(e => e.Title)
                //    .HasName("ux_title")
                //    .IsUnique();

                //entity.Property(e => e.Id).ValueGeneratedNever();

                //entity.Property(e => e.Author)
                //    .IsRequired()
                //    .HasColumnType("TEXT (100)");

                //entity.Property(e => e.CurrentPriceAmount)
                //    .IsRequired()
                //    .HasColumnName("CurrentPrice_Amount")
                //    .HasColumnType("NUMERIC")
                //    .HasDefaultValueSql("0");

                //entity.Property(e => e.CurrentPriceCurrency)
                //    .IsRequired()
                //    .HasColumnName("CurrentPrice_Currency")
                //    .HasColumnType("TEXT (3)")
                //    .HasDefaultValueSql("'EUR'");

                //entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                //entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                //entity.Property(e => e.FullPriceAmount)
                //    .IsRequired()
                //    .HasColumnName("FullPrice_Amount")
                //    .HasColumnType("NUMERIC")
                //    .HasDefaultValueSql("0");

                //entity.Property(e => e.FullPriceCurrency)
                //    .IsRequired()
                //    .HasColumnName("FullPrice_Currency")
                //    .HasColumnType("TEXT (3)")
                //    .HasDefaultValueSql("'EUR'");

                //entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                //entity.Property(e => e.RowVersion).HasColumnType("DATETIME");

                //entity.Property(e => e.Title)
                //    .IsRequired()
                //    .HasColumnType("TEXT (100)");
                #endregion
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(lesson => lesson.Course)
                    .WithMany(course => course.Lessons);

                #region Mapping generato automaticamente
                //entity.Property(e => e.Id).ValueGeneratedNever();

                //entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                //entity.Property(e => e.Duration)
                //    .IsRequired()
                //    .HasColumnType("TEXT (8)")
                //    .HasDefaultValueSql("'00:00:00'");

                //entity.Property(e => e.Title)
                //    .IsRequired()
                //    .HasColumnType("TEXT (100)");

                //entity.HasOne(d => d.Course)
                //    .WithMany(p => p.Lessons)
                //    .HasForeignKey(d => d.CourseId);
                #endregion
            });
        }
    }
}
