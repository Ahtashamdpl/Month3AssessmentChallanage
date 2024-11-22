using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Month3AssessmentCode.Models;

namespace Month3AssessmentCode.ApplDbCont
{
    public class ApplicationDBContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
            
        }

       public DbSet<TaskModel> Tasks { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        //{

        //    optionsBuilder.UseSqlServer("Write your database connection string");

        //}



        //protected override void OnModelCreating(ModelBuilder modelBuilder)

        //{

        //    modelBuilder.Entity<re>(entity =>
        //    {

        //        entity.ToTable("User");
        //        entity.HasKey(p => p.Id).HasName("PK_User");

        //        entity.Property(p => p.Id)

        //        .HasColumnName("id")

        //        .HasColumnType("int").ValueGeneratedNever();

        //        entity.Property(p => p.Name)

        //        .HasColumnName("name");

        //    });

        //}
    }
}
