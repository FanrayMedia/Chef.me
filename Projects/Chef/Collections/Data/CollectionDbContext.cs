using System.Data.Entity;

namespace Chef.Collections.Data
{
    public class CollectionDbContext : DbContext
    {
        public CollectionDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<ItemEntity> ItemEntities { get; set; }
        public DbSet<GroupEntity> GroupEntities { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>().ToTable("GroupItem");
            modelBuilder.Entity<GroupEntity>().ToTable("Group");

            modelBuilder.Entity<GroupEntity>()
                .HasMany(g => g.ItemEntities)
                .WithOptional()
                .HasForeignKey(c => c.GroupId);
        }
    }
}
