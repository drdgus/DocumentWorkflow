using DocumentWorkflow.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentWorkflow.Core.DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Template> Templates {get; set; }
        public DbSet<User> Users {get; set; }
        public DbSet<History> DocumentsHistory { get; set; }

        public DbSet<DocumentType> DocumentTypes {get; set; }
        public DbSet<DocumentCategory> DocumentCategories {get; set; }
        public DbSet<Right> Rights {get; set; }
        public DbSet<Role> Roles {get; set; }
        public DbSet<UserRoles> UsersRoles {get; set; }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Right>().HasNoKey();
            //modelBuilder.Entity<UserRoles>().HasNoKey();
        }
    }
}
