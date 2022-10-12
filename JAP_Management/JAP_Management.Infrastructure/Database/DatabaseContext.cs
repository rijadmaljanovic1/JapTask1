using Microsoft.EntityFrameworkCore;
using JAP_Management.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace JAP_Management.Infrastructure.Database
{
    public class DatabaseContext : IdentityDbContext<BaseUser>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BaseUser> Users { get; set; }
        public DbSet<Admin> Admins{ get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<StudentStatus> StudentStatus { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<SelectionStatus> SelectionStatus { get; set; }
        public DbSet<Technologies> Technologies { get; set; }
        public DbSet<Rank> Ranks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost, 1433;Initial Catalog=JAP_Management; Trusted_Connection=True; user=sa; Password=QWEasd123!",
                options => options.EnableRetryOnFailure());
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            
        }
    }
}
