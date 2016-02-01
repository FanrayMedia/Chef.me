using System.Data.Entity;

namespace Chef.Profiles
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileStyle> ProfileStyles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<ProfileStyle>().ToTable("ProfileStyle");
        }
    }
}
