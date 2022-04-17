using DocumentWorkflow.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentWorkflow.Core.DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CategoryRights> CategoriesRights { get; set; }
        public DbSet<UserRoles> UsersRoles { get; set; }

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<LogBook> LogBooks { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Document> Documents { get; set; }
        public DbSet<History> DocumentsHistory { get; set; }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CategoryRights>().HasNoKey();
            //modelBuilder.Entity<UserRoles>().HasNoKey();
        }
    }
}
